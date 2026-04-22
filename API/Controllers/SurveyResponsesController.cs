using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SurveyResponsesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SurveyResponsesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("trainings")]
        public async Task<IActionResult> GetTrainingsWithSurveys(
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 10, 
            [FromQuery] string? search = null)
        {
            var query = new GetTrainingsWithSurveysQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Search = search
            };
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("training/{trainingId}/surveys")]
        public async Task<IActionResult> GetSurveysForTraining(
            int trainingId,
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 10, 
            [FromQuery] string? search = null)
        {
            var query = new GetSurveysForTrainingQuery
            {
                TrainingId = trainingId,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Search = search
            };
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("survey/{surveyId}/responses")]
        public async Task<IActionResult> GetSurveyResponses(
            int surveyId,
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 10, 
            [FromQuery] string? search = null)
        {
            var query = new GetSurveyResponsesQuery
            {
                SurveyId = surveyId,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Search = search
            };
            var response = await _mediator.Send(query);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
