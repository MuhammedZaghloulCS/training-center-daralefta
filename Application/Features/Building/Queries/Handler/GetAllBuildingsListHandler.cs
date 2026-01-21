using Application.Common;
using Application.Features.Building.DTOs;
using Application.Features.Building.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Building.Queries.Handler
{
    public class GetAllBuildingsListHandler : IRequestHandler<GetAllBuildingsListQuery, BaseResponse<List<BuildingListDTO>>>
    {
        #region Fields
        IUnitOfWork _unitOfWork;
        #endregion
        //CTOR
        public GetAllBuildingsListHandler(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<List<BuildingListDTO>>> Handle(GetAllBuildingsListQuery request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.IBuildings.GetAllAsync();

            if (response == null || !response.Any())
            {
                return BaseResponse<List<BuildingListDTO>>.SuccessResponse(
                    new List<BuildingListDTO>(),
                    "No building found"
                );
            }

            var data = response.Select(b => new BuildingListDTO
            {
                Id = b.Id,
                CreatedBy = b.CreatedBy,
                CreatedDate = b.CreatedDate,
                UpdatedBy = b.UpdatedBy,
                UpdatedAt = b.UpdatedAt,
                Name = b.Name,
                Description = b.Description
            }).ToList();
            return BaseResponse<List<BuildingListDTO>>.SuccessResponse(
                data,
                "Buildings retrieved successfully"
            );
        }
    }
}
