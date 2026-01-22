using Application.Common;
using Application.Features.Session.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Session.Queries.Model
{
    public class GetSessionsPagedQuery : IRequest<BaseResponse<List<SessionListDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
