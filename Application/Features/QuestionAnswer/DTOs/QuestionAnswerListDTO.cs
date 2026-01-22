using System;

namespace Application.Features.QuestionAnswer.DTOs
{
    public class QuestionAnswerListDTO
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Answer { get; set; }
        public int SurveyQuestionId { get; set; }
        public int SurveyResponseId { get; set; }
    }
}
