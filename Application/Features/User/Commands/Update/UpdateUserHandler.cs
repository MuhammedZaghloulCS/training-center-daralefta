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
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            
            if (existingUser == null || existingUser.IsDeleted)
            {
                return BaseResponse<UserDTO>.FailureResponse("User not found", new List<string> { $"No user found with ID: {request.User.Id}" });
            }

            // Update user properties
            existingUser.Email = request.Email??existingUser.Email;
            existingUser.PhoneNumber = request.PhoneNumber??existingUser.PhoneNumber;
            existingUser.FirstName = request.FirstName ?? existingUser.FirstName;
            existingUser.LastName = request.LastName ?? existingUser.LastName;
            existingUser.Gender = request.Gender ;
            
            existingUser.JobTitle = request.JobTitle ?? existingUser.JobTitle;
            existingUser.AcademicTitle = request.AcademicTitle ?? existingUser.AcademicTitle;
            existingUser.Organization = request.Organization ?? existingUser.Organization;
            existingUser.Specialization = request.Specialization ?? existingUser.Specialization;
            existingUser.Skills = request.Skills ?? existingUser.Skills;
            existingUser.WhatsappNumber = request.WhatsappNumber ?? existingUser.WhatsappNumber;
            existingUser.BirthDate = request.BirthDate;
            existingUser.NationalIdImage = request.NationalIdImage ?? existingUser.NationalIdImage;
            existingUser.AddressInsideCairo = request.AddressInsideCairo ?? existingUser.AddressInsideCairo;
            existingUser.AddressOutsideCairo = request.AddressOutsideCairo ?? existingUser.AddressOutsideCairo;
            existingUser.Doctrine = request.Doctrine ?? existingUser.Doctrine;
            existingUser.MaritalState = request.MaritalState ?? existingUser.MaritalState;
            existingUser.AcademicQualification = request.AcademicQualification ?? existingUser.AcademicQualification;
            existingUser.Appreciation = request.Appreciation ?? existingUser.Appreciation;
            existingUser.ImagePath = request.ImagePath ?? existingUser.ImagePath;

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