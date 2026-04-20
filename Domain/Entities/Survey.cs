using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    public class Survey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<SurveyResponse> SurveyResponses { get; set; }
        public ICollection<TrainingsSurveys> TrainingsSurveys { get; set; }


    }
}
