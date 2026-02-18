using Application.Common;
using Application.Features.Room.DTOs;
using MediatR;

namespace Application.Features.Room.Commands.Create
{
    public class CreateRoomCommand : IRequest<BaseResponse<RoomDto>>
    {
        public string CreatedBy { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public int? BuildId { get; set; }
        public bool? HaveProjector { get; set; }
        public string AttRoomId{  get; set; }
        
    }
}
