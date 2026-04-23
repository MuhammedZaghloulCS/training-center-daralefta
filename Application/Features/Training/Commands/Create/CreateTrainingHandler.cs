using Application.Common;
using Application.Features.Training.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// Alias for clarity
using CairoTime = Application.Common.DateTimeHelper;

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

            #region Validation
            if (string.IsNullOrWhiteSpace(request.Title))
                errors.Add("اسم التدريب مطلوب");
            if(request.Title.Length>50)
                errors.Add("العنوان يجب أن لا يتعدي ال 50 حرفا");
            if (request.StartDate == default)
                errors.Add("تاريخ البدأ مطلوب");

            if (request.EndDate == default)
                errors.Add("تاريخ الانتهاء مطلوب");

            if (request.EndDate < request.StartDate)
                errors.Add("تاريخ الانتهاء يجب أن يكون بعد تاريخ البدأ");
            var today = DateTime.Now.Date;

            if (request.StartDate.Date < today)
            {
                errors.Add("تاريخ بداية التدريب لا يمكن أن يكون في الماضي");
            }

            if (request.EndDate.Date < today)
            {
                errors.Add("تاريخ انتهاء التدريب لا يمكن أن يكون في الماضي");
            }
            if (errors.Any())
                return BaseResponse<TrainingDto>.FailureResponse("Validation failed", errors);
            #endregion
            
            var training = new Domain.Entities.Training
            {
                CreatedBy = request.CreatedBy,
                CreatedDate = CairoTime.Now,
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
                EndDate = training.EndDate,
                
            };

            return BaseResponse<TrainingDto>.SuccessResponse(dto, "Training created successfully");
        }
    }
}
