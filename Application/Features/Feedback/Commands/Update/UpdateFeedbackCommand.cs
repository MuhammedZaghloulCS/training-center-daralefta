using Application.Common;
using Application.Features.Feedback.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Feedback.Commands.Update
{
    public class UpdateFeedbackCommand : IRequest<BaseResponse<FeedbackDto>>
    {
        public UpdateFeedbackDto UpdateFeedbackDto { get; set; }
    }
}
