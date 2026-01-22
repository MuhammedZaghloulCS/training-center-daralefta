using Application.Common;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Room.Commands.Delete
{
    public class DeleteRoomHandler : IRequestHandler<DeleteRoomCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoomHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _unitOfWork.IRooms.GetByPkAsync(request.Id);
            if (room == null)
            {
                return BaseResponse<bool>.NotFoundResponse("Room not found");
            }

            _unitOfWork.IRooms.Delete(room);
            await _unitOfWork.Complete();

            return BaseResponse<bool>.SuccessResponse(true, "Deleted successfully");
        }
    }
}
