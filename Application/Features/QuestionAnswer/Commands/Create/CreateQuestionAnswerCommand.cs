using Application.Common;
using Application.Features.QuestionAnswer.DTOs;
using MediatR;

namespace Application.Features.QuestionAnswer.Commands.Create
{
    public class CreateQuestionAnswerCommand : IRequest<BaseResponse<QuestionAnswerDto>>
    {
        public string CreatedBy { get; set; }
        public string Answer { get; set; }
        public int SurveyQuestionId { get; set; }
        public int SurveyResponseId { get; set; }
    }
}
