using Application.Common;
using Application.Features.Session.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Application.Features.Session.Commands.Files
{
    public class AddSessionFileCommand : IRequest<BaseResponse<SessionDto>>
    {
        public int SessionId { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
}
