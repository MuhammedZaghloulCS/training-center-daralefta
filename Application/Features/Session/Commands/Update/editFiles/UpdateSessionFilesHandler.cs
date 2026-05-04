using Application.Common;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Session.Commands.Update.editFiles
{
    public class UpdateSessionFilesHandler : IRequestHandler<UpdateSessionFilesCommand, BaseResponse<List<IFormFile>>>
    {
        private readonly IWebHostEnvironment _env;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateSessionFilesHandler(IWebHostEnvironment env,IUnitOfWork unitOfWork)
        {
            _env = env;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<IFormFile>>> Handle(UpdateSessionFilesCommand request, CancellationToken cancellationToken)
        {
            //var session = await _unitOfWork.ISession.GetByPkAsync(request.id);
            //if(session==null)
            //    return BaseResponse<List<IFormFile>>.BadRequestResponse();

            //var filesPaths = new List<string>();
            //if (request.filePaths == null || !request.filePaths.Any())
            //    return BaseResponse<List<IFormFile>>.BadRequestResponse();

            //foreach (var file in request.filePaths)
            //{
            //    var uploads = Path.Combine(_env.WebRootPath, "sessiondata", file);

            //    filesPaths.Add(uploads);
            //}
            //var diff=session.filesPaths.ExceptBy()
            throw new Exception();
        }
    }
}
