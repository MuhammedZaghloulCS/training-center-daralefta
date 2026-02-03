using System;

namespace Application.Features.Survey.DTOs
{
    public class SurveyListDTO
    {
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public int? TrainingId { get; set; }
        public int? SurveyCategoryId { get; set; }
    }
}
