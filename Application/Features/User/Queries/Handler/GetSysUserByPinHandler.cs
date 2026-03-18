using Application.Common;
using Application.Features.User.Queries.Model;
using Domain.Entities;
using Domain.Entities.Models;
using Domain.Helper;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Application.Features.User.Queries.Handler
{
    public class GetSysUserByPinHandler : IRequestHandler<Model.GetSysUserByPinQuery, BaseResponse<ExternalApiResponse<ZkPersonCreateDto>>>
    {
        private readonly HttpClient _httpClient;
        public GetSysUserByPinHandler(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("ExternalApi");


        }
        public async Task<BaseResponse<ExternalApiResponse<ZkPersonCreateDto>>> Handle(GetSysUserByPinQuery request, CancellationToken cancellationToken)
        {
            var sysUser = await _httpClient.GetAsync(MainConstants.Use($"person/get/{request.Pin}"), cancellationToken);
            if (sysUser.IsSuccessStatusCode)
            {
                var zkPersonCreateDto = await sysUser.Content.ReadFromJsonAsync<ExternalApiResponse<ZkPersonCreateDto>>();
                return BaseResponse<ExternalApiResponse<ZkPersonCreateDto>>.SuccessResponse(zkPersonCreateDto);
            }
            else
            {
                return BaseResponse<ExternalApiResponse<ZkPersonCreateDto>>.FailureResponse($"Failed to retrieve user with pin {request.Pin}");
            }
        }
    }
}
