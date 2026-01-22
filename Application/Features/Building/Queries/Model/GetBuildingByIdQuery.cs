using Application.Common;
using Application.Features.Building.DTOs;
using MediatR;

namespace Application.Features.Building.Queries.Model
{
    public class GetBuildingByIdQuery : IRequest<BaseResponse<BuildingDto>>
    {
        public int Id { get; set; }
    }
}
