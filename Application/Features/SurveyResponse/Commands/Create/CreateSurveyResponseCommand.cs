using Application.Common;
using Application.Features.SurveyResponse.DTOs;
using MediatR;
using System;

namespace Application.Features.SurveyResponse.Commands.Create
{
    public class CreateSurveyResponseCommand : IRequest<BaseResponse<SurveyResponseDto>>
    {
        public string CreatedBy { get; set; }
        public Guid UserId { get; set; }
        public int SurveyId { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
