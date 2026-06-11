using Application.Features.Survey.Commands.Create;
using Application.Features.Survey.Commands.Delete;
using Application.Features.Survey.Commands.Update;
using Application.Features.Survey.Commands.Update.updatequestions;
using Application.Features.Survey.Queries.Model;
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

    public class SurveyController : ControllerBase
    {
        IMediator _mediator;
        UserManager<ApplicationUser> _userManager;

        public SurveyController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetAllSurveysListQuery());
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] GetSurveysPagedQuery query)
        {
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _mediator.Send(new GetSurveyByIdQuery { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSurveyCommand command)
        {
            var user = await _userManager.GetUserAsync(User);
            
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateSurveyCommand command)
        {
            command.Id = id;
            var user = await _userManager.GetUserAsync(User);
            //command.UpdatedBy = user?.FullName;
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPatch("updateQuestions/{id:int}")]
        public async Task<IActionResult> UpdateQuestionsAsync(int id, [FromBody] UpdateSurveyQuestionsCommandtwo command)
        {
            command.Id = id;
            
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(new DeleteSurveyCommand { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }



        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllSurveysForUser(
           Guid userId,
           [FromQuery] bool isCompleted = false,
           [FromQuery] string search = "")
        {
            var result = await _mediator.Send(
                new GetAllSurveysForUserByIdQuery
                {
                    UserId = userId,
                    IsCompleted = isCompleted,
                    Search = search
                });

            return Ok(result);
        }
        [HttpGet("specified")]
        public async Task<IActionResult> GetAllSpecifiedSurveys()
        {
            var result = await _mediator.Send(
                new GetAllSpecifiedSurveysQuery());

            return Ok(result);
        }
    }
}
