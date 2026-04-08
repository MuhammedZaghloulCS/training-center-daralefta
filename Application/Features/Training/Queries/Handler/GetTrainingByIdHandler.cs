using Application.Common;
using Application.Features.Training.DTOs;
using Application.Features.Training.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Training.Queries.Handler
{
    public class GetTrainingByIdHandler : IRequestHandler<GetTrainingByIdQuery, BaseResponse<TrainingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTrainingByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<TrainingDto>> Handle(GetTrainingByIdQuery request, CancellationToken cancellationToken)
        {
            var trainings = await _unitOfWork.ITraining.FindRowAsync(t => t.Id == request.Id, t => t.UsersTrainings, t => t.Surveys);
            var training = trainings.FirstOrDefault();

            if (training == null||training.IsDeleted)
            {
                return BaseResponse<TrainingDto>.NotFoundResponse("Training not found");
            }

            var dto = new TrainingDto
            {
                Id = training.Id,
                CreatedBy = training.CreatedBy,
                CreatedDate = training.CreatedDate,
                UpdatedBy = training.UpdatedBy,
                UpdatedAt = training.UpdatedAt,
                Title = training.Title,
                StartDate = training.StartDate,
                EndDate = training.EndDate,
            };

            return BaseResponse<TrainingDto>.SuccessResponse(dto, "Training retrieved successfully");
        }
    }
}
