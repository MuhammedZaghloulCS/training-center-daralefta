using Application.Common;
using Application.Features.Survey.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Survey.Queries.Model
{
    public class GetCompletedStudentSurveysQuery : IRequest<BaseResponse<List<StudentSurveyDto>>>
    {
        public Guid UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
    }
}
