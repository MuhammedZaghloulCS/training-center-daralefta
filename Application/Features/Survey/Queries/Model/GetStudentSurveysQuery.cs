using Application.Common;
using Application.Features.Survey.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Survey.Queries.Model
{
    public class GetStudentSurveysQuery : IRequest<BaseResponse<List<StudentSurveyDto>>>
    {
        public Guid UserId { get; set; }
    }
}
