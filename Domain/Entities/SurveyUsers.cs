using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class SurveyUsers
    {
        public Guid UserId { get; set; }
        public int SurveyId { get; set; }
        public bool HasCompleted { get; set; }=false;
        public DateTime? CompletedAt { get; set; }
        public Survey Survey { get; set; }
        public ApplicationUser User { get; set; }
      
    }
}
