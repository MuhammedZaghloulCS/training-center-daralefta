using Application.Common;
using Application.Features.Room.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Room.Queries.Model
{
    public class GetAllRoomsListQuery : IRequest<BaseResponse<List<RoomListDTO>>>
    {
    }
}
