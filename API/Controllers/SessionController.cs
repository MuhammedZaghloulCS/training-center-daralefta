using Application.Features.Session.Commands.Create;
using Application.Features.Session.Commands.Delete;
using Application.Features.Session.Commands.Files;
using Application.Features.Session.Commands.Update;
using Application.Features.Session.Queries.Model;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class SessionController : ControllerBase
    {
        IMediator _mediator;
        UserManager<ApplicationUser> _userManager;
        public SessionController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetAllSessionsListQuery());
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("paged")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] GetSessionsPagedQuery query)
        {
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _mediator.Send(new GetSessionByIdQuery { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateSessionCommand command)
        {
            var user = await _userManager.GetUserAsync(User);
            command.CreatedBy = user?.FullName;
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateSessionCommand command)
        {
            command.Id = id;
            var user = await _userManager.GetUserAsync(User);
            command.UpdatedBy = user?.FullName;
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(new DeleteSessionCommand { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        #region File Management

        [HttpGet("{sessionId:int}/files")]
        [Authorize(Roles = "Admin,Student,Instructor")]
        public async Task<IActionResult> GetSessionFiles(int sessionId)
        {
            var response = await _mediator.Send(new GetSessionFilesQuery { SessionId = sessionId });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("{sessionId:int}/files")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> AddSessionFiles(int sessionId, List<IFormFile> files)
        {
            var command = new AddSessionFileCommand
            {
                SessionId = sessionId,
                Files = files
            };
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{sessionId:int}/files")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> DeleteSessionFile(int sessionId, [FromQuery] string filePath)
        {
            var command = new DeleteSessionFileCommand
            {
                SessionId = sessionId,
                FilePath = filePath
            };
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        #endregion
    }
}
