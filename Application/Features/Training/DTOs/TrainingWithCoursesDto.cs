using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Training.DTOs
{
    public class TrainingWithCoursesDto
    {
      
            public int Id { get; set; }
            public string Title { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public List<Application.Features.Course.DTOs.CourseIdWithNameDto> Courses { get; set; }

        
    }

}
