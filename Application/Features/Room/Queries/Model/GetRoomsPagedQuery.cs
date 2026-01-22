using Application.Common;
using Application.Features.Room.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Room.Queries.Model
{
    public class GetRoomsPagedQuery : IRequest<BaseResponse<List<RoomListDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
