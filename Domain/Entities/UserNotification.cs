using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserNotification
    {


        public Guid UserId { get; set; }

        public Guid NotificationId { get; set; }

        public bool IsRead { get; set; }

        public DateTime? ReadAt { get; set; }

        public ApplicationUser User { get; set; }

        public Notification Notification { get; set; }
    }
}
