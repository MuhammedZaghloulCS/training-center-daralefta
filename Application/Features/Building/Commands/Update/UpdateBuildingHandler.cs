using Application.Common;
using Application.Features.Building.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Building.Commands.Update
{
    public class UpdateBuildingHandler : IRequestHandler<UpdateBuildingCommand, BaseResponse<BuildingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBuildingHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<BuildingDto>> Handle(UpdateBuildingCommand request, CancellationToken cancellationToken)
        {
            var building = await _unitOfWork.IBuildings.GetByPkAsync(request.Id);
            if (building == null||building.IsDeleted)
            {
                return BaseResponse<BuildingDto>.NotFoundResponse("المبني غير موجود");
            }

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("الاسم مطلوب");

            if (string.IsNullOrWhiteSpace(request.Description))
                errors.Add("الوصف مطلوب");

            if (errors.Any())
                return BaseResponse<BuildingDto>.FailureResponse("حدث خطأ", errors);

            building.Name = request.Name;
            building.Description = request.Description;
            building.UpdatedBy = request.UpdatedBy;
            building.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.IBuildings.Update(building);
            await _unitOfWork.Complete();

            var dto = new BuildingDto
            {
                Id = building.Id,
                CreatedBy = building.CreatedBy,
                CreatedDate = building.CreatedDate,
                UpdatedBy = building.UpdatedBy,
                UpdatedAt = building.UpdatedAt,
                Name = building.Name,
                Description = building.Description,
                SysBuildingId = building.SysBuildingId
            };

            return BaseResponse<BuildingDto>.SuccessResponse(dto, "Building updated successfully");
        }
    }
}
