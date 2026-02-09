using Application.Common;
using Application.Features.Session.DTOs;
using MediatR;
using System;

namespace Application.Features.Session.Commands.Update
{
    public class UpdateSessionCommand : IRequest<BaseResponse<SessionDto>>
    {
        public int Id { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime SessionDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Topic { get; set; }
        public int RoomId { get; set; }
        public int CourseId { get; set; }
        public Guid LecturerId { get; set; }
    }
}
