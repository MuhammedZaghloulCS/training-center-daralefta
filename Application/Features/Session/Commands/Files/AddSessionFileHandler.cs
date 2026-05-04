using Application.Common;
using Application.Features.Session.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Session.Commands.Files
{
    public class AddSessionFileHandler : IRequestHandler<AddSessionFileCommand, BaseResponse<SessionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<AddSessionFileHandler> _logger;

        public AddSessionFileHandler(IUnitOfWork unitOfWork, IWebHostEnvironment env, ILogger<AddSessionFileHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _logger = logger;
        }

        public async Task<BaseResponse<SessionDto>> Handle(AddSessionFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var session = await _unitOfWork.ISession.GetByPkAsync(request.SessionId);
                
                if (session == null || session.IsDeleted)
                {
                    return BaseResponse<SessionDto>.NotFoundResponse("Session not found");
                }

                if (request.Files == null || request.Files.Count == 0)
                {
                    return BaseResponse<SessionDto>.FailureResponse("No files provided");
                }

                var filesPaths = session.filesPaths ?? new List<string>();

                var uploads = Path.Combine(_env.WebRootPath, "sessiondata");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                foreach (var file in request.Files)
                {
                    if (file.Length > 0)
                    {
                        var extension = Path.GetExtension(file.FileName);
                        var originalName = Path.GetFileNameWithoutExtension(file.FileName);
                        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        var fileName = $"{originalName}_{timestamp}{extension}";
                        var filePath = Path.Combine(uploads, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        filesPaths.Add(filePath);

                    }
                }

                session.filesPaths = filesPaths;
                _unitOfWork.ISession.Update(session);
                await _unitOfWork.Complete();

                var dto = session.Adapt<SessionDto>();

                return BaseResponse<SessionDto>.SuccessResponse(dto, "Files added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding files to session {SessionId}", request.SessionId);
                return BaseResponse<SessionDto>.FailureResponse("Error adding files");
            }
        }
    }
}
