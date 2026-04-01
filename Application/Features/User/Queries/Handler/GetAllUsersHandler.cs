using Application.Common;
using Application.Features.User.DTOs;
using Application.Features.User.Queries.Model;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Handler
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, BaseResponse<List<UserDTO>>>
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISysUnitOfWork sysUnitOfWork;
        public GetAllUsersHandler(UserManager<ApplicationUser> userManager, ISysUnitOfWork sysUnitOfWork)
        {
            this._userManager = userManager;
            this.sysUnitOfWork = sysUnitOfWork;
        }

        public async Task<BaseResponse<List<UserDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {

            var users = await _userManager.Users.Where(u => !u.IsDeleted).AsNoTracking().ToListAsync();
            if (users == null)
            {
                return BaseResponse<List<UserDTO>>.FailureResponse("No users found");
            }
            else if (users.Count == 0) {
                return BaseResponse<List<UserDTO>>.SuccessResponse(data: default, message: "No users found");
            }

            // ✅ Mapping
            var userDTOs = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userDTOs.Add(new UserDTO
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

                    BirthDate = user.BirthDate ?? DateTime.MinValue,
                    NationalIdImage = user.NationalIdImage,

                    AddressInsideCairo = user.AddressInsideCairo,
                    AddressOutsideCairo = user.AddressOutsideCairo,

                    Doctrine = user.Doctrine,
                    MaritalState = user.MaritalState,
                    AcademicQualification = user.AcademicQualification,
                    Appreciation = user.Appreciation,

                    ImagePath = user.ImagePath,
                    IsActive = user.IsActive,
                    roles = roles.ToList()
                });
            }


            return BaseResponse<List<UserDTO>>.SuccessResponse(data: userDTOs, message: "Users retrieved successfully");
        }
    }
}
