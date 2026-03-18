using Application.Common;
using Domain.Entities;
using Domain.Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Model
{
    public class GetSysUserByPinQuery : IRequest<BaseResponse<ExternalApiResponse<ZkPersonCreateDto>>>
    {
        public string Pin { get; set; }
    }
}
