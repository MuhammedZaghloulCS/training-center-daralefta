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

            if (await _userManager.FindByEmailAsync(request._dto.Email) != null)
            {
                return BaseResponse<UserDTO>.FailureResponse("الايميل مستخدم بالفعل");
            }

            pers_person userInSys = await _sysUnitOfWork.ISysPersonRepository.GetFirstOrderedByAsync(p => p.pin == request._dto.pin.ToString());

            if (userInSys is null)
                return BaseResponse<UserDTO>.BadRequestResponse("المستخدم لم يسجل علي جهاز البصمة");

                var id = Guid.NewGuid();
            string userName = request._dto.FirstName + id.ToString("N")[..6];
            //var lastSysPerson = await _sysUnitOfWork.ISysPersonRepository.GetFirstOrderedByAsync<int>(p => Convert.ToInt32(p.pin), descending: true);

            //var lastUser = await _userManager.Users
            //.Where(u => u.pin != null)
            //.OrderByDescending(u => Convert.ToInt64(u.pin))
            //.FirstOrDefaultAsync();

            //int sysPin = int.TryParse(lastSysPerson?.pin, out var s) ? s : 0;
            //int userPin = int.TryParse(lastUser?.pin, out var u) ? u : 0;

            //var lastId = Math.Max(sysPin, userPin) + 1;
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
                ImagePath = request._dto.ImagePath
                ,pin=request._dto.pin.ToString()

            };
            //update user in the sys database
            userInSys.name = newUser.FirstName;
            userInSys.last_name = newUser.LastName;
            userInSys.mobile_phone  = newUser.PhoneNumber;
            userInSys.email= newUser.Email;
            userInSys.birthday = newUser.BirthDate;


            var result = await _userManager.CreateAsync(newUser);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BaseResponse<UserDTO>.FailureResponse("User creation failed", errors.ToList());
            }
            result=await _userManager.AddToRolesAsync(newUser, request._dto.roles);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BaseResponse<UserDTO>.FailureResponse("User creation failed", errors.ToList());
            }


            var newSysUser = new ZkPersonCreateDto
            {
                Pin = request._dto.pin.ToString() ,
                Name = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Gender = newUser.Gender != Gender.male && newUser.Gender != Gender.female ? "M" : newUser.Gender.GetDescription(),
                MobilePhone = newUser.PhoneNumber

            };
            var res = await _httpClient.PostAsJsonAsync(MainConstants.Use("person/add"), newSysUser);
            var success = await res.Content.ReadFromJsonAsync<ExternalApiResponse < List<string> >> ();
            if (success.Message == "false" || success.Code != 0)
            { 
               
                await _userManager.RemoveFromRolesAsync(newUser, request._dto.roles);
                await _userManager.DeleteAsync(newUser);
                return BaseResponse<UserDTO>.FailureResponse("Failed to edit the users");
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
