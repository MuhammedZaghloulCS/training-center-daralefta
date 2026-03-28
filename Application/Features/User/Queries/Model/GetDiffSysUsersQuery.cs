using Application.Common;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Model
{
    public class GetDiffSysUsersQuery: IRequest<BaseResponse<IEnumerable>>
    {
    }
}
