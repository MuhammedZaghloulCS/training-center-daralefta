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
        
            Expression<Func<Domain.Entities.Room, bool>> filter;
            var term = request.SearchTerm?.Trim();

            bool isInt = int.TryParse(term, out int capacity);
            bool isBool = bool.TryParse(term, out bool haveProjector);

            if (string.IsNullOrWhiteSpace(term))
            {
                filter = r => !r.IsDeleted;
            }
            else if (isInt)
            {
                filter = r => !r.IsDeleted && r.Capacity == capacity;
            }
            else if (isBool)
            {
                filter = r => !r.IsDeleted &&
                    r.HaveProjector.HasValue &&
                    r.HaveProjector == haveProjector;
            }
            else
            {
                filter = r =>
                    !r.IsDeleted &&
                    (
                        r.Name.Contains(term) ||
                        r.Location.Contains(term)
                    );
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
            var all = await _unitOfWork.IRooms.GetAllAsync() ;
            totalCount= all.Count(r => !r.IsDeleted);
            return BaseResponse<List<RoomListDTO>>.SuccessResponse(
                data,
                request.PageNumber,
                request.PageSize,
                data.Count(),
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
