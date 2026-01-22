using Application.Common;
using Application.Features.Training.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Training.Commands.Create
{
    public class CreateTrainingHandler : IRequestHandler<CreateTrainingCommand, BaseResponse<TrainingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTrainingHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<TrainingDto>> Handle(CreateTrainingCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.CreatedBy))
                errors.Add("CreatedBy is required");

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

            var training = new Domain.Entities.Training
            {
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                Title = request.Title,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            await _unitOfWork.ITraining.AddAsync(training);
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

            return BaseResponse<TrainingDto>.SuccessResponse(dto, "Training created successfully");
        }
    }
}
