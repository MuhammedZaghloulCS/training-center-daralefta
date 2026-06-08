using Application.Common;
using Application.Features.Survey.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Survey.Queries.Model
{
    public class GetAllSurveysForUserByIdQuery : IRequest<BaseResponse<List<SurveyDto>>>
    {
        public Guid UserId { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string Search { get; set; } = string.Empty;
    }
}
