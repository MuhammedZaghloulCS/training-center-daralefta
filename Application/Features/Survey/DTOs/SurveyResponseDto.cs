using System;
using System.Collections.Generic;

namespace Application.Features.Survey.DTOs
{
    public class SurveyResponseDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SurveyId { get; set; }
        public int? TrainingId { get; set; }
        public DateTime SubmittedAt { get; set; }
        public List<QuestionAnswerDto> Answers { get; set; } = new();
    }

    public class QuestionAnswerDto
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }

    public class StudentSurveyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuestionCount { get; set; }
        public bool HasResponded { get; set; }
        public int? TrainingId { get; set; }
        public string TrainingName { get; set; }
    }

    public class SurveyWithQuestionsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? TrainingId { get; set; }
        public string TrainingName { get; set; }
        public List<QuestionForResponseDto> Questions { get; set; } = new();
    }

    public class QuestionForResponseDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int QuestionType { get; set; }
        public string Options { get; set; }
        public bool IsRequired { get; set; }
        public int SortOrder { get; set; }
    }
}
