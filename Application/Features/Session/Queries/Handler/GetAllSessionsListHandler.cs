using Application.Common;
using Application.Features.Session.DTOs;
using Application.Features.Session.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Session.Queries.Handler
{
    public class GetAllSessionsListHandler : IRequestHandler<GetAllSessionsListQuery, BaseResponse<List<SessionListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSessionsListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<SessionListDTO>>> Handle(GetAllSessionsListQuery request, CancellationToken cancellationToken)
        {
        
            var response = await _unitOfWork.ISession.GetAllAsync(s => s.Room, s => s.Course,s=>s.Lecturer);

            if (response == null || !response.Any()||response.All(r=>r.IsDeleted))
            {
                return BaseResponse<List<SessionListDTO>>.SuccessResponse(
                    new List<SessionListDTO>(),
                    "No session found"
                );
            }

            var data = response.Where(r=>!r.IsDeleted).Select(s => new SessionListDTO
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
                LecturerId=s.lecturerId
            }).ToList();

            return BaseResponse<List<SessionListDTO>>.SuccessResponse(
                data,
                "Sessions retrieved successfully"
            );
        }
    }
}
