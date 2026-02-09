using Application.Common;
using Application.Features.User.DTOs;
using Application.Features.User.Queries.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Handler
{
    public class GetUsersBySessionIdHandler : IRequestHandler<GetUsersBySessionIdPagedQuery, BaseResponse<List<UserDTO>>>
    {
        public Task<BaseResponse<List<UserDTO>>> Handle(GetUsersBySessionIdPagedQuery request, CancellationToken cancellationToken)
        {
            
        }
    }
}
