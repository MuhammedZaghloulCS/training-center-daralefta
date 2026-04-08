using Application.Common;
using Application.Features.Training.DTOs;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Training.Commands.Update.Trainees
{
    internal class UpdateTrainingTraineesHandler : IRequestHandler<UpdateTrainingTraineesCommand, BaseResponse<TrainingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTrainingTraineesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<TrainingDto>> Handle(UpdateTrainingTraineesCommand request, CancellationToken cancellationToken)
        {
            var training = await _unitOfWork.ITraining.GetTrainingWithUsersTrainingsById(request.Id);
            if (training == null || training.IsDeleted)
            {
                return BaseResponse<TrainingDto>.NotFoundResponse("التدريب غير موجود");
            }

            // 1. امسح القديم
            training.UsersTrainings.Clear();

            // مهم جداً تحفظ قبل الإضافة عشان تتفادى duplicate key
            await _unitOfWork.Complete();


            // 2. ضيف الجديد
            training.UsersTrainings = request.UserIds.Select(userId => new UsersTrainings
            {
                UserId = userId,
                TrainingId = training.Id
            }).ToList();


            // 3. احفظ
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
                EndDate = training.EndDate,


            };

            return BaseResponse<TrainingDto>.SuccessResponse(dto, "Training updated successfully");
        }
    }
}