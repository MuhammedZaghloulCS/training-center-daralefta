using Application.Features.Training.Commands.Create;
using Application.Features.Training.Commands.Delete;
using Application.Features.Training.Commands.Update;
using Application.Features.Training.Commands.Update.Trainees;
using Application.Features.Training.Queries.Model;
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

    public class TrainingController : ControllerBase
    {
        IMediator _mediator;
        UserManager<ApplicationUser> _userManager;
        public TrainingController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetAllTrainingsListQuery());
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("paged")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] GetTrainingsPagedQuery query)
        {
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _mediator.Send(new GetTrainingWithCoursesQuery { TrainingId = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTrainingCommand command)
        {
            var user = await _userManager.GetUserAsync(User);
            command.CreatedBy = user?.FullName;
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateTrainingCommand command)
        {
            command.Id = id;
            var user = await _userManager.GetUserAsync(User);
            command.UpdatedBy = user?.FullName;
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPatch("updatetrainees")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateTrainingTraineesCommand command)
        {

            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(new DeleteTrainingCommand { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpGet("schedule/{id}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetScheduleAsync(Guid id)
        {
            var response = await _mediator.Send(new GetUserScheduleQuery { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpGet("schedule/instructor/{id}")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> GetScheduleInstructorAsync(Guid id)
        {
            var response = await _mediator.Send(new GetInstructorScheduleQuery { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
