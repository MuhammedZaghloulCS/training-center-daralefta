using Application.Common;
using Application.Features.User.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Helper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Json;
using System.Text;

namespace Application.Features.User.Commands.Create.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand,BaseResponse<UserDTO>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpClient _httpClient;
        public CreateUserHandler(UserManager<ApplicationUser> userManager,IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _httpClient = httpClientFactory.CreateClient("ExternalApi");
        }
        public async Task<BaseResponse<UserDTO>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            if (await _userManager.FindByEmailAsync(request._dto.Email) != null)
            {
                return BaseResponse<UserDTO>.FailureResponse("الايميل مستخدم بالفعل");
            }
                var id = Guid.NewGuid();
            string userName = request._dto.FirstName + id.ToString("N")[..6];
            var newUser = new ApplicationUser
            {
                Id = id,
                UserName = userName,
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


            };
            var result = await _userManager.CreateAsync(newUser, request._dto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BaseResponse<UserDTO>.FailureResponse("User creation failed", errors.ToList());
            }

            await _userManager.AddToRolesAsync(newUser, request._dto.roles);

           var lastUser = await _userManager.Users
    .Where(u => u.pin != null)
    .OrderByDescending(u => Convert.ToInt64(u.pin))
    .FirstOrDefaultAsync();

            var lastId = Convert.ToInt64(lastUser?.pin) + 1;
            var newSysUser = new ZkPersonCreateDto
            {
                Pin = lastId.ToString()??"0",
                Name = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Gender = newUser.Gender.GetDescription()[0],
                MobilePhone=newUser.PhoneNumber

            };
            var res=await _httpClient.PostAsJsonAsync(MainConstants.Use("person/add"), newSysUser);
            res.EnsureSuccessStatusCode();

            var res2 = await res.Content.ReadFromJsonAsync<ZkPersonCreateDto>();
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
                ImagePath =newUser.ImagePath

            });
            }
    }
}
