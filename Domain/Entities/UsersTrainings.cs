using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UsersTrainings
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int TrainingId { get; set; }
        public Training Training { get; set; }
    }
}
