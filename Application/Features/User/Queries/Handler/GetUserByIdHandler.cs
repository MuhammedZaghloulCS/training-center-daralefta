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
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, BaseResponse<UserDTO>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public GetUserByIdHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<BaseResponse<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
           var user= await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null||user.IsDeleted)
            {
                return BaseResponse<UserDTO>.NotFoundResponse("User not found");
            }
            var userDto = new UserDTO
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

                Doctrine = user.Doctrine,
                MaritalState = user.MaritalState,
                AcademicQualification = user.AcademicQualification,
                Appreciation = user.Appreciation,
                ImagePath = user.ImagePath
            };
            return (BaseResponse<UserDTO>.SuccessResponse(userDto));
        }
    }
}
