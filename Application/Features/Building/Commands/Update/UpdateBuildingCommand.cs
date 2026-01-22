using Application.Common;
using Application.Features.Building.DTOs;
using MediatR;

namespace Application.Features.Building.Commands.Update
{
    public class UpdateBuildingCommand : IRequest<BaseResponse<BuildingDto>>
    {
        public int Id { get; set; }
        public string? UpdatedBy { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
