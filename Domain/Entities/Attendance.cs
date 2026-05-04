using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Attendance : BaseClass
    {
        [Required]
        public Guid UserId { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [Required]
        public int SessionId { get; set; }
        
        [ForeignKey(nameof(SessionId))]
        public Session Session { get; set; }

        /// <summary>
        /// وقت أول دخول للغرفة خلال السيشن
        /// </summary>
        public DateTime? FirstEntryTime { get; set; }

        /// <summary>
        /// وقت آخر خروج من الغرفة خلال السيشن
        /// </summary>
        public DateTime? LastExitTime { get; set; }

        /// <summary>
        /// هل المستخدم حاضر؟
        /// </summary>
        public bool IsPresent { get; set; } = false;

        /// <summary>
        /// ملاحظات إضافية
        /// </summary>
        public string? Notes { get; set; }
    }
}
