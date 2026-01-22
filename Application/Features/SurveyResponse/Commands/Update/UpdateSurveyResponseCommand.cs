using Application.Common;
using Application.Features.SurveyResponse.DTOs;
using MediatR;
using System;

namespace Application.Features.SurveyResponse.Commands.Update
{
    public class UpdateSurveyResponseCommand : IRequest<BaseResponse<SurveyResponseDto>>
    {
        public int Id { get; set; }
        public string? UpdatedBy { get; set; }
        public Guid UserId { get; set; }
        public int SurveyId { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
