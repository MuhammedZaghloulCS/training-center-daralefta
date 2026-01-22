using Application.Common;
using Application.Features.Room.DTOs;
using MediatR;

namespace Application.Features.Room.Queries.Model
{
    public class GetRoomByIdQuery : IRequest<BaseResponse<RoomDto>>
    {
        public int Id { get; set; }
    }
}
