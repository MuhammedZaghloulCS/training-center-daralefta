using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.DTOs
{
    public class UsersCourseDTO
    {
        public List<Guid> UserIds { get; set; }
        public int CourseId { get; set; }
    }
}
