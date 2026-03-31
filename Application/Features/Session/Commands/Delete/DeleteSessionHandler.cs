using Application.Common;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Session.Commands.Delete
{
    public class DeleteSessionHandler : IRequestHandler<DeleteSessionCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSessionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _unitOfWork.ISession.GetByPkAsync(request.Id);
            if (session == null||session.IsDeleted)
            {
                return BaseResponse<bool>.NotFoundResponse("Session not found");
            }
            session.IsDeleted = true;
            _unitOfWork.ISession.Update(session);
            var userSession = await _unitOfWork.IUserSessionRepository.FindRowAsync(us => us.SessionId == request.Id);
             _unitOfWork.IUserSessionRepository.DeleteRange(userSession);
            await _unitOfWork.Complete();

            return BaseResponse<bool>.SuccessResponse(true, "Deleted successfully");
        }
    }
}
