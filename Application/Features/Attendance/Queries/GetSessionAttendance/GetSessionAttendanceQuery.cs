using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using MediatR;

namespace Application.Features.Attendance.Queries.GetSessionAttendance
{
    public class GetSessionAttendanceQuery : IRequest<SessionAttendanceResponse>
    {
        public int SessionId { get; set; }
    }
}
