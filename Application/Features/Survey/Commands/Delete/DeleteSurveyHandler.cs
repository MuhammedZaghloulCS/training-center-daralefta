using Application.Common;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Survey.Commands.Delete
{
    public class DeleteSurveyHandler : IRequestHandler<DeleteSurveyCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSurveyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteSurveyCommand request, CancellationToken cancellationToken)
        {
            var survey = await _unitOfWork.ISurvey.GetByPkAsync(request.Id);
            if (survey == null)
            {
                return BaseResponse<bool>.NotFoundResponse("Survey not found");
            }

            _unitOfWork.ISurvey.Delete(survey);
            await _unitOfWork.Complete();

            return BaseResponse<bool>.SuccessResponse(true, "Deleted successfully");
        }
    }
}
