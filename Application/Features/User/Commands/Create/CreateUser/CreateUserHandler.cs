using Application.Common;
using Application.Features.Session.DTOs;
using Application.Features.User.DTOs;
using del.Models;
using Domain.Entities;
using Domain.Entities.Models;
using Domain.Enums;
using Domain.Helper;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Features.User.Commands.Create.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand,BaseResponse<UserDTO>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISysUnitOfWork _sysUnitOfWork;
        private readonly HttpClient _httpClient;
        public CreateUserHandler(UserManager<ApplicationUser> userManager, ISysUnitOfWork sysUnitOfWork,IHttpClientFactory httpClient)
        {
            _userManager = userManager;
            _sysUnitOfWork = sysUnitOfWork;
            _httpClient= httpClient.CreateClient("ExternalApi");
        }
        public async Task<BaseResponse<UserDTO>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Name validation
            if (string.IsNullOrWhiteSpace(request._dto.FirstName))
            {
                return BaseResponse<UserDTO>.FailureResponse("الاسم الأول مطلوب");
            }

            if (string.IsNullOrWhiteSpace(request._dto.LastName))
            {
                return BaseResponse<UserDTO>.FailureResponse("الاسم الأخير مطلوب");
            }

            // Regex: حروف عربي + إنجليزي + مسافة فقط
            var nameRegex = new Regex(@"^[a-zA-Z\u0600-\u06FF\s]+$");

            if (!nameRegex.IsMatch(request._dto.FirstName))
            {
                return BaseResponse<UserDTO>.FailureResponse("الاسم الأول غير صالح");
            }

            if (!nameRegex.IsMatch(request._dto.LastName))
            {
                return BaseResponse<UserDTO>.FailureResponse("الاسم الأخير غير صالح");
            }

            // Length validation
            if (request._dto.FirstName.Length > 100 || request._dto.LastName.Length > 100)
            {
                return BaseResponse<UserDTO>.FailureResponse("الاسم طويل جداً");
            }

            // Password validation
            if (string.IsNullOrWhiteSpace(request._dto.Password) || request._dto.Password.Length < 8)
            {
                return BaseResponse<UserDTO>.FailureResponse("كلمة المرور يجب ألا تقل عن 8 أحرف");
            }

            var passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).+$");
            if (!passwordRegex.IsMatch(request._dto.Password))
            {
                return BaseResponse<UserDTO>.FailureResponse("كلمة المرور يجب أن تحتوي على حرف كبير وصغير ورقم ورمز");
            }

            // Confirm password
            if (request._dto.Password != request._dto.ConfirmPassword)
            {
                return BaseResponse<UserDTO>.FailureResponse("كلمتا المرور غير متطابقتين");
            }

            // Phone validation
            if (string.IsNullOrWhiteSpace(request._dto.PhoneNumber))
            {
                return BaseResponse<UserDTO>.FailureResponse("رقم الهاتف مطلوب");
            }

            var phoneRegex = new Regex(@"^\+?[0-9]\d{3,14}$");
            if (!phoneRegex.IsMatch(request._dto.PhoneNumber))
            {
                return BaseResponse<UserDTO>.FailureResponse("رقم الهاتف غير صحيح");
            }

            // WhatsApp validation (optional)
            if (!string.IsNullOrWhiteSpace(request._dto.WhatsappNumber) &&
                !phoneRegex.IsMatch(request._dto.WhatsappNumber))
            {
                return BaseResponse<UserDTO>.FailureResponse("رقم الواتساب غير صحيح");
            }
            var userPhone= await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request._dto.PhoneNumber);
            if (userPhone != null)
            {
                return BaseResponse<UserDTO>.FailureResponse("رقم الهاتف مستخدم بالفعل");
            }

            if (await _userManager.FindByEmailAsync(request._dto.Email) != null)
            {
                return BaseResponse<UserDTO>.FailureResponse("الايميل مستخدم بالفعل");
            }
            pers_person userInSys=null;
            if (request._dto.pin!="0")
            {

            
             userInSys = await _sysUnitOfWork.ISysPersonRepository.GetFirstOrderedByAsync(p => p.pin == request._dto.pin.ToString());

            if (userInSys is null)
                return BaseResponse<UserDTO>.BadRequestResponse("المستخدم لم يسجل علي جهاز البصمة");
            }
            var id = Guid.NewGuid();
            string userName = request._dto.FirstName + id.ToString("N")[..6];
            var emailRegex = new Regex(@"^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");
            bool isValid = emailRegex.IsMatch(request._dto.Email);
            if (!isValid)
            {
                return BaseResponse<UserDTO>.FailureResponse("البريد الإلكتروني غير صالح");
            }

            var newUser = new ApplicationUser
            {
                Id = id,
                UserName = Regex.Replace(userName, @"[^a-zA-Z0-9]", ""),
                Email = request._dto.Email,
                PhoneNumber = request._dto.PhoneNumber,
                FirstName = request._dto.FirstName,
                LastName = request._dto.LastName,
                Gender = request._dto.Gender,
                JobTitle = request._dto.JobTitle,
                AcademicTitle = request._dto.AcademicTitle,
                Organization = request._dto.Organization,
                Specialization = request._dto.Specialization,
                Skills = request._dto.Skills,
                WhatsappNumber = request._dto.WhatsappNumber,
                BirthDate = request._dto.BirthDate,
                NationalIdImage = request._dto.NationalIdImage,
                AddressInsideCairo = request._dto.AddressInsideCairo,
                AddressOutsideCairo = request._dto.AddressOutsideCairo,
                Doctrine = request._dto.Doctrine,
                MaritalState = request._dto.MaritalState,
                AcademicQualification = request._dto.AcademicQualification,
                Appreciation = request._dto.Appreciation,
                ImagePath = request._dto.ImagePath,
                IsActive = true
                ,
                pin=request._dto.pin.ToString()=="0"?null:request._dto.pin.ToString()

            };
            if (userInSys != null)
            {
                //update user in the sys database
                userInSys.name = newUser.FirstName;
                userInSys.last_name = newUser.LastName;
                userInSys.mobile_phone = newUser.PhoneNumber;
                userInSys.email = newUser.Email;
                userInSys.birthday = newUser.BirthDate;
            }

            var result = await _userManager.CreateAsync(newUser, request._dto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                var errorMessage = string.Join(" | ", errors);
                return BaseResponse<UserDTO>.FailureResponse($"فشل إنشاء المستخدم: {errorMessage}", errors);
            }
            result=await _userManager.AddToRolesAsync(newUser, request._dto.roles);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BaseResponse<UserDTO>.FailureResponse("فشل انشاء المستخدم", errors.ToList());
            }
            if (userInSys != null)
            {



                var newSysUser = new
                {
                    Pin = request._dto.pin.ToString(),
                    Name = newUser.FirstName,
                    LastName = newUser.LastName,
                    Email = newUser.Email,
                    Gender = newUser.Gender != Gender.male && newUser.Gender != Gender.female ? "M" : newUser.Gender.GetDescription(),
                    MobilePhone = newUser.PhoneNumber,
                   
                };
            var res = await _httpClient.PostAsJsonAsync(MainConstants.Use("person/add"), newSysUser);
            var success = await res.Content.ReadFromJsonAsync<ExternalApiResponse < List<string> >> ();
            if (success.Message == "false" || success.Code != 0)
            { 
               
                await _userManager.RemoveFromRolesAsync(newUser, request._dto.roles);
                await _userManager.DeleteAsync(newUser);
                    if (success.Message == "Repeated Password")

                        return BaseResponse<UserDTO>.FailureResponse("كلمة المرور الخاصة بمكينة البصمة موجود بالفعل,اختر كلمة أخري من فضلك");
                    else if(success.Message== "Mobile number already exists")
                        return BaseResponse<UserDTO>.FailureResponse("رقم الهاتف مستخدم بالفعل");

                    return BaseResponse<UserDTO>.FailureResponse("حاول مره أخري");

                }

            }
            return BaseResponse<UserDTO>.SuccessResponse(data:new UserDTO
            {
                Id = newUser.Id,
                UserName = newUser.UserName,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Gender =newUser.Gender,
                JobTitle =newUser.JobTitle,
                AcademicTitle =newUser.AcademicTitle,
                Organization =newUser.Organization,
                Specialization =newUser.Specialization,
                Skills =newUser.Skills,
                WhatsappNumber =newUser.WhatsappNumber,
                BirthDate = request._dto.BirthDate,
                NationalIdImage =newUser.NationalIdImage,
                AddressInsideCairo =newUser.AddressInsideCairo,
                AddressOutsideCairo =newUser.AddressOutsideCairo,
                Doctrine =newUser.Doctrine,
                MaritalState =newUser.MaritalState,
                AcademicQualification =newUser.AcademicQualification,
                Appreciation =newUser.Appreciation,
                ImagePath =newUser.ImagePath,
                pin = newUser.pin

            });
            }
    }
}
