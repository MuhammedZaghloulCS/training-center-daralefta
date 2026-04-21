using Domain.Entities;
using System;

namespace Application.Features.Training.DTOs
{
    public class TrainingDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
       
        public List<Domain.Entities.Session>? Sessions { get; set; }
        public ICollection<CoursesTrainings>? CoursesTrainings { get; set; }

        public List<Domain.Entities.UsersTrainings>? UsersTrainings { get; set; }
        public ICollection<TrainingsSurveys>? TrainingsSurveys { get; set; }

    }
}
