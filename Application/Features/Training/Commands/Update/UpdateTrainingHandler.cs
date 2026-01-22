using Application.Common;
using Application.Features.Training.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Training.Commands.Update
{
    public class UpdateTrainingHandler : IRequestHandler<UpdateTrainingCommand, BaseResponse<TrainingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTrainingHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<TrainingDto>> Handle(UpdateTrainingCommand request, CancellationToken cancellationToken)
        {
            var training = await _unitOfWork.ITraining.GetByPkAsync(request.Id);
            if (training == null)
            {
                return BaseResponse<TrainingDto>.NotFoundResponse("Training not found");
            }

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Title))
                errors.Add("Title is required");

            if (request.StartDate == default)
                errors.Add("StartDate is required");

            if (request.EndDate == default)
                errors.Add("EndDate is required");

            if (request.EndDate < request.StartDate)
                errors.Add("EndDate must be greater than or equal to StartDate");

            if (errors.Any())
                return BaseResponse<TrainingDto>.FailureResponse("Validation failed", errors);

            training.Title = request.Title;
            training.StartDate = request.StartDate;
            training.EndDate = request.EndDate;
            training.UpdatedBy = request.UpdatedBy;
            training.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.ITraining.Update(training);
            await _unitOfWork.Complete();

            var dto = new TrainingDto
            {
                Id = training.Id,
                CreatedBy = training.CreatedBy,
                CreatedDate = training.CreatedDate,
                UpdatedBy = training.UpdatedBy,
                UpdatedAt = training.UpdatedAt,
                Title = training.Title,
                StartDate = training.StartDate,
                EndDate = training.EndDate
            };

            return BaseResponse<TrainingDto>.SuccessResponse(dto, "Training updated successfully");
        }
    }
}
