using Application.Common;
using Application.Features.User.DTOs;
using Application.Features.User.Queries.Model;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.User.Queries.Handler
{
    public class GetAllUsersPagedHandler 
        : IRequestHandler<GetAllUsersPagedQuery, BaseResponse<List<UserDTO>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISysUnitOfWork _sysUnitOfWork;

        public GetAllUsersPagedHandler(
            UserManager<ApplicationUser> userManager,
            ISysUnitOfWork sysUnitOfWork)
        {
            _userManager = userManager;
            _sysUnitOfWork = sysUnitOfWork;
        }

        public async Task<BaseResponse<List<UserDTO>>> Handle(
            GetAllUsersPagedQuery request,
            CancellationToken cancellationToken)
        {
            // ✅ Validate pagination
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<UserDTO>>
                    .BadRequestResponse("Invalid pagination parameters");
            }

            // ✅ Base query
            var usersQuery = _userManager.Users
                .Where(u => !u.IsDeleted)
                .AsNoTracking();

            // ✅ Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var term = request.Search.Trim();

                bool genderParsed = Enum.TryParse<Gender>(term, true, out var gender);
                bool dateParsed = DateTime.TryParse(term, out var birthDate);

                usersQuery = usersQuery.Where(u =>
                    // Identity
                    u.UserName.Contains(term) ||
                    u.Email.Contains(term) ||
                    u.PhoneNumber.Contains(term) ||

                    // Basic
                    u.FirstName.Contains(term) ||
                    u.LastName.Contains(term) ||

                    // Enum
                    (genderParsed && u.Gender == gender) ||

                    // Professional
                    u.JobTitle.Contains(term) ||
                    u.AcademicTitle.Contains(term) ||
                    u.Organization.Contains(term) ||
                    u.Specialization.Contains(term) ||
                    u.Skills.Contains(term) ||

                    // Contact
                    u.WhatsappNumber.Contains(term) ||
                    u.AddressInsideCairo.Contains(term) ||
                    u.AddressOutsideCairo.Contains(term) ||

                    // Other
                    u.Doctrine.Contains(term) ||
                    u.MaritalState.Contains(term) ||
                    u.AcademicQualification.Contains(term) ||
                    u.Appreciation.Contains(term) ||

                    // Date
                    (dateParsed &&
                     u.BirthDate.HasValue &&
                     u.BirthDate.Value.Date == birthDate.Date)
                );
            }

            // ✅ Pagination
            var users = await usersQuery
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            // ✅ No data
            if (!users.Any())
            {
                return BaseResponse<List<UserDTO>>
                    .SuccessResponse(new List<UserDTO>(), "No users found");
            }

            // ✅ Mapping
            var userDTOs = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userDTOs.Add(new UserDTO
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

                    BirthDate = user.BirthDate ?? DateTime.MinValue,
                    NationalIdImage = user.NationalIdImage,

                    AddressInsideCairo = user.AddressInsideCairo,
                    AddressOutsideCairo = user.AddressOutsideCairo,

                    Doctrine = user.Doctrine,
                    MaritalState = user.MaritalState,
                    AcademicQualification = user.AcademicQualification,
                    Appreciation = user.Appreciation,

                    ImagePath = user.ImagePath,

                    roles = roles.ToList()
                });
            }

            // ✅ Response
            return BaseResponse<List<UserDTO>>
                .SuccessResponse(userDTOs, "Users retrieved successfully");
        }
    }
}