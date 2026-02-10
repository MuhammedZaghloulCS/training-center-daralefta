using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserSession
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int SessionId { get; set; }   
        public Session Session { get; set; }
    }

}
