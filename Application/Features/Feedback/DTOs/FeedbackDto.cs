using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Feedback.DTOs
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsAnonymous { get; set; }
        public bool seen { get; set; }

        // Audit
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
 
    }
}
