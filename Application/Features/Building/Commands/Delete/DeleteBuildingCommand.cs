using Application.Common;
using MediatR;

namespace Application.Features.Building.Commands.Delete
{
    public class DeleteBuildingCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
}
