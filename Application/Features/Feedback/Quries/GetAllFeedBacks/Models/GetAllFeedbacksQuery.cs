using Application.Common;
using Application.Features.Feedback.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Feedback.Quries.GetAllFeedBacks.Models
{
    public class GetAllFeedbacksQuery : IRequest<BaseResponse<List<FeedbackDTOQuery>>>
    {
        public string? Search { get; set; } = string.Empty;
        public bool seen { get; set; } = false;
    }
}
