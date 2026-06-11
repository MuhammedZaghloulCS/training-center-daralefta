using Application.Common;
using Application.Features.Feedback.DTOs;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Feedback.Commands.Delete
{
    public class DeleteFeedbackHandler : IRequestHandler<DeleteFeedbackCommand, BaseResponse<FeedbackDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteFeedbackHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<FeedbackDto>> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
        {
           var feedback =await _unitOfWork.IFeedback.GetByPkAsync(request.Id);
            if (feedback == null)
            {
                return BaseResponse<FeedbackDto>.NotFoundResponse("المراجعة غير موجودة");
            }
            feedback.IsDeleted = true;
            await _unitOfWork.Complete();
            var feedbackDto = feedback.Adapt<FeedbackDto>();
            return BaseResponse<FeedbackDto>.SuccessResponse(feedbackDto, "Feedback deleted successfully");
        }
    }
}
