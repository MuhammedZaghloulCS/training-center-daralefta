using Application.Common;
using Application.Features.User.DTOs;
using Application.Features.User.Queries.Model;
using Domain.Entities;
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
        public GetAllUsersHandler(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;   
        }

        public async Task<BaseResponse<List<UserDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.AsNoTracking().ToListAsync();
            if (users == null)
            {
                return BaseResponse<List<UserDTO>>.FailureResponse("No users found");
            }
            else if (users.Count == 0) {
                return BaseResponse<List<UserDTO>>.SuccessResponse(data:default,message:"No users found");
            }
            var userDTOs = users.Select(u => new UserDTO
            {
                Id = u.Id,
                UserName = u.UserName,

                Email = u.Email,
                PhoneNumber = u.PhoneNumber,

                FirstName = u.FirstName,
                LastName = u.LastName,
                Gender = u.Gender,
                PersonType = u.PersonType,

                JobTitle = u.JobTitle,
                AcademicTitle = u.AcademicTitle,
                Organization = u.Organization,
                Specialization = u.Specialization,
                Skills = u.Skills,
                WhatsappNumber = u.WhatsappNumber,

                BirthDate = u.BirthDate ?? DateTime.MinValue, // أو سيبها nullable لو عدلت DTO
                NationalIdImage = u.NationalIdImage,

                AddressInsideCairo = u.AddressInsideCairo,
                AddressOutsideCairo = u.AddressOutsideCairo,

                Doctrine = u.Doctrine,
                MaritalState = u.MaritalState,
                AcademicQualification = u.AcademicQualification,
                Appreciation = u.Appreciation,
                ImagePath = u.ImagePath

            }).ToList();

            return BaseResponse<List<UserDTO>>.SuccessResponse(data: userDTOs, message: "Users retrieved successfully");
        }
    }
}
