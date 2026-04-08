using Application.Common;
using Domain.Entities;
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
            if (training == null|training.IsDeleted)
            {
                return BaseResponse<bool>.NotFoundResponse("Training not found");
            }
            var sessions =await  _unitOfWork.ISession.FindRowAsync(s => s.TrainingId == request.Id);
            _unitOfWork.ISession.DeleteRange(sessions);
            _unitOfWork.ITraining.Delete(training);
            var userTrainging = await _unitOfWork.IUserTrainingRepository.FindRowAsync(us => us.TrainingId == request.Id);
            _unitOfWork.IUserTrainingRepository.DeleteRange(userTrainging);
            await _unitOfWork.Complete();

            return BaseResponse<bool>.SuccessResponse(true, "Deleted successfully");
        }
    }
}
