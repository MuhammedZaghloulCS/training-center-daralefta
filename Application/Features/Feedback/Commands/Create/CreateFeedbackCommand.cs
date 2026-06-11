using Application.Common;
using Application.Features.Feedback.DTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Feedback.Commands.Create
{
    public class CreateFeedbackCommand : IRequest<BaseResponse<FeedbackDto>>
    {

        public Guid? UserId { get; set; }
        public string Title { get; set; } 
        public string? Description { get; set; } = string.Empty;
        public bool IsAnonymous { get; set; } = false;

 
   




    }
}
