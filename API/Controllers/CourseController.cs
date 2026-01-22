using Application.Features.Course.Commands.Create;
using Application.Features.Course.Commands.Delete;
using Application.Features.Course.Commands.Update;
using Application.Features.Course.Queries.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetAllCoursesListQuery());
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] GetCoursesPagedQuery query)
        {
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _mediator.Send(new GetCourseByIdQuery { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCourseCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCourseCommand command)
        {
            command.Id = id;
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(new DeleteCourseCommand { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
