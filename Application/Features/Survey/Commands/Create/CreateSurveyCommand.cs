using Application.Common;
using Application.Features.Survey.DTOs;
using Domain.Entities.Models;
using MediatR;
using System;

namespace Application.Features.Survey.Commands.Create
{
    public class CreateSurveyCommand : IRequest<BaseResponse<SurveyDto>>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public List<QuestionDto>? Questions { get; set; }

    }
}
