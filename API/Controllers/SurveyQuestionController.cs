using Application.Features.SurveyQuestion.Commands.Create;
using Application.Features.SurveyQuestion.Commands.Delete;
using Application.Features.SurveyQuestion.Commands.Update;
using Application.Features.SurveyQuestion.Queries.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyQuestionController : ControllerBase
    {
        IMediator _mediator;

        public SurveyQuestionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetAllSurveyQuestionsListQuery());
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] GetSurveyQuestionsPagedQuery query)
        {
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _mediator.Send(new GetSurveyQuestionByIdQuery { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSurveyQuestionCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateSurveyQuestionCommand command)
        {
            command.Id = id;
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(new DeleteSurveyQuestionCommand { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
