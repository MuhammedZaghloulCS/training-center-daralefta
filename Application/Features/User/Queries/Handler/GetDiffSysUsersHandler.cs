using Application.Common;
using Application.Features.User.Queries.Model;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Handler
{
    public class GetDiffSysUsersHandler : MediatR.IRequestHandler<Model.GetDiffSysUsersQuery, BaseResponse<IEnumerable>>
    {
        private readonly ISysUnitOfWork _sysUnitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public GetDiffSysUsersHandler(ISysUnitOfWork sysUnitOfWork, UserManager<ApplicationUser> userManager)
        {
            _sysUnitOfWork = sysUnitOfWork;
            _userManager = userManager;

        }

        async Task<BaseResponse<IEnumerable>> IRequestHandler<GetDiffSysUsersQuery, BaseResponse<IEnumerable>>.Handle(GetDiffSysUsersQuery request, CancellationToken cancellationToken)
        {
            var sysUsers = await _sysUnitOfWork.ISysPersonRepository.GetAllAsync();
            var existingPinsOnSys= sysUsers.Select(u => u.pin).ToList();
            var existingPins = await _userManager.Users.Where(u=>!u.IsDeleted)
                .Select(u => u.pin)
                .ToListAsync(cancellationToken);
            var diffPins = existingPinsOnSys.Except(existingPins).ToList();
            var results = sysUsers.Where(u => !string.IsNullOrEmpty(u.pin) && diffPins.Contains(u.pin)).Select(u => new { pin = u.pin, name = $"{u.pin} - {u.name} {u.last_name}" });
            return BaseResponse<IEnumerable>.SuccessResponse(data: results);
        }
    }
}
