using Application.Common;
using Application.Features.User.Queries.Model;
using Domain.Entities;
using Domain.Helper;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Application.Features.User.Queries.Handler
{
    public class GetaAllSysUsersHandler : MediatR.IRequestHandler<Model.GetAllSysUsersQuery, BaseResponse<IEnumerable>>
    {
        private readonly ISysUnitOfWork _sysUnitOfWork;
        public GetaAllSysUsersHandler(ISysUnitOfWork sysUnitOfWork)
        {
            _sysUnitOfWork= sysUnitOfWork;


        }
        public async Task<BaseResponse<IEnumerable>> Handle(GetAllSysUsersQuery request, CancellationToken cancellationToken)
        {
            var sysUsers = await _sysUnitOfWork.ISysPersonRepository.GetAllAsync();

           var results= sysUsers.Select(u => new { pin=u.pin,name= $"{u.pin} - {u.name} {u.last_name}" });
            return  BaseResponse <IEnumerable>.SuccessResponse(data: results);

        }
    }
}
