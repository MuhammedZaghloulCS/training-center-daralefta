using Application.Common;
using Application.Features.SurveyQuestion.DTOs;
using Domain.Enums;
using MediatR;

namespace Application.Features.SurveyQuestion.Commands.Create
{
    public class CreateSurveyQuestionCommand : IRequest<BaseResponse<SurveyQuestionDto>>
    {
        public string CreatedBy { get; set; }
        public string QuestionText { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public string? Hint { get; set; }
        public bool Active { get; set; }
        public int SurveyId { get; set; }
    }
}
