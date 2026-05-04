using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Attendance.Queries.GetSessionAttendance
{
    public class GetSessionAttendanceHandler : IRequestHandler<GetSessionAttendanceQuery, SessionAttendanceResponse>
    {
        private readonly ISysUnitOfWork _sysUnitOfWork;

        public GetSessionAttendanceHandler(ISysUnitOfWork sysUnitOfWork)
        {
            _sysUnitOfWork = sysUnitOfWork;
        }

        public async Task<SessionAttendanceResponse> Handle(GetSessionAttendanceQuery request, CancellationToken cancellationToken)
        {
            return await _sysUnitOfWork.ISysAccTransactionRepository.GetSessionAttendance(request.SessionId);
        }
    }
}
