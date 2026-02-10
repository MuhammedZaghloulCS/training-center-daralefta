using Application.Common;
using Application.Features.User.DTOs;
using Application.Features.User.Queries.Model;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Features.User.Queries.Handler
{
    public class GetUserCourseRangePaginatedHandler : IRequestHandler<GetUsersCourseRangePaginatedQuery, BaseResponse<List<UserDTO>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public GetUserCourseRangePaginatedHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<List<UserDTO>>> Handle(
       GetUsersCourseRangePaginatedQuery request,
       CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.ICourse
                .GetByPkAsync(request.CourseId, t => t.UsersCourse);

            if (course is null)
                return BaseResponse<List<UserDTO>>
                    .NotFoundResponse("Course not found for the provided ID.");

            var userIds = course.UsersCourse?
                .Select(ut => ut.UserId)
                .ToList();

            if (userIds == null || !userIds.Any())
                return BaseResponse<List<UserDTO>>
                    .NotFoundResponse("No users assigned to this training.");

            var query = _userManager.Users
                .Where(u => userIds.Contains(u.Id));

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

            var users = await query.Where(search)
                .OrderBy(u => u.UserName) // مهم جدًا
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(user => new UserDTO
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
                    ImagePath = user.ImagePath
                }).ToListAsync();

            return BaseResponse<List<UserDTO>>
                .SuccessResponse(users);
        }
    }
}
