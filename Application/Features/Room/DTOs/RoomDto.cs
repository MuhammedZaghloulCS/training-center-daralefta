using System;

namespace Application.Features.Room.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public int? BuildId { get; set; }
        public bool? HaveProjector { get; set; }
    }
}
