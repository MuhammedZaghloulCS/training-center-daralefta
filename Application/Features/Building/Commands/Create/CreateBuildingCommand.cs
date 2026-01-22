using Application.Common;
using Application.Features.Building.DTOs;
using MediatR;

namespace Application.Features.Building.Commands.Create
{
    public class CreateBuildingCommand : IRequest<BaseResponse<BuildingDto>>
    {
        public string CreatedBy { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
