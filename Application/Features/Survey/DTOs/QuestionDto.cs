using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Survey.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }

        public string? Options { get; set; } // JSON
        public bool IsRequired { get; set; }
        public int SortOrder { get; set; }
    }
}
