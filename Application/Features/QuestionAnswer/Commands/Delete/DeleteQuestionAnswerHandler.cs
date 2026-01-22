using Application.Common;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.QuestionAnswer.Commands.Delete
{
    public class DeleteQuestionAnswerHandler : IRequestHandler<DeleteQuestionAnswerCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteQuestionAnswerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteQuestionAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = await _unitOfWork.ISurveyAnswer.GetByPkAsync(request.Id);
            if (answer == null)
            {
                return BaseResponse<bool>.NotFoundResponse("QuestionAnswer not found");
            }

            _unitOfWork.ISurveyAnswer.Delete(answer);
            await _unitOfWork.Complete();

            return BaseResponse<bool>.SuccessResponse(true, "Deleted successfully");
        }
    }
}
