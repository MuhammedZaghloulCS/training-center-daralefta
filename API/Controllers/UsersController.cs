using Application.Common;
using Application.Features.User.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var result=new BaseResponse<List<ApplicationUser>>
            {
                Data = users,
                Success = true,
                Message = users.Count()>0?"Users retrieved successfully": "Mission Succeeded,but No users found",

            };
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id=Guid.NewGuid();
            string userName = user.FirstName + id.ToString("N")[..6];
            var newUser = new ApplicationUser
            {
              Id = id,
              UserName = userName,
              Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender =user.Gender,
                PersonType = user.PersonType,
                JobTitle = user.JobTitle,
                AcademicTitle = user.AcademicTitle,
                Organization = user.Organization,
                Specialization = user.Specialization,
                Skills = user.Skills,
                WhatsappNumber = user.WhatsappNumber,
                BirthDate = DateTime.Parse(user.BirthDate),
                NationalIdImage = user.NationalIdImage,
                AddressInsideCairo = user.AddressInsideCairo,
                AddressOutsideCairo = user.AddressOutsideCairo,
                Doctrine = user.Doctrine,
                MaritalState = user.MaritalState,
                AcademicQualification = user.AcademicQualification,
                Appreciation = user.Appreciation,
                ImagePath = user.ImagePath


            };
            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(errors);
            }
                return Ok(user);
        }

    }
}
