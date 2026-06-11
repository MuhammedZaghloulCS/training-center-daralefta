using Application.Common;
using Application.Features.Feedback.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;

namespace Application.Features.Feedback.Commands.Seen
{
    public class FeedbackSeebHandler : IRequestHandler<FeedbackSeenCommand, BaseResponse<FeedbackDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackSeebHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<FeedbackDto>> Handle(FeedbackSeenCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            if (request.Id <= 0)
            {
                errors.Add("معرف غير صحيح");
            }

            var existingFeedback = await _unitOfWork.IFeedback
                .GetByPkAsync(request.Id);

            if (existingFeedback == null)
            {
                return BaseResponse<FeedbackDto>.NotFoundResponse("التعليق غير موجود");
            }

            if (errors.Any())
            {
                return BaseResponse<FeedbackDto>.FailureResponse("فشل في تعديل الملاحظة", errors);
            }

            existingFeedback.seen = true;
            existingFeedback.UpdatedAt = DateTime.Now;

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