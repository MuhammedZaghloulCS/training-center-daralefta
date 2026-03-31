using Application.Features.Dashboard.Query.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator mediator;
        public DashboardController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetCounts()
        {
            var response = await mediator.Send(new GetCountsQuery());
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
