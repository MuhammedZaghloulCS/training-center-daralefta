using Application.Common;
using Application.Features.Feedback.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Feedback.Quries.GetAllFeedbacksPaged.Models
{
    public class GetAllFeedbacksPagedModel : IRequest<BaseResponse<List<FeedbackDTOQuery>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = string.Empty;
        public bool seen { get; set; } = false;
    }
}
