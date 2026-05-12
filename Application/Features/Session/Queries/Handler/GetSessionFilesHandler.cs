using Application.Common;
using Application.Features.Session.DTOs;
using Application.Features.Session.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<GetSessionFilesHandler> _logger;

        public GetSessionFilesHandler(IUnitOfWork unitOfWork, IWebHostEnvironment env, ILogger<GetSessionFilesHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _env = env;
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
                    var storagePath = Path.GetFullPath(Path.Combine(_env.ContentRootPath, "..", "..", "TrainingCenterStorage"));
                    
                    foreach (var filePath in session.filesPaths)
                    {
                        // Convert relative URL path to physical path
                        var physicalPath = Path.Combine(storagePath, filePath.Replace("/storage/", "").Replace("/", Path.DirectorySeparatorChar.ToString()));
                        
                        if (!File.Exists(physicalPath))
                            continue;

                        var fileInfo = new FileInfo(physicalPath);
                        var bytes = await File.ReadAllBytesAsync(physicalPath, cancellationToken);

                        files.Add(new SessionFileDto
                        {
                            FileName = Path.GetFileName(physicalPath),
                            FilePath = filePath, // Keep relative URL path
                            FileType = Path.GetExtension(physicalPath),
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
