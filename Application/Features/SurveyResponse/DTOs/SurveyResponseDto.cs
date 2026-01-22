using System;

namespace Application.Features.SurveyResponse.DTOs
{
    public class SurveyResponseDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public int SurveyId { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
