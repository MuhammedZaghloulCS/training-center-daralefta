using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class SurveyAnswers:BaseClass
    {
        public string answer { get; set; }
        public int QuestionId {  get; set; }
        [ForeignKey(nameof(QuestionId))]
        public SurveyQuestion SurveyQuestion { get; set; }
        public Guid UserId {  get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
