using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class TrainingsSurveys
    {
        public int? trainingId { get; set; }
        public int? surveyId { get; set; }
        [ForeignKey(nameof(trainingId))]
        public Training Training { get; set; }

        [ForeignKey(nameof(surveyId))]
        public Survey Survey { get; set; }
    }
}
