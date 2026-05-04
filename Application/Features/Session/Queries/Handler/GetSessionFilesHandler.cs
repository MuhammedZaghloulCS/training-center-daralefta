using Application.Common;
using Application.Features.Session.DTOs;
using Application.Features.Session.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Session.Queries.Handler
{
    public class GetSessionFilesHandler : IRequestHandler<GetSessionFilesQuery, BaseResponse<List<SessionFileDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetSessionFilesHandler> _logger;

        public GetSessionFilesHandler(IUnitOfWork unitOfWork, ILogger<GetSessionFilesHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<BaseResponse<List<SessionFileDto>>> Handle(GetSessionFilesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var session = await _unitOfWork.ISession.GetByPkAsync(request.SessionId);

                if (session == null || session.IsDeleted)
                    return BaseResponse<List<SessionFileDto>>.NotFoundResponse("Session not found");

                var files = new List<SessionFileDto>();

                if (session.filesPaths != null && session.filesPaths.Any())
                {
                    foreach (var filePath in session.filesPaths)
                    {
                        if (!File.Exists(filePath))
                            continue;

                        var fileInfo = new FileInfo(filePath);
                        var bytes = await File.ReadAllBytesAsync(filePath, cancellationToken);

                        files.Add(new SessionFileDto
                        {
                            FileName = Path.GetFileName(filePath),
                            FilePath = filePath,
                            FileType = Path.GetExtension(filePath),
                            FileSize = fileInfo.Length,
                            Content = Convert.ToBase64String(bytes),
                            SessionId = session.Id
                        });
                    }
                }

                return BaseResponse<List<SessionFileDto>>.SuccessResponse(files);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving files");
                return BaseResponse<List<SessionFileDto>>.FailureResponse("Error retrieving files");
            }
        }
    }
}
