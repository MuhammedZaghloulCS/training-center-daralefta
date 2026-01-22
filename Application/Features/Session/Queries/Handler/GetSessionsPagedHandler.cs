using Application.Common;
using Application.Features.Session.DTOs;
using Application.Features.Session.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Session.Queries.Handler
{
    public class GetSessionsPagedHandler : IRequestHandler<GetSessionsPagedQuery, BaseResponse<List<SessionListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSessionsPagedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<SessionListDTO>>> Handle(GetSessionsPagedQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<SessionListDTO>>.BadRequestResponse("Invalid pagination parameters");
            }

            var (items, totalCount) = await _unitOfWork.ISession.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                null,
                s => s.Id,
                true,
                s => s.Room,
                s => s.Course);

            if (items == null || !items.Any())
            {
                return BaseResponse<List<SessionListDTO>>.SuccessResponse(
                    new List<SessionListDTO>(),
                    request.PageNumber,
                    request.PageSize,
                    totalCount,
                    "No session found");
            }

            var data = items.Select(s => new SessionListDTO
            {
                Id = s.Id,
                CreatedBy = s.CreatedBy,
                CreatedDate = s.CreatedDate,
                UpdatedBy = s.UpdatedBy,
                UpdatedAt = s.UpdatedAt,
                SessionDate = s.SessionDate,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Topic = s.Topic,
                RoomId = s.RoomId,
                CourseId = s.CourseId
            }).ToList();

            return BaseResponse<List<SessionListDTO>>.SuccessResponse(
                data,
                request.PageNumber,
                request.PageSize,
                totalCount,
                "Sessions retrieved successfully");
        }
    }
}
