using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsAnonymous { get; set; }
        public bool seen { get; set; }=false;


        // Audit
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation Properties
        public ApplicationUser User { get; set; }
 
    }
}
