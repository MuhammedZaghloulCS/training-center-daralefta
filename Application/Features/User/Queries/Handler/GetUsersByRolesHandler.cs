using Application.Common;
using Application.Features.User.DTOs;
using Application.Features.User.Queries.Model;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Handler
{
    public class GetUsersByRolesHandler : IRequestHandler<GetUsersByRolesQuery, BaseResponse<List<UserDTO>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUsersByRolesHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BaseResponse<List<UserDTO>>> Handle(GetUsersByRolesQuery request, CancellationToken cancellationToken)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(request.roleName);
           usersInRole = usersInRole.Where(user => !user.IsDeleted).ToList(); // تأكد من استبعاد المستخدمين المحذوفين
            if (usersInRole == null||usersInRole.All(r=>r.IsDeleted))
            {
                return BaseResponse<List<UserDTO>>.NotFoundResponse($"No users found in role '{request.roleName}'.");
            }
        
            var userDTOs = usersInRole.Where(r => !r.IsDeleted).Select(user => new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,

                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                
                JobTitle = user.JobTitle,
                AcademicTitle = user.AcademicTitle,
                Organization = user.Organization,
                Specialization = user.Specialization,
                Skills = user.Skills,
                WhatsappNumber = user.WhatsappNumber,

                BirthDate = user.BirthDate ?? DateTime.MinValue, // أو سيبها nullable لو عدلت DTO
                NationalIdImage = user.NationalIdImage,
                AddressInsideCairo = user.AddressInsideCairo,
                AddressOutsideCairo = user.AddressOutsideCairo,
                FullName = $"{user.FirstName} {user.LastName}",
                Doctrine = user.Doctrine,
                MaritalState = user.MaritalState,
                AcademicQualification = user.AcademicQualification,
                Appreciation = user.Appreciation,
                ImagePath = user.ImagePath
            }).ToList();
            

            return BaseResponse<List<UserDTO>>.SuccessResponse(userDTOs, $"Users in role '{request.roleName}' retrieved successfully.");
        }
    }
}
