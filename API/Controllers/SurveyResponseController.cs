using Application.Features.SurveyResponse.Commands.Create;
using Application.Features.SurveyResponse.Commands.Delete;
using Application.Features.SurveyResponse.Commands.Update;
using Application.Features.SurveyResponse.Queries.Model;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyResponseController : ControllerBase
    {
        IMediator _mediator;
        UserManager<ApplicationUser> _userManager;
        public SurveyResponseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetAllSurveyResponsesListQuery());
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] GetSurveyResponsesPagedQuery query)
        {
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _mediator.Send(new GetSurveyResponseByIdQuery { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSurveyResponseCommand command)
        {
            var user = await _userManager.GetUserAsync(User);
            command.CreatedBy = user?.FullName;
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateSurveyResponseCommand command)
        {
            command.Id = id;
            var user = await _userManager.GetUserAsync(User);
            command.UpdatedBy = user?.FullName;
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(new DeleteSurveyResponseCommand { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
