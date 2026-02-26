using Application.Common;
using Application.Features.User.Commands.Create;
using Application.Features.User.Commands.Create.AssignCourseToUser;
using Application.Features.User.Commands.Create.AssignSessionToUser;
using Application.Features.User.Commands.Create.AssignTrainingToUser;
using Application.Features.User.Commands.Create.CreateUser;
using Application.Features.User.Commands.Delete;
using Application.Features.User.DTOs;
using Application.Features.User.Queries;
using Application.Features.User.Queries.Model;
using Azure;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;
        MediatR.IMediator _mediator;

        public UserController(UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _mediator.Send(new Application.Features.User.Queries.Model.GetAllUsersQuery());
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO user)
        {

            var response = await _mediator.Send(new CreateUserCommand { _dto = user });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetUsersPaged(int pageNumber = 1, int pageSize = 10, string search = "")
        {
            var response = await _mediator.Send(new GetAllUsersPagedQuery { PageNumber = pageNumber, PageSize = pageSize, Search = search });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("username/{userName}")]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            var response = await _mediator.Send(new GetUserByUserNameQuery { UserName = userName });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery { Id = id });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var response = await _mediator.Send(new GetUserByEmailQuery { Email = email });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("role/{role}")]
        public async Task<IActionResult> GetUsersByRole(string role)
        {
            var response = await _mediator.Send(new GetUsersByRolesQuery { roleName = role });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPatch("{userName}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string userName, [FromBody] UserDTO user)
        {
            user.UserName = userName; // Ensure the username in the URL is used
            var response = await _mediator.Send(new Application.Features.User.Commands.Update.UpdateUserCommand { User = user });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteByName/{userName}")]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            var command = new DeleteUserByUserNameCommand
            {
                UserName = userName
            };

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpDelete("DeleteByEmail/{email}")]
        public async Task<IActionResult> DeleteUserbyEmail(string email)
        {
            var command = new DeleteUserByEmailCommand
            {
                Email = email
            };

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpGet("training/{trainingId}")]
        public async Task<IActionResult> GetUsersByTrainingId(int trainingId,int pageNumber = 1, int pageSize = 10)
        {
            var response = await _mediator.Send(new GetUsersTrainingRangePaginatedQuery { PageNumber = pageNumber, PageSize = pageSize, TrainingId = trainingId });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }
        
        [HttpGet("trainings/{userId}/training")]
        public async Task<IActionResult> GetTrainingByUserId(Guid userId, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _mediator.Send(new GetTrainingUsersRangePaginatedQuery { PageNumber = pageNumber, PageSize = pageSize, UserId = userId });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        //todo

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetUsersByCourseId(int courseId, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _mediator.Send(new GetUsersCourseRangePaginatedQuery { PageNumber = pageNumber, PageSize = pageSize, CourseId = courseId });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpGet("courses/{userId}/course")]
        public async Task<IActionResult> GetCourseByUserId(Guid userId, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _mediator.Send(new GetCourseUsersRangePaginatedQuery { PageNumber = pageNumber, PageSize = pageSize, UserId = userId });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        //todo

        [HttpGet("session/{sessionId}")]
        public async Task<IActionResult> GetUsersBySessionId(int sessionId, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _mediator.Send(new GetUsersSessionRangePaginatedQuery { PageNumber = pageNumber, PageSize = pageSize, SessionId = sessionId });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpGet("sessions/{userId}/session")]
        public async Task<IActionResult> GetSessionByUserId(Guid userId, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _mediator.Send(new GetSessionUsersRangePaginatedQuery { PageNumber = pageNumber, PageSize = pageSize, UserId = userId });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        //to dooooooooooooo asign users in sys DON'T IGNORE
        [HttpPost("assignTraining")]
        public async Task<IActionResult> AssignUserToTraining([FromBody] UsersTrainingDTO dto)
        {
            var response = await _mediator.Send(new AssignTrainingToUserCommand { _dto=dto });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("assignCourse")]
        public async Task<IActionResult> AssignUserToCourse([FromBody] UsersCourseDTO dto)
        {
            var response = await _mediator.Send(new AssignCourseToUserCommand { _dto=dto });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("assignSession")]
        public async Task<IActionResult> AssignUserToSession([FromBody] UsersSessionDTO dto)
        {
            var response = await _mediator.Send(new AssignSessionToUsersCommand { _dto=dto });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("userinrole")]
        public async Task<IActionResult> GetUsersInRole ( UsersRolesEnum role)
        {
            var response = await _mediator.Send(new GetUsersInRoleQuery { Role=role });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
