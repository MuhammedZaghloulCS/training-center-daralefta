using Domain.Enums;
using System;

namespace Application.Features.SurveyQuestion.DTOs
{
    public class SurveyQuestionDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string QuestionText { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public string Hint { get; set; }
        public bool Active { get; set; }
        public int SurveyId { get; set; }
    }
}
