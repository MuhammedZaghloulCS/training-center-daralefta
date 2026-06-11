using Application.Common;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Mapster;
using Infrastructure.Implementations.Repository;
using Application.Features.Feedback.DTOs;
namespace Application.Features.Feedback.Commands.Create
{
    public class CreateFeedbackhandler : IRequestHandler<CreateFeedbackCommand, BaseResponse<FeedbackDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateFeedbackhandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork= unitOfWork;
            _userManager = userManager;
        }

        public async Task<BaseResponse<FeedbackDto>> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                errors.Add("الأسم مطلوب");
            }
            if (request.UserId == Guid.Empty|| request.UserId==null)
            {
                errors.Add("معرف المستخدم مطلوب");
            }
            if(request.Description != null && request.Description.Length > 1001)
            {
                errors.Add("الوصف لا يجب أن يتجاوز 1000 حرف");
            }
            if(errors.Count > 0)
            {
                return BaseResponse<FeedbackDto>.FailureResponse("فشل في إنشاء الملاحظات", errors);
            }
            var userFullName=await _userManager.Users.Where(u => u.Id == request.UserId).Select(u => u.FullName).FirstOrDefaultAsync();
            var requestFeedback = new Domain.Entities.Feedback
            {
                UserId = request?.UserId,
                Title = request.Title,
                Description = request.Description,
                IsAnonymous = request.IsAnonymous,
                IsDeleted = false,
                CreatedBy = userFullName??"System",
                CreatedDate = DateTime.Now
            };
            await _unitOfWork.IFeedback.AddAsync(requestFeedback);
            await _unitOfWork.Complete();
            var dto = requestFeedback.Adapt<FeedbackDto>();
            return BaseResponse<FeedbackDto>.SuccessResponse(dto, "تم إنشاء الملاحظات بنجاح");

        }
    }
    
}
