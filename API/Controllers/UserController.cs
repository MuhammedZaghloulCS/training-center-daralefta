using Application.Common;
using Application.Features.User.Commands.Activation.Command;
using Application.Features.User.Commands.Create;
using Application.Features.User.Commands.Create.AssignCourseToUser;
using Application.Features.User.Commands.Create.AssignSessionToUser;
using Application.Features.User.Commands.Create.AssignTrainingToUser;
using Application.Features.User.Commands.Create.CreateUser;
using Application.Features.User.Commands.DeActivate.Command;
using Application.Features.User.Commands.Delete;
using Application.Features.User.DTOs;
using Application.Features.User.Queries;
using Application.Features.User.Queries.Model;
using Azure;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class UserController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;
        MediatR.IMediator _mediator;
        private readonly HttpClient _httpClientFactory;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(UserManager<ApplicationUser> userManager, IMediator mediator, IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _mediator = mediator;
            _httpClientFactory = httpClientFactory.CreateClient("ExternalApi");
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("test-image")]
        public async Task<IActionResult> testImagePreview()
        {


            var client = _httpClientFactory;

            var token = "0699F386CB16161E0AD3E86AA0A08CA8F178C0823C23119CDF34A53C84E7783C";
            var url = $"https://10.0.250.169:8098/api/person/get/1?access_token={token}";

            var response = await client.GetStringAsync(url);
            var json = JsonDocument.Parse(response);

            // جيب الصورة من الـ JSON
            var base64 = json.RootElement
                             .GetProperty("data")
                             .GetProperty("personPhoto")
                             .GetString();

            // حوّل base64 لـ bytes
            var imageBytes = Convert.FromBase64String(base64);

            // احفظها في wwwroot
            var savePath = Path.Combine("wwwroot", "images", "employees");
            Directory.CreateDirectory(savePath);

            var filePath = Path.Combine(savePath, "admin_photo.jpg");
            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
            return Ok("see wwwroot");
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
        [HttpGet("sysusers")]
        public async Task<IActionResult> GetSysUsers()
        {
            var response = await _mediator.Send(new Application.Features.User.Queries.Model.GetAllSysUsersQuery());
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpGet("diffsysusers")]
        public async Task<IActionResult> GetDiffSysUsers()
        {
            var response = await _mediator.Send(new Application.Features.User.Queries.Model.GetDiffSysUsersQuery());
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("sysusers/{pin}")]
        public async Task<IActionResult> GetSysUsers(string pin)
        {
            var response = await _mediator.Send(new Application.Features.User.Queries.Model.GetSysUserByPinQuery { Pin = pin });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO user)
        {
            var user1 = await _userManager.GetUserAsync(User);
            user.CreatedBy = user1?.FullName;
            var response = await _mediator.Send(new CreateUserCommand { _dto = user });
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("with-image")]
        public async Task<IActionResult> CreateUserWithImage([FromForm] CreateUserDTO user)
        {
            var user1 = await _userManager.GetUserAsync(User);
            user.CreatedBy = user1?.FullName;

            Console.WriteLine($"CreateUserWithImage called. ImageFile: {(user.ImageFile != null ? user.ImageFile.FileName : "null")}");

            // Handle image upload
            if (user.ImageFile != null && user.ImageFile.Length > 0)
            {
                try
                {
                    // Save to external storage folder (two levels up - outside project)
                    var storagePath = Path.GetFullPath(Path.Combine(_webHostEnvironment.ContentRootPath, "..", "..", "TrainingCenterStorage"));
                    var uploadsFolder = Path.Combine(storagePath, "images", "users");
                    Directory.CreateDirectory(uploadsFolder);

                    // Always save as .jpg to avoid PNG permission issues
                    var uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await user.ImageFile.CopyToAsync(fileStream);
                    }

                    user.ImagePath = $"/storage/images/users/{uniqueFileName}";
                    Console.WriteLine($"Image saved. user.ImagePath: {user.ImagePath}");
                }
                catch (Exception ex)
                {
                    // Log error but continue without image
                    Console.WriteLine($"Error saving image: {ex.Message}");
                }
            }

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
        public async Task<IActionResult> UpdateUser([FromRoute] string userName, [FromBody] UpdateUserDTO user)
        {
            var user1 = await _userManager.GetUserAsync(User);
            user.UpdatedBy = user1?.FullName;
            user.UserName = userName; // Ensure the username in the URL is used

            var response = await _mediator.Send(new Application.Features.User.Commands.Update.UpdateUserCommand { UpdateUser = user });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPatch("{userName}/with-image")]
        public async Task<IActionResult> UpdateUserWithImage([FromRoute] string userName, [FromForm] UpdateUserDTO user)
        {
            var user1 = await _userManager.GetUserAsync(User);
            user.UpdatedBy = user1?.FullName;
            user.UserName = userName;

            Console.WriteLine($"UpdateUserWithImage called. ImageFile: {(user.ImageFile != null ? user.ImageFile.FileName : "null")}, ImagePath: '{user.ImagePath}', RemoveImage: {user.RemoveImage}");

            // Get existing user to check for old image
            var existingUser = await _userManager.FindByNameAsync(userName);
            Console.WriteLine($"Existing user ImagePath: '{existingUser?.ImagePath}'");

            // Handle image upload
            if (user.ImageFile != null && user.ImageFile.Length > 0)
            {
                try
                {
                    var storagePath = Path.GetFullPath(Path.Combine(_webHostEnvironment.ContentRootPath, "..", "..", "TrainingCenterStorage"));
                    
                    // Delete old image if exists
                    if (existingUser != null && !string.IsNullOrEmpty(existingUser.ImagePath))
                    {
                        var oldFilePath = Path.Combine(storagePath, existingUser.ImagePath.Replace("/storage/", "").Replace("/", Path.DirectorySeparatorChar.ToString()));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    var uploadsFolder = Path.Combine(storagePath, "images", "users");
                    Directory.CreateDirectory(uploadsFolder);

                    // Always save as .jpg to avoid PNG permission issues
                    var uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await user.ImageFile.CopyToAsync(fileStream);
                    }

                    user.ImagePath = $"/storage/images/users/{uniqueFileName}";
                    Console.WriteLine($"Image saved. user.ImagePath: {user.ImagePath}");
                }
                catch (Exception ex)
                {
                    // Log error but continue without image
                    Console.WriteLine($"Error saving image: {ex.Message}");
                }
            }
            else if (user.RemoveImage && existingUser != null && !string.IsNullOrEmpty(existingUser.ImagePath))
            {
                // User wants to delete image - remove file from wwwroot
                Console.WriteLine($"Removing image. Old path: {existingUser.ImagePath}");
                try
                {
                    var storagePath = Path.GetFullPath(Path.Combine(_webHostEnvironment.ContentRootPath, "..", "..", "TrainingCenterStorage"));
                    var oldFilePath = Path.Combine(storagePath, existingUser.ImagePath.Replace("/storage/", "").Replace("/", Path.DirectorySeparatorChar.ToString()));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                        Console.WriteLine($"Image file deleted: {oldFilePath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting image file: {ex.Message}");
                }
                user.ImagePath = "";  // Signal to handler to clear ImagePath in DB
            }

            Console.WriteLine($"Before mediator. user.ImagePath: {user.ImagePath}");
            var response = await _mediator.Send(new Application.Features.User.Commands.Update.UpdateUserCommand { UpdateUser = user });
            Console.WriteLine($"After mediator. response.data.imagePath: {response.Data?.ImagePath}");
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
        public async Task<IActionResult> GetUsersByTrainingId(int trainingId, int pageNumber = 1, int pageSize = 10)
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
            var response = await _mediator.Send(new AssignTrainingToUserCommand { _dto = dto });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("assignCourse")]
        public async Task<IActionResult> AssignUserToCourse([FromBody] UsersCourseDTO dto)
        {

            var response = await _mediator.Send(new AssignCourseToUserCommand { _dto = dto });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("assignSession")]
        public async Task<IActionResult> AssignUserToSession([FromBody] UsersSessionDTO dto)
        {
            var response = await _mediator.Send(new AssignSessionToUsersCommand { _dto = dto });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("userinroleV2")]
        public async Task<IActionResult> GetUsersInRole(UsersRolesEnum role)
        {
            var response = await _mediator.Send(new GetUsersInRoleQuery { Role = role });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPatch("activate/{username}")]
        public async Task<IActionResult> ActivateUser(string username)
        {
            var response = await _mediator.Send(new ActivateUserByUsernameCommand { Username = username });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPatch("deactivate/{username}")]
        public async Task<IActionResult> DeactivateUser(string username)
        {
            var response = await _mediator.Send(new DeactivateUserByUsernameCommand { Username = username });
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("activatedusers")]

        public async Task<IActionResult> GetActivatedUsers()
        {
            var response = await _mediator.Send(new GetAllActivatedUsersQuery());
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
