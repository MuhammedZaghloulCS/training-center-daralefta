using Application.Common;
using Application.Features.Room.DTOs;
using MediatR;

namespace Application.Features.Room.Commands.Update
{
    public class UpdateRoomCommand : IRequest<BaseResponse<RoomDto>>
    {
        public int Id { get; set; }
        public string? UpdatedBy { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public string AttRoomIdOutSide { get; set; } = string.Empty;
        public string AttRoomIdinside { get; set; } = string.Empty;
        public int? BuildId { get; set; }
        public bool? HaveProjector { get; set; }
    }
}
