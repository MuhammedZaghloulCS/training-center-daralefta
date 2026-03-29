using Application.Common;
using Application.Features.Building.DTOs;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Building.Commands.Create
{
    public class CreateBuildingHandler : IRequestHandler<CreateBuildingCommand, BaseResponse<BuildingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBuildingHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<BuildingDto>> Handle(CreateBuildingCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            var buildings=await _unitOfWork.IBuildings.GetFirstByPropAsync(b => b.SysBuildingId == request.SysBuildingId || b.Name==request.Name&&!b.IsDeleted);
            if (buildings != null)
                errors.Add("يوجد مبنى بنفس المعرف");

            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("الاسم مطلوب");

            if (string.IsNullOrWhiteSpace(request.Description))
                errors.Add("الوصف مطلوب");
            if (string.IsNullOrWhiteSpace(request.SysBuildingId))
                errors.Add("معرف المبنى مطلوب");
            if (request.Name.Length > 50)
                errors.Add("اسم المبنى لا يجب أن يتجاوز 50 حرفًا");



            if (errors.Any())
                return BaseResponse<BuildingDto>.FailureResponse("Validation failed", errors);



            var building = new Domain.Entities.Building
            {
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                Name = request.Name,
                Description = request.Description,
                SysBuildingId=request.SysBuildingId,
            };

            await _unitOfWork.IBuildings.AddAsync(building);
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
                SysBuildingId = request.SysBuildingId,

            };

            return BaseResponse<BuildingDto>.SuccessResponse(dto, "Building created successfully");
        }
    }
}
