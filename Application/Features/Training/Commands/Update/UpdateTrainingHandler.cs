using Application.Common;
using Application.Features.Training.DTOs;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// Alias for clarity
using CairoTime = Application.Common.DateTimeHelper;

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
            var training = await _unitOfWork.ITraining.GetTrainingWithSessionsById(request.Id,t=>t.Sessions);
            if (training == null||training.IsDeleted)
            {
                return BaseResponse<TrainingDto>.NotFoundResponse("التدريب غير موجود");
            }

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Title))
                errors.Add("اسم التدريب مطلوب");

            if (request.StartDate == default)
                errors.Add("تاريخ البدأ مطلوب");

            if (request.EndDate == default)
                errors.Add("تاريخ الانتهاء مطل");

            if (request.EndDate < request.StartDate)
                errors.Add("يجب أن يكون تاريخ الانتهاء أكبر من أو يساوي تاريخ البدء");

            
            var today = CairoTime.Now.Date;

            if (training.StartDate.Date <= today
                && request.StartDate.Date < training.StartDate.Date)
            {
                errors.Add(
                   "لا يمكن تعديل تاريخ بدء تدريب قد بدأ بالفعل إلى الماضي"
                );
            }
            var datesOfSessions= training.Sessions.Select(s => s.SessionDate.Date).ToList();
            if (datesOfSessions.Any() && request.StartDate.Date > datesOfSessions.Min())
            {
                errors.Add(
                   "لا يمكن تعديل تاريخ بدء التدريب إلى تاريخ بعد مواعيد جلسات التدريب المحددة"
                );
            }
            if (datesOfSessions.Any() && request.EndDate.Date < datesOfSessions.Max())
            {
                errors.Add(
                    "لا يمكن تعديل تاريخ بدء التدريب إلى تاريخ قبل مواعيد جلسات التدريب المحددة"
                );
            }
            if (errors.Any())
                return BaseResponse<TrainingDto>.FailureResponse("خطأ في البيانات المعدلة", errors);
            training.Title = request.Title;
            training.StartDate = request.StartDate;
            training.EndDate = request.EndDate;
            training.UpdatedBy = request.UpdatedBy;
            training.UpdatedAt = CairoTime.Now;
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
