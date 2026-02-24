using Application.Common;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SurveyResponse.Commands.Delete
{
    public class DeleteSurveyResponseHandler : IRequestHandler<DeleteSurveyResponseCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSurveyResponseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteSurveyResponseCommand request, CancellationToken cancellationToken)
        {
            var surveyResponse = await _unitOfWork.ISurveyResponse.GetByPkAsync(request.Id);
            if (surveyResponse == null|| surveyResponse.IsDeleted)
            {
                return BaseResponse<bool>.NotFoundResponse("SurveyResponse not found");
            }
            surveyResponse.IsDeleted = true;
            _unitOfWork.ISurveyResponse.Update(surveyResponse);
            await _unitOfWork.Complete();

            return BaseResponse<bool>.SuccessResponse(true, "Deleted successfully");
        }
    }
}
