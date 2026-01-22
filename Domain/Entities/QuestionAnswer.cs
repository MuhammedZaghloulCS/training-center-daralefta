using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class QuestionAnswer : BaseClass
    {
        public string Answer { get; set; }

        public int SurveyQuestionId { get; set; }

        [ForeignKey(nameof(SurveyQuestionId))]
        public SurveyQuestion SurveyQuestion { get; set; }

        public int SurveyResponseId { get; set; }
       

        [ForeignKey(nameof(SurveyResponseId))]
        public SurveyResponse SurveyResponse { get; set; }
    }

}
