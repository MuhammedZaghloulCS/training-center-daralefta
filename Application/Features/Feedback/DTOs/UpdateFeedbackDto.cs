using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Feedback.DTOs
{
    public class UpdateFeedbackDto
    {

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsAnonymous { get; set; }


        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
