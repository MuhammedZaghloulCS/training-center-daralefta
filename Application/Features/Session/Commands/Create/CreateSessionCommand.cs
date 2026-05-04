using Application.Common;
using Application.Features.Session.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.Features.Session.Commands.Create
{
    public class CreateSessionCommand : IRequest<BaseResponse<SessionDto>>
    {
        public string? CreatedBy { get; set; }
        public DateTime SessionDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Topic { get; set; }
        public List<IFormFile>? formFiles { get; set; }
        public int RoomId { get; set; }
        public int CourseId { get; set; }
        public int TrainingId { get; set; }
        public List<Guid> LecturersIds {  get; set; }

    }
}
