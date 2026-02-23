using Application.Common;
using Application.Features.Building.DTOs;
using Application.Features.Building.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Building.Queries.Handler
{
    public class GetBuildingByIdHandler : IRequestHandler<GetBuildingByIdQuery, BaseResponse<BuildingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBuildingByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<BuildingDto>> Handle(GetBuildingByIdQuery request, CancellationToken cancellationToken)
        {
            var buildings = await _unitOfWork.IBuildings.FindRowAsync(b => b.Id == request.Id&&!b.IsDeleted, b => b.Rooms);
            var building = buildings.FirstOrDefault();
            if (building == null||building.IsDeleted)
            {
                return BaseResponse<BuildingDto>.NotFoundResponse("Building not found");
            }

            var dto = new BuildingDto
            {
                Id = building.Id,
                CreatedBy = building.CreatedBy,
                CreatedDate = building.CreatedDate,
                UpdatedBy = building.UpdatedBy,
                UpdatedAt = building.UpdatedAt,
                Name = building.Name,
                Description = building.Description
            };

            return BaseResponse<BuildingDto>.SuccessResponse(dto, "Building retrieved successfully");
        }
    }
}
