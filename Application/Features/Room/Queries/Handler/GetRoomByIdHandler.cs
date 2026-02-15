using Application.Common;
using Application.Features.Room.DTOs;
using Application.Features.Room.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Room.Queries.Handler
{
    public class GetRoomByIdHandler : IRequestHandler<GetRoomByIdQuery, BaseResponse<RoomDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRoomByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<RoomDto>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _unitOfWork.IRooms.FindRowAsync(r => r.Id == request.Id, r => r.Building, r => r.Sessions);
            var room = rooms.FirstOrDefault();

            if (room == null)
            {
                return BaseResponse<RoomDto>.NotFoundResponse("Room not found");
            }

            var dto = new RoomDto
            {
                Id = room.Id,
                CreatedBy = room.CreatedBy,
                CreatedDate = room.CreatedDate,
                UpdatedBy = room.UpdatedBy,
                UpdatedAt = room.UpdatedAt,
                Name = room.Name,
                Capacity = room.Capacity,
                Location = room.Location,
                AttRoomId = room.AttRoomId,
                BuildId = room.BuildId,
                HaveProjector = room.HaveProjector
            };

            return BaseResponse<RoomDto>.SuccessResponse(dto, "Room retrieved successfully");
        }
    }
}
