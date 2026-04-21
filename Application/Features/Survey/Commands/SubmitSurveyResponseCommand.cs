using Application.Common;
using Application.Features.Survey.DTOs;
using MediatR;

namespace Application.Features.Survey.Commands
{
    public class SubmitSurveyResponseCommand : IRequest<BaseResponse<SurveyResponseDto>>
    {
        public Guid UserId { get; set; }
        public int SurveyId { get; set; }
        public int? TrainingId { get; set; }
        public List<QuestionAnswerDto> Answers { get; set; } = new();
    }
}
