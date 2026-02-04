using Application.Common;
using Application.Features.User.Commands.Create;
using Application.Features.User.Commands.Delete;
using Application.Features.User.DTOs;
using Azure;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;
        MediatR.IMediator _mediator;

        public UsersController(UserManager<ApplicationUser> userManager, MediatR.IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
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
           
            var response = await _mediator.Send(new CreateUserCommand{ _dto=user});
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO user)
        {
            var response = await _mediator.Send(new Application.Features.User.Commands.Update.UpdateUserCommand { User = user });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteUserCommand
            {
                UserId = id
            };

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
