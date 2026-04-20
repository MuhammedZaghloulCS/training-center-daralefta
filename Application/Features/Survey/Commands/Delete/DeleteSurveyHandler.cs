using Application.Common;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Survey.Commands.Delete
{
    public class DeleteSurveyHandler : IRequestHandler<DeleteSurveyCommand, BaseResponse<Domain.Entities.Survey>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSurveyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Domain.Entities.Survey>> Handle(DeleteSurveyCommand request, CancellationToken cancellationToken)
        {

            var survey = await _unitOfWork.ISurvey.GetByPkAsync(request.Id);

            if (survey == null)
            {
                return BaseResponse<Domain.Entities.Survey>.NotFoundResponse("Survey not found");
            }
            _unitOfWork.ISurvey.Delete(survey);
            await _unitOfWork.Complete();
            return BaseResponse<Domain.Entities.Survey>.SuccessResponse(survey, "Deleted successfully");
        }
    }
}
