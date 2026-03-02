using Application.Common;
using Application.Features.Session.DTOs;
using Application.Features.Session.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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


            Expression<Func<Domain.Entities.Session, bool>> searchPredicate = null;

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                // جرّب parse من غير ما يطلع exception
                if (int.TryParse(search, out var id))
                {
                    // لو رقم → ابحث بالـ ID
                    searchPredicate = s => s.Id == id;
                }
                else
                {
                    // لو نص → ابحث نصي
                    searchPredicate = s =>
                        s.Topic.Contains(search) ||
                        s.Room.Name.Contains(search) ||
                        s.Course.Name.Contains(search);
                }
            }

            var (items, totalCount) = await _unitOfWork.ISession.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                searchPredicate,
                s => s.Id,
                true,
                s => s.Room,
                s => s.Course,
                s=>s.UserSessions);

            if (items == null || !items.Any()||items.All(i=>i.IsDeleted))
            {
                return BaseResponse<List<SessionListDTO>>.SuccessResponse(
                    new List<SessionListDTO>(),
                    request.PageNumber,
                    request.PageSize,
                    totalCount,
                    "No session found");
            }

            var data = items.Where(r => !r.IsDeleted).Select(s => new SessionListDTO
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
                CourseId = s.CourseId,
                LecturerId = s.lecturerId,
                UsersIds = s.UserSessions.Select(us => us.UserId).ToList()
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
