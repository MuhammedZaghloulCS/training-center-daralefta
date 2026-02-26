using Application.Common;
using Application.Features.Room.DTOs;
using Application.Features.Room.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Room.Queries.Handler
{
    public class GetAllRoomsListHandler : IRequestHandler<GetAllRoomsListQuery, BaseResponse<List<RoomListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllRoomsListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<RoomListDTO>>> Handle(GetAllRoomsListQuery request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.IRooms.GetAllAsync(r => r.Building, r => r.Sessions);

            if (response == null || !response.Any() || response.All(r => r.IsDeleted))
            {
                return BaseResponse<List<RoomListDTO>>.SuccessResponse(
                    new List<RoomListDTO>(),
                    "No room found"
                );
            }

            var data = response.Where(r=>!r.IsDeleted).Select(r => new RoomListDTO
            {
                Id = r.Id,
                CreatedBy = r.CreatedBy,
                CreatedDate = r.CreatedDate,
                UpdatedBy = r.UpdatedBy,
                UpdatedAt = r.UpdatedAt,
                Name = r.Name,
                Capacity = r.Capacity,
                Location = r.Location,
                AttRoomIdOutSide = r.AttRoomIdOutSide,
                AttRoomIdinside = r.AttRoomIdinside,
                BuildId = r.BuildId,
                HaveProjector = r.HaveProjector
            }).ToList();

            return BaseResponse<List<RoomListDTO>>.SuccessResponse(
                data,
                "Rooms retrieved successfully"
            );
        }
    }
}
