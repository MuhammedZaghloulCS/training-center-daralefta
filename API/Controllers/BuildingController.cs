using Application.Features.Building.Commands.Create;
using Application.Features.Building.Commands.Delete;
using Application.Features.Building.Commands.Update;
using Application.Features.Building.Queries.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        IMediator _mediator;
        public BuildingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetAllBuildingsListQuery());
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] GetBuildingsPagedQuery query)
        {
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _mediator.Send(new GetBuildingByIdQuery { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBuildingCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateBuildingCommand command)
        {
            command.Id = id;
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _mediator.Send(new DeleteBuildingCommand { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
