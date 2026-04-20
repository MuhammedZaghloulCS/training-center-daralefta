using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }

        public string QuestionText { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }

        public string? Options { get; set; } // JSON
        public bool IsRequired { get; set; }
        public int SortOrder { get; set; }

        public DateTime CreatedAt { get; set; }

        public Survey Survey { get; set; }
    }
}
