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
    public class GetRoomsPagedHandler : IRequestHandler<GetRoomsPagedQuery, BaseResponse<List<RoomListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRoomsPagedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<RoomListDTO>>> Handle(GetRoomsPagedQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<RoomListDTO>>.BadRequestResponse("Invalid pagination parameters");
            }

            var (items, totalCount) = await _unitOfWork.IRooms.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                null,
                r => r.Id,
                true,
                r => r.Building,
                r => r.Sessions);

            if (items == null || !items.Any())
            {
                return BaseResponse<List<RoomListDTO>>.SuccessResponse(
                    new List<RoomListDTO>(),
                    request.PageNumber,
                    request.PageSize,
                    totalCount,
                    "No room found");
            }

            var data = items.Select(r => new RoomListDTO
            {
                Id = r.Id,
                CreatedBy = r.CreatedBy,
                CreatedDate = r.CreatedDate,
                UpdatedBy = r.UpdatedBy,
                UpdatedAt = r.UpdatedAt,
                Name = r.Name,
                Capacity = r.Capacity,
                Location = r.Location,
                AttRoomId = r.AttRoomId,
                BuildId = r.BuildId,
                HaveProjector = r.HaveProjector,
                Building= new Building.DTOs.BuildingDto
                {
                    Id = r.Building.Id,
                    Name = r.Building.Name,
                   
                }
            }).ToList();

            return BaseResponse<List<RoomListDTO>>.SuccessResponse(
                data,
                request.PageNumber,
                request.PageSize,
                totalCount,
                "Rooms retrieved successfully");
        }
    }
}
