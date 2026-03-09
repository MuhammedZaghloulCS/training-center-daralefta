using Application.Common;
using Application.Features.User.DTOs;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.User.Commands.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, BaseResponse<UserDTO>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BaseResponse<UserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // Find the existing user
            var existingUser = await _userManager.FindByNameAsync(request.UpdateUser.UserName);
            
            if (existingUser == null || existingUser.IsDeleted)
            {
                return BaseResponse<UserDTO>.FailureResponse("User not found", new List<string> { $"No user found with Username: {request.UpdateUser.UserName}" });
            }

            // Update user properties
            existingUser.Email = request.UpdateUser.Email??existingUser.Email;
            existingUser.PhoneNumber = request.UpdateUser.PhoneNumber??existingUser.PhoneNumber;
            existingUser.FirstName = request.UpdateUser.FirstName ?? existingUser.FirstName;
            existingUser.LastName = request.UpdateUser.LastName ?? existingUser.LastName;
            existingUser.Gender =(Gender) request.UpdateUser.Gender ;
            
            existingUser.JobTitle = request.UpdateUser.JobTitle ?? existingUser.JobTitle;
            existingUser.AcademicTitle = request.UpdateUser.AcademicTitle ?? existingUser.AcademicTitle;
            existingUser.Organization = request.UpdateUser.Organization ?? existingUser.Organization;
            existingUser.Specialization = request.UpdateUser.Specialization ?? existingUser.Specialization;
            existingUser.Skills = request.UpdateUser.Skills ?? existingUser.Skills;
            existingUser.WhatsappNumber = request.UpdateUser.WhatsappNumber ?? existingUser.WhatsappNumber;
            existingUser.BirthDate = request.UpdateUser.BirthDate;
            existingUser.NationalIdImage = request.UpdateUser.NationalIdImage ?? existingUser.NationalIdImage;
            existingUser.AddressInsideCairo = request.UpdateUser.AddressInsideCairo ?? existingUser.AddressInsideCairo;
            existingUser.AddressOutsideCairo = request.UpdateUser.AddressOutsideCairo ?? existingUser.AddressOutsideCairo;
            existingUser.Doctrine = request.UpdateUser.Doctrine ?? existingUser.Doctrine;
            existingUser.MaritalState = request.UpdateUser.MaritalState ?? existingUser.MaritalState;
            existingUser.AcademicQualification = request.UpdateUser.AcademicQualification ?? existingUser.AcademicQualification;
            existingUser.Appreciation = request.UpdateUser.Appreciation ?? existingUser.Appreciation;
            existingUser.ImagePath = request.UpdateUser.ImagePath ?? existingUser.ImagePath;

            // Update the user
            var result = await _userManager.UpdateAsync(existingUser);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BaseResponse<UserDTO>.FailureResponse("User update failed", errors.ToList());
            }

            /*// If password is provided, update it
            if (!string.IsNullOrEmpty(request.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
                var passwordResult = await _userManager.ResetPasswordAsync(existingUser, token, request.Password);

                if (!passwordResult.Succeeded)
                {
                    var errors = passwordResult.Errors.Select(e => e.Description);
                    return BaseResponse<UserDTO>.FailureResponse("Password update failed", errors.ToList());
                }
            }*/


            return BaseResponse<UserDTO>.SuccessResponse(data: new UserDTO
            {
                Id = existingUser.Id,
                UserName = existingUser.UserName,
                Email = existingUser.Email,
                PhoneNumber = existingUser.PhoneNumber,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                Gender = existingUser.Gender,
                JobTitle = existingUser.JobTitle,
                AcademicTitle = existingUser.AcademicTitle,
                Organization = existingUser.Organization,
                Specialization = existingUser.Specialization,
                Skills = existingUser.Skills,
                WhatsappNumber = existingUser.WhatsappNumber,
                BirthDate = existingUser.BirthDate.Value,
                NationalIdImage = existingUser.NationalIdImage,
                AddressInsideCairo = existingUser.AddressInsideCairo,
                AddressOutsideCairo = existingUser.AddressOutsideCairo,
                Doctrine = existingUser.Doctrine,
                MaritalState = existingUser.MaritalState,
                AcademicQualification = existingUser.AcademicQualification,
                Appreciation = existingUser.Appreciation,
                ImagePath = existingUser.ImagePath
            });
        }
    }
}