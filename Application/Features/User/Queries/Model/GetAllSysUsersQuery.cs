using Application.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Model
{
    public class GetAllSysUsersQuery : IRequest<BaseResponse<IEnumerable>>
    {
    }
}
