using Application.Features.Building.Commands.Create;
using Application.Features.Building.Commands.Delete;
using Application.Features.Building.Commands.Update;
using Application.Features.Building.Queries.Model;
using Domain.Entities;
using Domain.Helper;
using Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class BuildingController : ControllerBase
    {
        IMediator _mediator;
        UserManager<ApplicationUser> _userManager;
        HttpClient HttpClient;
        private readonly ApplicationContext context;

        public BuildingController(IMediator mediator, IHttpClientFactory httpClientFactory, ApplicationContext context, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            HttpClient = httpClientFactory.CreateClient("ExternalApi");
            this.context = context;
            _userManager = userManager;
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
            var user = await _userManager.GetUserAsync(User);
            command.CreatedBy = user?.FullName;
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateBuildingCommand command)
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
            var response = await _mediator.Send(new DeleteBuildingCommand { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }


        [HttpGet("attbuildings")]
        public async Task<IActionResult> GetPagedSystemizedRooms()
        {
            var door = await HttpClient.GetAsync(MainConstants.Use("v2/attAreaPerson/area/list", "pageNo=1&pageSize=1000"));
            door.EnsureSuccessStatusCode();
            var result = await door.Content.ReadAsStringAsync();
            return Ok(result);

        }
        [HttpGet("db-test")]
        public IActionResult TestDb()
        {
            return Ok(new { message = "API IS RUNNING" });

        }
    }
}
