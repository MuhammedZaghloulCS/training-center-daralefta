using Application.Common;
using Application.Features.QuestionAnswer.DTOs;
using MediatR;

namespace Application.Features.QuestionAnswer.Commands.Update
{
    public class UpdateQuestionAnswerCommand : IRequest<BaseResponse<QuestionAnswerDto>>
    {
        public int Id { get; set; }
        public string? UpdatedBy { get; set; }
        public string Answer { get; set; }
        public int SurveyQuestionId { get; set; }
        public int SurveyResponseId { get; set; }
    }
}
