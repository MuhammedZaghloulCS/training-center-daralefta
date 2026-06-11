using Application.Features.User.DTOs;
using Domain.Entities;
using System;

namespace Application.Features.Survey.DTOs
{
    public class SurveyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public List<QuestionDto>? Questions { get; set; }
        public bool IsForSpecificUsers { get; set; }
        public List<SimpleUserDto>? SpecificUsers { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
