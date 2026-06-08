using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }



        public ICollection<UserNotification> UserNotifications { get; set; }
    }
}
