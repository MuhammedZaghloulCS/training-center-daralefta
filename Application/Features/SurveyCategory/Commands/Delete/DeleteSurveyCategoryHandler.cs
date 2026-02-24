using Application.Common;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SurveyCategory.Commands.Delete
{
    public class DeleteSurveyCategoryHandler : IRequestHandler<DeleteSurveyCategoryCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSurveyCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteSurveyCategoryCommand request, CancellationToken cancellationToken)
        {
            var surveyCategory = await _unitOfWork.ISurveyCategory.GetByPkAsync(request.Id);
            if (surveyCategory == null|| surveyCategory.IsDeleted)
            {
                return BaseResponse<bool>.NotFoundResponse("SurveyCategory not found");
            }
            surveyCategory.IsDeleted = true;
            _unitOfWork.ISurveyCategory.Update(surveyCategory);
            await _unitOfWork.Complete();

            return BaseResponse<bool>.SuccessResponse(true, "Deleted successfully");
        }
    }
}
