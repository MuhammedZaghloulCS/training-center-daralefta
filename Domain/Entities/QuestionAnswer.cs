using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class QuestionAnswer : BaseClass
    {
        public string Answer { get; set; }

        public int questionId { get; set; }

        [ForeignKey(nameof(questionId))]
        public Question Question { get; set; }

        public int SurveyResponseId { get; set; }
       

        [ForeignKey(nameof(SurveyResponseId))]
        public SurveyResponse SurveyResponse { get; set; }
    }

}
