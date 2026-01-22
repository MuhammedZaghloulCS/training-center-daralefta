using Application.Common;
using MediatR;

namespace Application.Features.QuestionAnswer.Commands.Delete
{
    public class DeleteQuestionAnswerCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
}
