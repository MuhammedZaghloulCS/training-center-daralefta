using Application.Common;
using Application.Features.Training.DTOs;
using Application.Features.User.DTOs;
using Application.Features.User.Queries.Model;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using Infrastructure.Implementations.UnitOfWork.SysUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Features.User.Queries.Handler
{
    public class GetAllUsersPagedHandler : IRequestHandler<GetAllUsersPagedQuery, BaseResponse<List<UserDTO>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISysUnitOfWork sysUnitOfWork;
        public GetAllUsersPagedHandler(UserManager<ApplicationUser> userManager,ISysUnitOfWork sysUnitOfWork)
        {
            _userManager = userManager;
            this.sysUnitOfWork = sysUnitOfWork;
        }
        public async Task<BaseResponse<List<UserDTO>>> Handle(GetAllUsersPagedQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<UserDTO>>.BadRequestResponse("Invalid pagination parameters");
            }

            Expression<Func<ApplicationUser, bool>> search = u => true;

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var term = request.Search.Trim();

                // Enum parsing
                bool personTypeParsed = Enum.TryParse<PersonType>(term, true, out var personType);
                bool genderParsed = Enum.TryParse<Gender>(term, true, out var gender);

                // Date parsing
                bool dateParsed = DateTime.TryParse(term, out var birthDate);

                search = u =>
                    // Identity
                    u.UserName.Contains(term) ||
                    u.Email.Contains(term) ||
                    u.PhoneNumber.Contains(term) ||

                    // Basic info
                    u.FirstName.Contains(term) ||
                    u.LastName.Contains(term) ||

                    // Enums
                   
                    (genderParsed && u.Gender == gender) ||

                    // Professional info
                    u.JobTitle.Contains(term) ||
                    u.AcademicTitle.Contains(term) ||
                    u.Organization.Contains(term) ||
                    u.Specialization.Contains(term) ||
                    u.Skills.Contains(term) ||

                    // Contact & address
                    u.WhatsappNumber.Contains(term) ||
                    u.AddressInsideCairo.Contains(term) ||
                    u.AddressOutsideCairo.Contains(term) ||

                    // Other info
                    u.Doctrine.Contains(term) ||
                    u.MaritalState.Contains(term) ||
                    u.AcademicQualification.Contains(term) ||
                    u.Appreciation.Contains(term) ||

                    // Dates
                    (dateParsed && u.BirthDate.HasValue &&
                     u.BirthDate.Value.Date == birthDate.Date);
                    
            }

            var usersQuery = _userManager.Users.Where(u=>!u.IsDeleted).AsNoTracking();
            var pins   = await usersQuery.Select(u => u.pin).ToListAsync();

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
            IEnumerable<ApplicationUser> users;

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                usersQuery = usersQuery.Where(search);
            }
          
                 users = await usersQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).AsNoTracking().ToListAsync();
            
            
            if (users == null||users.All(r=>r.IsDeleted))
            {
                return BaseResponse<List<UserDTO>>.FailureResponse("No users found");
            }
            else if (users.Count() == 0)
            {
                return BaseResponse<List<UserDTO>>.SuccessResponse(data: default, message: "No users found");
            }
            var userDTOs = users.Where(r => !r.IsDeleted).Select(u => new UserDTO
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
               

            }).ToList();
            if (inCompleteUsers.Count() > 0)
            {
                userDTOs.InsertRange(0, inCompleteUsers);
                
            }
            return BaseResponse<List<UserDTO>>.SuccessResponse(data: userDTOs, message: "Users retrieved successfully");
        }
    }
}
