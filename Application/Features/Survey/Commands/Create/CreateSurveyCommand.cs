using Application.Common;
using Application.Features.Survey.DTOs;
using MediatR;
using System;

namespace Application.Features.Survey.Commands.Create
{
    public class CreateSurveyCommand : IRequest<BaseResponse<SurveyDto>>
    {
        public string CreatedBy { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CreatedByUserId { get; set; }
        public int TrainingId { get; set; }
        public int SurveyCategoryId { get; set; }
    }
}
