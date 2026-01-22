using Application.Common;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Training.Commands.Delete
{
    public class DeleteTrainingHandler : IRequestHandler<DeleteTrainingCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTrainingHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteTrainingCommand request, CancellationToken cancellationToken)
        {
            var training = await _unitOfWork.ITraining.GetByPkAsync(request.Id);
            if (training == null)
            {
                return BaseResponse<bool>.NotFoundResponse("Training not found");
            }

            _unitOfWork.ITraining.Delete(training);
            await _unitOfWork.Complete();

            return BaseResponse<bool>.SuccessResponse(true, "Deleted successfully");
        }
    }
}
