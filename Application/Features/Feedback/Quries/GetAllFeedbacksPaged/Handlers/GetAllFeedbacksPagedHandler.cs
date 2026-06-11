using Application.Common;
using Application.Features.Feedback.DTOs;
using Application.Features.Feedback.Quries.GetAllFeedbacksPaged.Models;
using Application.Features.User.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Features.Feedback.Quries.GetAllFeedbacksPaged.Handlers
{
    public class GetAllFeedbacksPagedHandler : IRequestHandler<GetAllFeedbacksPagedModel, BaseResponse<List<FeedbackDTOQuery>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllFeedbacksPagedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<List<FeedbackDTOQuery>>> Handle(GetAllFeedbacksPagedModel request, CancellationToken cancellationToken)
        {
            if (request.PageNumber <= 0 || request.PageSize <= 0)
            {
                return BaseResponse<List<FeedbackDTOQuery>>.BadRequestResponse("يجب أن يكون رقم الصفحة وحجم الصفحة أكبر من الصفر.");
            }
            Expression<Func<Domain.Entities.Feedback, bool>> filter = f =>
     (string.IsNullOrEmpty(request.Search)
         || f.Title.Contains(request.Search)
         || f.Description.Contains(request.Search))
     &&
     (request.seen ? f.seen : !f.seen);

            var pagedResult = await _unitOfWork.IFeedback.GetPaginatedAsync(
                 pageNumber: request.PageNumber,
                 pageSize: request.PageSize,
                 predicate: filter,
                 orderBy: f => f.CreatedDate,
                 ascending: false,
                 includeProperties:f=>f.User
            );
          

            if (pagedResult.items == null || pagedResult.items.Count == 0)
            {
                return BaseResponse<List<FeedbackDTOQuery>>.SuccessResponse(new List<FeedbackDTOQuery>(), "لا توجد تعليقات.");
            }
            var feedbackDTOs = pagedResult.items.Select(feedback => new FeedbackDTOQuery
            {
                Id = feedback.Id,
                Title = feedback.Title,
                Description = feedback.Description,
                CreatedBy = feedback.IsAnonymous ? "" : feedback.CreatedBy,
                CreatedDate = feedback.CreatedDate,
                IsAnonymous = feedback.IsAnonymous,
                UpdatedAt = feedback.UpdatedAt,
                UpdatedBy = feedback.IsAnonymous ? "" : feedback.UpdatedBy,
                User = feedback.IsAnonymous
        ? null
        : feedback.User?.Adapt<UserDTO>()
            }).ToList();
            return BaseResponse<List<FeedbackDTOQuery>>.SuccessResponse(
                data: feedbackDTOs,
                currentPage: request.PageNumber,
                pageSize: request.PageSize,
                totalItems: pagedResult.totalCount,
                message: "تم جلب التعليقات بنجاح."
            );


        }
    }
}
