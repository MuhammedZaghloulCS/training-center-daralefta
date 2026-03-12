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
            var pins = users.Select(u => u.pin).ToList();
            var usersInSys = await sysUnitOfWork.ISysPersonRepository.GetAllAsync(p => !pins.Contains(p.pin));

            var inCompleteUsers = usersInSys.Select(u => new UserDTO
            {
                Id = Guid.Empty,
                UserName = String.Empty,

                Email = String.Empty,
                PhoneNumber = String.Empty,

                FirstName = String.Empty,
                LastName = String.Empty,
                Gender = Gender.male,


                JobTitle = String.Empty,
                AcademicTitle = String.Empty,
                Organization = String.Empty,
                Specialization = String.Empty,
                Skills = String.Empty,
                WhatsappNumber = String.Empty,

                BirthDate = DateTime.MinValue, // أو سيبها nullable لو عدلت DTO
                NationalIdImage = String.Empty,

                AddressInsideCairo = String.Empty,
                AddressOutsideCairo = String.Empty,

                Doctrine = String.Empty,
                MaritalState = String.Empty,
                AcademicQualification = String.Empty,
                Appreciation = String.Empty,
                ImagePath = String.Empty,
                pin = u.pin
            }).ToList();
            var userDTOs = users.Select(u => new UserDTO
                {
                    Id = u.Id,
                    UserName = u.UserName,

                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,

                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Gender = u.Gender,

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
                    ImagePath = u.ImagePath,
                    pin = u.pin
                

            }).ToList();
            if(inCompleteUsers.Count()>0)
            userDTOs.InsertRange(0,inCompleteUsers);
            return BaseResponse<List<UserDTO>>.SuccessResponse(data: userDTOs, message: "Users retrieved successfully");
        }
    }
}
