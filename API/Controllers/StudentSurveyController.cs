using Application.Features.Survey.Commands;
using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student,Instructor")]
    public class StudentSurveyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentSurveyController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetMySurveys()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var query = new GetStudentSurveysQuery { UserId = (user.Id) };
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingSurveys([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var query = new GetPendingStudentSurveysQuery 
            { 
                UserId = user.Id,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Search = search
            };
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("completed")]
        public async Task<IActionResult> GetCompletedSurveys([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var query = new GetCompletedStudentSurveysQuery 
            { 
                UserId = user.Id,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Search = search
            };
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{surveyId:int}")]
        public async Task<IActionResult> GetSurveyForResponse(int surveyId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var query = new GetSurveyForResponseQuery 
            { 
                SurveyId = surveyId, 
                UserId = (user.Id) 
            };
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitResponse([FromBody] SubmitSurveyResponseCommand command)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            command.UserId = (user.Id);
            var response = await _mediator.Send(command);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
