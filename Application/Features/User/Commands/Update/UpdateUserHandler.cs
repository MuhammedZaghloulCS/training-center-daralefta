using Application.Common;
using Application.Features.User.DTOs;
using Domain.Entities;
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
            var existingUser = await _userManager.FindByNameAsync(request.User.UserName);
            
            if (existingUser == null)
            {
                return BaseResponse<UserDTO>.FailureResponse("User not found", new List<string> { $"No user found with ID: {request.User.Id}" });
            }

            // Update user properties
            existingUser.Email = request.User.Email;
            existingUser.PhoneNumber = request.User.PhoneNumber;
            existingUser.FirstName = request.User.FirstName;
            existingUser.LastName = request.User.LastName;
            existingUser.Gender = request.User.Gender;
            
            existingUser.JobTitle = request.User.JobTitle;
            existingUser.AcademicTitle = request.User.AcademicTitle;
            existingUser.Organization = request.User.Organization;
            existingUser.Specialization = request.User.Specialization;
            existingUser.Skills = request.User.Skills;
            existingUser.WhatsappNumber = request.User.WhatsappNumber;
            existingUser.BirthDate = request.User.BirthDate;
            existingUser.NationalIdImage = request.User.NationalIdImage;
            existingUser.AddressInsideCairo = request.User.AddressInsideCairo;
            existingUser.AddressOutsideCairo = request.User.AddressOutsideCairo;
            existingUser.Doctrine = request.User.Doctrine;
            existingUser.MaritalState = request.User.MaritalState;
            existingUser.AcademicQualification = request.User.AcademicQualification;
            existingUser.Appreciation = request.User.Appreciation;
            existingUser.ImagePath = request.User.ImagePath;

            // Update the user
            var result = await _userManager.UpdateAsync(existingUser);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BaseResponse<UserDTO>.FailureResponse("User update failed", errors.ToList());
            }

            // If password is provided, update it
            if (!string.IsNullOrEmpty(request.User.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
                var passwordResult = await _userManager.ResetPasswordAsync(existingUser, token, request.User.Password);

                if (!passwordResult.Succeeded)
                {
                    var errors = passwordResult.Errors.Select(e => e.Description);
                    return BaseResponse<UserDTO>.FailureResponse("Password update failed", errors.ToList());
                }
            }


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