using Application.Common;
using Application.Features.Building.DTOs;
using Application.Features.Building.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Building.Queries.Handler
{
    public class GetBuildingsPagedHandler : IRequestHandler<GetBuildingsPagedQuery, BaseResponse<List<BuildingListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBuildingsPagedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<BuildingListDTO>>> Handle(GetBuildingsPagedQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<BuildingListDTO>>.BadRequestResponse("Invalid pagination parameters");
            }

            var (items, totalCount) = await _unitOfWork.IBuildings.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                null,
                b => b.Id,
                true,
                b => b.Rooms);

            if (items == null || !items.Any()||items.All(r=>r.IsDeleted))
            {
                return BaseResponse<List<BuildingListDTO>>.SuccessResponse(
                    new List<BuildingListDTO>(),
                    request.PageNumber,
                    request.PageSize,
                    totalCount,
                    "No building found");
            }

            var data = items.Where(b=>!b.IsDeleted).Select(b => new BuildingListDTO
            {
                Id = b.Id,
                CreatedBy = b.CreatedBy,
                CreatedDate = b.CreatedDate,
                UpdatedBy = b.UpdatedBy,
                UpdatedAt = b.UpdatedAt,
                Name = b.Name,
                Description = b.Description,
                SysBuildingId = b.SysBuildingId
            }).ToList();

            return BaseResponse<List<BuildingListDTO>>.SuccessResponse(
                data,
                request.PageNumber,
                request.PageSize,
                totalCount,
                "Buildings retrieved successfully");
        }
    }
}
