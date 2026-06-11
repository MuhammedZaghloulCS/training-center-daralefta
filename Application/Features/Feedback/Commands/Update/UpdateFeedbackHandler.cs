using Application.Common;
using Application.Features.Feedback.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;

namespace Application.Features.Feedback.Commands.Update
{
    public class UpdateFeedbackHandler : IRequestHandler<UpdateFeedbackCommand, BaseResponse<FeedbackDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFeedbackHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<FeedbackDto>> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            if (request.UpdateFeedbackDto.Id <= 0)
            {
                errors.Add("معرف غير صحيح");
            }

            var existingFeedback = await _unitOfWork.IFeedback
                .GetByPkAsync(request.UpdateFeedbackDto.Id);

            if (existingFeedback == null)
            {
                return BaseResponse<FeedbackDto>.NotFoundResponse("التعليق غير موجود");
            }

            if (string.IsNullOrWhiteSpace(request.UpdateFeedbackDto.Title))
            {
                errors.Add("العنوان مطلوب");
            }

            if (string.IsNullOrWhiteSpace(request.UpdateFeedbackDto.Description))
            {
                errors.Add("الوصف مطلوب");
            }

            if (errors.Any())
            {
                
                    return BaseResponse<FeedbackDto>.FailureResponse("فشل في تعديل الملاحظة", errors);
                
            }

            existingFeedback.Title = request.UpdateFeedbackDto.Title;
            existingFeedback.Description = request.UpdateFeedbackDto.Description;
            existingFeedback.IsAnonymous = request.UpdateFeedbackDto.IsAnonymous;
            existingFeedback.UpdatedAt = DateTime.Now;
            existingFeedback.UpdatedBy = request.UpdateFeedbackDto.UpdatedBy ?? existingFeedback.UpdatedBy;

            await _unitOfWork.Complete();

            var result = new FeedbackDto
            {
                Id = existingFeedback.Id,
                Title = existingFeedback.Title,
                Description = existingFeedback.Description,
                IsAnonymous = existingFeedback.IsAnonymous,
                UserId = existingFeedback.UserId,
                CreatedBy = existingFeedback.CreatedBy,
                CreatedDate = existingFeedback.CreatedDate,
                UpdatedBy = existingFeedback.UpdatedBy,
                UpdatedAt = existingFeedback.UpdatedAt
            };

            return BaseResponse<FeedbackDto>.SuccessResponse(result, "تم تحديث التعليق بنجاح");
        }
    }
}