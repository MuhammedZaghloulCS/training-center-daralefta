using Application.Common;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SurveyQuestion.Commands.Delete
{
    public class DeleteSurveyQuestionHandler : IRequestHandler<DeleteSurveyQuestionCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSurveyQuestionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteSurveyQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _unitOfWork.ISurveyQuestion.GetByPkAsync(request.Id);
            if (question == null||question.IsDeleted)
            {
                return BaseResponse<bool>.NotFoundResponse("SurveyQuestion not found");
            }
            question.IsDeleted = true;
            _unitOfWork.ISurveyQuestion.Update(question);
            await _unitOfWork.Complete();

            return BaseResponse<bool>.SuccessResponse(true, "Deleted successfully");
        }
    }
}
