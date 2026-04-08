using Application.Common;
using Application.Features.User.DTOs;
using del.Models;
using Domain.Entities;
using Domain.Entities.Models;
using Domain.Enums;
using Domain.Helper;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.User.Commands.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, BaseResponse<UserDTO>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISysUnitOfWork _sysUnitOfWork;
        private readonly HttpClient _httpClient;

        public UpdateUserHandler(UserManager<ApplicationUser> userManager, ISysUnitOfWork sysUnitOfWork, IHttpClientFactory httpClient)
        {
            _userManager = userManager;
            _sysUnitOfWork = sysUnitOfWork;
            _httpClient = httpClient.CreateClient("ExternalApi");

        }

        public async Task<BaseResponse<UserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UpdateUser.UserName);

            if (existingUser == null || existingUser.IsDeleted)
                return BaseResponse<UserDTO>.FailureResponse("User not found",
                    new List<string> { $"No user found with Username: {request.UpdateUser.UserName}" });

            // Update properties
            existingUser.Email = request.UpdateUser.Email ?? existingUser.Email;
            existingUser.PhoneNumber = request.UpdateUser.PhoneNumber ?? existingUser.PhoneNumber;
            existingUser.FirstName = request.UpdateUser.FirstName ?? existingUser.FirstName;
            existingUser.LastName = request.UpdateUser.LastName ?? existingUser.LastName;
            existingUser.Gender = request.UpdateUser.Gender.HasValue
                                                    ? (Gender)request.UpdateUser.Gender.Value
                                                    : existingUser.Gender;
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

            // ✅ Update roles دايماً
            var oldRoles = await _userManager.GetRolesAsync(existingUser);
            await _userManager.RemoveFromRolesAsync(existingUser, oldRoles);
            await _userManager.AddToRolesAsync(existingUser, request.UpdateUser.roles);

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BaseResponse<UserDTO>.FailureResponse("User update failed", errors.ToList());
            }
            var emailRegex = new Regex(@"^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");
            bool isValid = emailRegex.IsMatch(request.UpdateUser.Email);
            if (!isValid)
            {
                return BaseResponse<UserDTO>.FailureResponse("البريد الإلكتروني غير صالح");
            }

            // Fingerprint update
            if (!string.IsNullOrEmpty(request.UpdateUser.pin) && request.UpdateUser.pin != "0")
            {
                var pinValue = request.UpdateUser.pin.ToString(); // ← خزّنه في متغير أمان

                var userInSys = await _sysUnitOfWork.ISysPersonRepository
                    .GetFirstOrderedByAsync(p => p.pin == pinValue);
                

                if (userInSys is not null)
                {
                    userInSys.name = existingUser.FirstName;
                    userInSys.last_name = existingUser.LastName;
                    userInSys.mobile_phone = existingUser.PhoneNumber;
                    userInSys.email = existingUser.Email;
                    userInSys.birthday = existingUser.BirthDate;

                    var newSysUser = new
                    {
                        Pin = request.UpdateUser.pin.ToString(),
                        Name = existingUser.FirstName,
                        LastName = existingUser.LastName,
                        Email = existingUser.Email,
                        Gender = existingUser.Gender != Gender.male && existingUser.Gender != Gender.female
                                        ? "M" : existingUser.Gender.GetDescription(),
                        MobilePhone = existingUser.PhoneNumber,
                        personPwd = request.UpdateUser.personPwd
                    };

                    // ✅ PostAsJsonAsync مش PostAsync
                    var res = await _httpClient.PostAsJsonAsync(MainConstants.Use("person/add"), newSysUser);
                    var success = await res.Content.ReadFromJsonAsync<ExternalApiResponse<List<string>>>();

                    if (success.Message == "false" || success.Code != 0)
                    {
                        // ✅ Rollback الـ roles بس - مش حذف المستخدم!
                        await _userManager.RemoveFromRolesAsync(existingUser, request.UpdateUser.roles);
                        await _userManager.AddToRolesAsync(existingUser, oldRoles);

                        if (success.Message == "Repeated Password")
                            return BaseResponse<UserDTO>.FailureResponse("كلمة المرور الخاصة بمكينة البصمة موجودة بالفعل، اختر كلمة أخرى");
                        else if (success.Message == "Mobile number already exists")
                            return BaseResponse<UserDTO>.FailureResponse("رقم الهاتف مستخدم بالفعل");

                        return BaseResponse<UserDTO>.FailureResponse("حاول مرة أخرى");
                    }
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
                ImagePath = existingUser.ImagePath,
                pin = request.UpdateUser.pin  // ← ضيف دي

            });
        }
    }
}