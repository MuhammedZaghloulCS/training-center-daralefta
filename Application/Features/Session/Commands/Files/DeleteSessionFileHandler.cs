using Application.Common;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Session.Commands.Files
{
    public class DeleteSessionFileHandler : IRequestHandler<DeleteSessionFileCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteSessionFileHandler> _logger;

        public DeleteSessionFileHandler(IUnitOfWork unitOfWork, ILogger<DeleteSessionFileHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteSessionFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var session = await _unitOfWork.ISession.GetByPkAsync(request.SessionId);
                
                if (session == null || session.IsDeleted)
                {
                    return BaseResponse<bool>.NotFoundResponse("Session not found");
                }

                if (session.filesPaths == null || !session.filesPaths.Contains(request.FilePath))
                {
                    return BaseResponse<bool>.FailureResponse("File not found in session");
                }

                // Delete physical file
                if (File.Exists(request.FilePath))
                {
                    File.Delete(request.FilePath);
                }

                // Remove from session's file list
                session.filesPaths = session.filesPaths.Where(f => f != request.FilePath).ToList();
                _unitOfWork.ISession.Update(session);
                await _unitOfWork.Complete();

                _logger.LogInformation("Deleted file {FilePath} from session {SessionId}", request.FilePath, session.Id);

                return BaseResponse<bool>.SuccessResponse(true, "File deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file from session {SessionId}", request.SessionId);
                return BaseResponse<bool>.FailureResponse("Error deleting file");
            }
        }
    }
}
