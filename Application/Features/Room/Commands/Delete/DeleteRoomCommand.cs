using Application.Common;
using MediatR;

namespace Application.Features.Room.Commands.Delete
{
    public class DeleteRoomCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
}
