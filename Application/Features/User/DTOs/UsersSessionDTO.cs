using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.DTOs
{
    public class UsersSessionDTO
    {
        public List<Guid> UserIds { get; set; }
        public int sessionId { get; set; }
    }
}
