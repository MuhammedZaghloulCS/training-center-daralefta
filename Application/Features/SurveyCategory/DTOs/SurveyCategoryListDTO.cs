using System;

namespace Application.Features.SurveyCategory.DTOs
{
    public class SurveyCategoryListDTO
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
