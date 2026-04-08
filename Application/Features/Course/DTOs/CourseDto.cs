using System;

namespace Application.Features.Course.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Prerequisites { get; set; }
        public int Duration { get; set; }
       
    }
}
