using Application.Common;
using Application.Features.Building.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Building.Queries.Model
{
    public class GetBuildingsPagedQuery : IRequest<BaseResponse<List<BuildingListDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
    }
}
