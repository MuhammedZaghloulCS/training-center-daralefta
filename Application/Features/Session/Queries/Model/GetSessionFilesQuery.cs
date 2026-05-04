using Application.Common;
using Application.Features.Session.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Session.Queries.Model
{
    public class GetSessionFilesQuery : IRequest<BaseResponse<List<SessionFileDto>>>
    {
        public int SessionId { get; set; }
    }
}
