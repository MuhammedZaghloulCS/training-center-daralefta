using Application.Common;
using Application.Features.Building.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Building.Queries.Model
{
    public class GetAllBuildingsListQuery : IRequest<BaseResponse<List<BuildingListDTO>>>
    {
    }
}
