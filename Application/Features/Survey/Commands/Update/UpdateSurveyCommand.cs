using Application.Common;
using Application.Features.Survey.DTOs;
using MediatR;
using System;

namespace Application.Features.Survey.Commands.Update
{
    public class UpdateSurveyCommand : IRequest<BaseResponse<SurveyDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public List<QuestionDto>? Questions { get; set; }
    }
}
