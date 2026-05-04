using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Session.Commands.Update.editFiles
{
    public class UpdateSessionFilesCommand : IRequest<BaseResponse<List<IFormFile>>>
    {
        public int id { get; set; }
        public List<string>? filePaths { get; set; }
    }
}
