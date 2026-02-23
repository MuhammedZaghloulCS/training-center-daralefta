using Application.Common;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Building.Commands.Delete
{
    public class DeleteBuildingHandler : IRequestHandler<DeleteBuildingCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBuildingHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteBuildingCommand request, CancellationToken cancellationToken)
        {
            var building = await _unitOfWork.IBuildings.GetByPkAsync(request.Id);
            if (building == null||building.IsDeleted)
            {
                return BaseResponse<bool>.NotFoundResponse("Building not found");
            }
            building.IsDeleted = true;
            _unitOfWork.IBuildings.Update(building);
            await _unitOfWork.Complete();

            return BaseResponse<bool>.SuccessResponse(true, "Deleted successfully");
        }
    }
}
