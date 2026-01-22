using Application.Common;
using MediatR;

namespace Application.Features.Session.Commands.Delete
{
    public class DeleteSessionCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
}
