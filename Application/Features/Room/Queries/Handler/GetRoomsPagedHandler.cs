using Application.Common;
using Application.Features.Room.DTOs;
using Application.Features.Room.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            Expression<Func<Domain.Entities.Room, bool>> filter = r => !r.IsDeleted;
            // 1️⃣ Guard
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            { 

            // 2️⃣ Base filter (string)
            filter =
                r => (r.Name.Contains(request.SearchTerm)
                  || r.Location.Contains(request.SearchTerm)&& !r.IsDeleted);

            // 3️⃣ int search (Capacity)
            if (int.TryParse(request.SearchTerm, out int capacity))
            {
                filter = filter.Or(r => r.Capacity == capacity && !r.IsDeleted);
            }

            // 4️⃣ bool? search (HaveProjector)
            if (bool.TryParse(request.SearchTerm, out bool haveProjector))
            {
                filter = filter.Or(r =>
                    r.HaveProjector.HasValue &&
                    r.HaveProjector == haveProjector && !r.IsDeleted
                );
            }
            }

            var (items, totalCount) = await _unitOfWork.IRooms.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                filter,
                r => r.Id,
                true,
                r => r.Building,
                r => r.Sessions);

            if (items == null || !items.Any() || items.All(r => r.IsDeleted))
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
                AttRoomIdOutSide = r.AttRoomIdOutSide,
                AttRoomIdinside = r.AttRoomIdinside,
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

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second)
    {
        var parameter = Expression.Parameter(typeof(T));

        var left = Expression.Invoke(first, parameter);
        var right = Expression.Invoke(second, parameter);

        return Expression.Lambda<Func<T, bool>>(
            Expression.OrElse(left, right),
            parameter
        );
    }
}
}
