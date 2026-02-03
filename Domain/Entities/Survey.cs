
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    public class Survey : BaseClass
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        // Creator
        public Guid? CreatedByUserId { get; set; }

        [ForeignKey(nameof(CreatedByUserId))]
        public ApplicationUser CreatedByUser { get; set; }

        public int TrainingId { get; set; }
        [ForeignKey(nameof(TrainingId))]
        public Training Training { get; set; }


        public int? SurveyCategoryId { get; set; }
        [ForeignKey(nameof(SurveyCategoryId))]

        public SurveyCategory SurveyCategory { get; set; }

        public ICollection<SurveyQuestion> SurveyQuestions { get; set; }
        public ICollection<SurveyResponse> SurveyResponses { get; set; }



    }
}
