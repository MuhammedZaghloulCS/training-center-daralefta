using System;

namespace Application.Features.Session.DTOs
{
    public class SessionDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime SessionDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Topic { get; set; }
        public int RoomId { get; set; }
        public int? CourseId { get; set; }
        public Guid? LecturerId { get; set; }
        public List<Guid> UsersIds { get; set; }
    }
}
