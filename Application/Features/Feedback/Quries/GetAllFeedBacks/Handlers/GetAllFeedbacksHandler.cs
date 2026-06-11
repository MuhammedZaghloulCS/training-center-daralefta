using Application.Common;
using Application.Features.Feedback.DTOs;
using Application.Features.Feedback.Quries.GetAllFeedBacks.Models;
using Application.Features.User.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Features.Feedback.Quries.GetAllFeedBacks.Handlers
{
    public class GetAllFeedbacksHandler : IRequestHandler<GetAllFeedbacksQuery, BaseResponse<List<FeedbackDTOQuery>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllFeedbacksHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<List<FeedbackDTOQuery>>> Handle(
        GetAllFeedbacksQuery request,
        CancellationToken cancellationToken)
        {
            Expression<Func<Domain.Entities.Feedback, bool>> filter = f =>
    (string.IsNullOrEmpty(request.Search)
        || f.Title.Contains(request.Search)
        || f.Description.Contains(request.Search))
    &&
    (!request.seen || f.seen);
            var feedbacks = await _unitOfWork.IFeedback
                .FindRowAsync(predicate:filter ,includeProperties: f => f.User);

            if (feedbacks == null || !feedbacks.Any())
            {
                return BaseResponse<List<FeedbackDTOQuery>>
                    .SuccessResponse(new List<FeedbackDTOQuery>(),
                        "لا يوجد مقترحات أو شكاوي");
            }

            var dtos = feedbacks.Select(feedback => new FeedbackDTOQuery
            {
                Id = feedback.Id,
                Title = feedback.Title,
                Description = feedback.Description,
                CreatedBy = feedback.IsAnonymous?"": feedback.CreatedBy,
                CreatedDate = feedback.CreatedDate,
                IsAnonymous = feedback.IsAnonymous,
                UpdatedAt = feedback.UpdatedAt,
                UpdatedBy = feedback.IsAnonymous ? "" : feedback.UpdatedBy,
                User = feedback.IsAnonymous
                    ? null
                    : feedback.User?.Adapt<UserDTO>()
            }).ToList();

            return BaseResponse<List<FeedbackDTOQuery>>
                .SuccessResponse(dtos,
                    "تم جلب المقترحات والشكاوي بنجاح");
        }
    }
}
