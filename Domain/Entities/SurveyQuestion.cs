using Domain.Entities;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    public class SurveyQuestion :BaseClass
    {
        [Required]
        [MaxLength(500)]
        public string QuestionText { get; set; }

  
        public QuestionTypeEnum QuestionType { get; set; } 

        // Optional default value or hint
        [MaxLength(300)]
        public string Hint { get; set; }
        public string answer { get; set; }



   
        public int surveyId { get; set; }

        [ForeignKey(nameof(surveyId))]
        public  Survey Survey { get; set; }
        public bool  Active { get; set; }
    }
}
