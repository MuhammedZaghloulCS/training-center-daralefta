using Application.Common;
using Application.Features.Session.DTOs;
using MediatR;

namespace Application.Features.Session.Queries.Model
{
    public class GetSessionByIdQuery : IRequest<BaseResponse<SessionDto>>
    {
        public int Id { get; set; }
    }
}
