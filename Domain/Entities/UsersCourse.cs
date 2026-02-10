using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UsersCourse
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
