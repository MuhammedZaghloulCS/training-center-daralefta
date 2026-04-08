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
    [Authorize(Roles = "Admin")]

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
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetAllTrainingsListQuery());
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] GetTrainingsPagedQuery query)
        {
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _mediator.Send(new GetTrainingWithCoursesQuery { TrainingId = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTrainingCommand command)
        {
            var user = await _userManager.GetUserAsync(User);
            command.CreatedBy = user?.FullName;
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateTrainingCommand command)
        {
            command.Id = id;
            var user = await _userManager.GetUserAsync(User);
            command.UpdatedBy = user?.FullName;
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPatch("updatetrainees")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateTrainingTraineesCommand command)
        {
           
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(new DeleteTrainingCommand { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
