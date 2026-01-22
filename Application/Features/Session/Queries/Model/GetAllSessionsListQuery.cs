using Application.Common;
using Application.Features.Session.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Session.Queries.Model
{
    public class GetAllSessionsListQuery : IRequest<BaseResponse<List<SessionListDTO>>>
    {
    }
}
