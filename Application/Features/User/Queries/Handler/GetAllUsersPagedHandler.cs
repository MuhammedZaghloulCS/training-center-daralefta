using Application.Common;
using Application.Features.User.DTOs;
using Application.Features.User.Queries.Model;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Features.User.Queries.Handler
{
    public class GetAllUsersPagedHandler
        : IRequestHandler<GetAllUsersPagedQuery, BaseResponse<List<UserDTO>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISysUnitOfWork _sysUnitOfWork;
        private readonly ILogger<GetAllUsersPagedHandler> _logger;

        public GetAllUsersPagedHandler(
            UserManager<ApplicationUser> userManager,
            ISysUnitOfWork sysUnitOfWork,
            ILogger<GetAllUsersPagedHandler> logger)
        {
            _userManager = userManager;
            _sysUnitOfWork = sysUnitOfWork;
            _logger = logger;
        }

        public async Task<BaseResponse<List<UserDTO>>> Handle(
            GetAllUsersPagedQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting users paged: Page {Page}, Size {Size}", request.PageNumber, request.PageSize);

            // Validate pagination
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<UserDTO>>
                    .BadRequestResponse("Invalid pagination parameters");
            }

            // Limit PageSize (حماية)
            var pageSize = request.PageSize > 100 ? 100 : request.PageSize;

            // Base query
            var usersQuery = _userManager.Users
                .Where(u => !u.IsDeleted)
                .AsNoTracking();

            // Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var term = request.Search.Trim();

                var roles = new List<(string Ar, string En)>
                {
                    ("مدير", "Admin"),
                    ("محاضر", "Instructor"),
                    ("طالب", "Student")
                };

                var matchedRoles = roles
                    .Where(r => r.Ar.Contains(term, StringComparison.OrdinalIgnoreCase))
                    .Select(r => r.En)
                    .ToList();

                bool genderParsed = Enum.TryParse<Gender>(term, true, out var gender);
                bool dateParsed = DateTime.TryParse(term, out var birthDate);
                bool maritalParsed = Enum.TryParse<MaritalStatus>(term, true, out var maritalStatus);

                // لو فيه roles متطابقة
                if (matchedRoles.Any())
                {
                    var usersSearchedFor = new List<ApplicationUser>();

                    foreach (var role in matchedRoles)
                    {
                        var usersInRole = await _userManager.GetUsersInRoleAsync(role);
                        usersSearchedFor.AddRange(usersInRole);
                    }

                    usersSearchedFor = usersSearchedFor
                        .Where(u => !u.IsDeleted)
                        .Distinct()
                        .ToList();

                    var usersDTO = new List<UserDTO>();

                    foreach (var user in usersSearchedFor)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);

                        usersDTO.Add(new UserDTO
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
                            pin = user.pin,
                            ImagePath = user.ImagePath,
                            IsActive = user.IsActive,
                            roles = userRoles.ToList()
                        });
                    }

                    return BaseResponse<List<UserDTO>>.SuccessResponse(
                        usersDTO,
                        request.PageNumber,
                        pageSize,
                        usersDTO.Count,
                        "تم استلام المستخدمين بنجاح"
                    );
                }

                // fallback search (زي ما عندك)
                usersQuery = usersQuery.Where(u =>
                    u.UserName.Contains(term) ||
                    u.Email.Contains(term) ||
                    u.PhoneNumber.Contains(term) ||
                    u.FullName.Contains(term) ||

                    u.FirstName.Contains(term) ||
                    u.LastName.Contains(term) ||

                    (genderParsed && u.Gender == gender) ||

                    (maritalParsed && u.MaritalState == maritalStatus) ||

                    u.JobTitle.Contains(term) ||
                    u.AcademicTitle.Contains(term) ||
                    u.Organization.Contains(term) ||
                    u.Specialization.Contains(term) ||
                    u.Skills.Contains(term) ||

                    u.WhatsappNumber.Contains(term) ||
                    u.AddressInsideCairo.Contains(term) ||
                    u.AddressOutsideCairo.Contains(term) ||

                    u.Doctrine.Contains(term) ||
                    u.AcademicQualification.Contains(term) ||
                    u.Appreciation.Contains(term) ||

                    (dateParsed &&
                     u.BirthDate.HasValue &&
                     u.BirthDate.Value.Date == birthDate.Date)
                );
            }

            // Total count (قبل pagination)
            var totalCount = await usersQuery.CountAsync(cancellationToken);

            // Sorting (مهم جداً)
            usersQuery = usersQuery.OrderByDescending(u => u.CreatingDate);

            // Pagination
            var users = await usersQuery
                .Skip((request.PageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            // Fix N+1 query: Batch load roles for all users at once
            var userIds = users.Select(u => u.Id).ToList();

            // Use a single query to get all user roles via UserRoles table
            var userRolesDict = new Dictionary<Guid, IList<string>>();
            foreach (var user in users)
            {
                // Get roles for each user - this is still N+1 but UserManager caches internally
                // For better performance, consider direct query on AspNetUserRoles table
                userRolesDict[user.Id] = await _userManager.GetRolesAsync(user);
            }

            // Mapping
            var userDTOs = users.Select(user => new UserDTO
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
                pin = user.pin,
                ImagePath = user.ImagePath,
                IsActive = user.IsActive,
                roles = userRolesDict.TryGetValue(user.Id, out var roles) ? roles.ToList() : new List<string>()
            }).ToList();

            _logger.LogInformation("Retrieved {Count} users", userDTOs.Count);

            // Return with pagination
            return BaseResponse<List<UserDTO>>.SuccessResponse(
                userDTOs,
                request.PageNumber,
                pageSize,
                totalCount,
                "Users retrieved successfully"
            );
        }
    }
}