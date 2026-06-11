using Application.Common;
using Application.Features.Feedback.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Feedback.Commands.Seen
{
    public class FeedbackSeenCommand : IRequest<BaseResponse<FeedbackDto>>
    {
        public int Id { get; set; }
    }
}
