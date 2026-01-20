using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class SurveyResponse :BaseClass
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]

        public ApplicationUser User { get; set; }

        public int SurveyId { get; set; }
        [ForeignKey(nameof(SurveyId))]

        public Survey Survey { get; set; }
    }
}
