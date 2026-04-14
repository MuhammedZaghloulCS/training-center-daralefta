using Application.Common;
using Application.Features.User.DTOs;
using Application.Features.User.Queries.Model;
using Domain.Entities;
using Infrastructure.Context;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Handler
{
    public class GetAllActivatedUsersHandler : IRequestHandler<GetAllActivatedUsersQuery, BaseResponse<List<UserDTO>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationContext _context;
        public GetAllActivatedUsersHandler(UserManager<ApplicationUser> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<BaseResponse<List<UserDTO>>> Handle(GetAllActivatedUsersQuery request, CancellationToken cancellationToken)
        {

            var activatedUsers = await _userManager.Users
                .Where(u =>
                    u.IsActive &&
                    !u.IsDeleted &&
                    !string.IsNullOrWhiteSpace(u.pin) &&
                    u.pin != "0"
                ).OrderByDescending(u => u.CreatingDate)
                .ToListAsync();
            if (activatedUsers == null || !activatedUsers.Any())
            {
                return BaseResponse<List<UserDTO>>.NotFoundResponse("No activated users found.");
            }
            List<UserDTO> result = activatedUsers.Adapt<List<UserDTO>>();
           
            foreach (var user in activatedUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.FirstOrDefault(u => u.Id == user.Id).roles = roles.ToList();
            }
            ;

            return BaseResponse<List<UserDTO>>.SuccessResponse(result);


        }
    }
}
