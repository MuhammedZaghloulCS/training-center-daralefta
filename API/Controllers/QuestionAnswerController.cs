using Application.Features.QuestionAnswer.Commands.Create;
using Application.Features.QuestionAnswer.Commands.Delete;
using Application.Features.QuestionAnswer.Commands.Update;
using Application.Features.QuestionAnswer.Queries.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionAnswerController : ControllerBase
    {
        IMediator _mediator;

        public QuestionAnswerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetAllQuestionAnswersListQuery());
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] GetQuestionAnswersPagedQuery query)
        {
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _mediator.Send(new GetQuestionAnswerByIdQuery { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateQuestionAnswerCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateQuestionAnswerCommand command)
        {
            command.Id = id;
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(new DeleteQuestionAnswerCommand { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
