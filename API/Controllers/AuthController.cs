using Application.Common.Abstraction;
using Application.Features.User.Commands.ResetPassword.Command;
using Domain.Entities;
using Domain.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IMediator _mediator;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService,
            IMediator mediator
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Domain.Entities.Models.LoginRequest request)
        {
            if (request == null)
                throw new Exception("Request is null");
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null||user.IsDeleted)
                return Unauthorized(new { message = "الإيميل او كلمة السر خاطئة" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);
            if (!result.Succeeded)
                return Unauthorized(new { message = "الإيميل او كلمة السر خاطئة" });

            var roles = await _userManager.GetRolesAsync(user);

            // توليد Access Token و Refresh Token
            var accessToken = _jwtService.GenerateAccessToken(user.Id, user.UserName, user.Email, roles.ToList());
            var refreshToken = _jwtService.GenerateRefreshToken();

            // حفظ Refresh Token في الداتابيز
            user.RefreshToken = refreshToken;
            // إذا كان Remember Me = true، اجعل الـ Refresh Token صالح لمدة شهر، وإلا 7 أيام
            var expiryDays = request.RememberMe ? 30 : 7;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(expiryDays);
            await _userManager.UpdateAsync(user);

            return Ok(new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Email = user.Email,
                UserName = user.UserName
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            // استخراج البيانات من Access Token المنتهي
            var principal = _jwtService.GetPrincipalFromExpiredToken(request.AccessToken);
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return BadRequest(new { message = "Invalid access token" });

            var user = await _userManager.FindByIdAsync(userId);

            // التحقق من الـ Refresh Token
            if (user == null)
                return BadRequest(new { message = "User not found" });
            
            if (user.IsDeleted)
                return BadRequest(new { message = "User is deleted" });
            
            if (user.RefreshToken != request.RefreshToken)
                return BadRequest(new { message = $"Token mismatch. DB: {(user.RefreshToken ?? "NULL")}, Request: {(request.RefreshToken ?? "NULL")}" });
            
            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return BadRequest(new { message = $"Token expired. Expiry: {user.RefreshTokenExpiryTime}, Now: {DateTime.UtcNow}" });

            var roles = await _userManager.GetRolesAsync(user);

            // توليد Access Token جديد
            var newAccessToken = _jwtService.GenerateAccessToken(user.Id, user.UserName, user.Email, roles.ToList());
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            // تحديث Refresh Token
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new LoginResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Email = user.Email,
                UserName = user.UserName
            });
        }

        [Authorize]
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null || user.IsDeleted)
                return NotFound();

            // إلغاء الـ Refresh Token (Logout)
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return Ok(new { message = "Token revoked successfully" });
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("resetPasswordByAdmin")]
        public async Task<IActionResult> ResetPasswordByAdmin([FromBody] ResetPasswordCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
    }

   
}