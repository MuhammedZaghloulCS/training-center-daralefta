using Application.Common;
using MediatR;

namespace Application.Features.SurveyQuestion.Commands.Delete
{
    public class DeleteSurveyQuestionCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
}
