using Application.Features.Room.Commands.Create;
using Application.Features.Room.Commands.Delete;
using Application.Features.Room.Commands.Update;
using Application.Features.Room.Queries.Model;
using Azure;
using Domain.Entities;
using Domain.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class RoomController : ControllerBase
    {
        IMediator _mediator;
        UserManager<ApplicationUser> _userManager;
        HttpClient HttpClient;

        public RoomController(IMediator mediator, IHttpClientFactory httpClientFactory, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            HttpClient = httpClientFactory.CreateClient("ExternalApi");
       
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetAllRoomsListQuery());
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] GetRoomsPagedQuery query)
        {
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _mediator.Send(new GetRoomByIdQuery { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRoomCommand command)
        {
            var user = await _userManager.GetUserAsync(User);
            command.CreatedBy = user?.FullName;
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateRoomCommand command)
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
            var response = await _mediator.Send(new DeleteRoomCommand { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("attrooms")]
        public async Task<IActionResult> GetPagedSystemizedRooms()
        {
            var door =await HttpClient.GetAsync(MainConstants.Use("door/list","pageNo=1&pageSize=1000"));
            door.EnsureSuccessStatusCode();
            var result=await door.Content.ReadAsStringAsync();
            return Ok(result );

        }

    }
}
