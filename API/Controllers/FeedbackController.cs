using Application.Features.Feedback.Commands.Create;
using Application.Features.Feedback.Commands.Delete;
using Application.Features.Feedback.Commands.Seen;
using Application.Features.Feedback.Commands.Update;
using Application.Features.Feedback.DTOs;
using Application.Features.Feedback.Quries.GetAllFeedBacks.Models;
using Application.Features.Feedback.Quries.GetAllFeedbacksPaged.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        public FeedbackController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackCommand feedback)
        {
            // Here you would typically save the feedback to a database or send it to a service
            // For demonstration, we'll just return the received feedback
            var user = await _userManager.GetUserAsync(User);
            feedback.UserId = user?.Id;
            var result = await _mediator.Send(feedback);
            if (result.Success)
            {
                return Ok(result);
            }
            
                return BadRequest(result);
            
    }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var res = await _mediator.Send(new DeleteFeedbackCommand { Id = id });
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFeedbackDto dto)
        {
            dto.Id = id;
            var user=await _userManager.GetUserAsync(User);
            dto.UpdatedAt = DateTime.Now;
            dto.UpdatedBy= user.FullName??"system";
            var result = await _mediator.Send(new UpdateFeedbackCommand
            {
                UpdateFeedbackDto = dto
            });

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{id}/seen")]
        public async Task<IActionResult> MarkAsSeen(int id)
        {
            var result = await _mediator.Send(new FeedbackSeenCommand { Id = id });
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllFeedBacks([FromQuery] GetAllFeedbacksQuery search)
        {
            var result = await _mediator.Send(search);
            if (!result.Success)
            {
                return Ok(result);
            }
            return Ok(result);
        }
        [HttpGet("GetPaged")]
        public async Task<IActionResult> GetPaged([FromQuery] GetAllFeedbacksPagedModel query)
        {
            var result = await _mediator.Send(query);
            if (!result.Success)
            {
                return Ok(result);
            }
            return Ok(result);

        }
}
}
