

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Session:BaseClass
    {
        public DateTime SessionDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Topic { get; set; }

        public int RoomId { get; set; }
        [ForeignKey(nameof(RoomId))]
        public Room Room { get; set; }


        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }
    }
}
