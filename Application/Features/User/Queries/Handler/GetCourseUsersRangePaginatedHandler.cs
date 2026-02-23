using Application.Common;
using Application.Features.Course.DTOs;
using Application.Features.Training.DTOs;
using Application.Features.User.DTOs;
using Application.Features.User.Queries.Model;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Handler
{
    public class GetCourseUsersRangePaginatedHandler : IRequestHandler<GetCourseUsersRangePaginatedQuery, BaseResponse<List<CourseDto>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public GetCourseUsersRangePaginatedHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<List<CourseDto>>> Handle(GetCourseUsersRangePaginatedQuery request, CancellationToken cancellationToken)
        {

            var userExist= _userManager.Users.FirstOrDefault(u => u.Id == request.UserId&&!u.IsDeleted);

            if (userExist is null)
                return BaseResponse<List<CourseDto>>.NotFoundResponse("User not found for the provided ID.");


             var CourseIds = await _unitOfWork.IAssignUserCourse
                .GetCourseIdsByUserId(request.UserId);



            
            if (CourseIds == null || !CourseIds.Any())
                return BaseResponse<List<CourseDto>>
                    .NotFoundResponse("No Course assigned to this training.");

            var query=await _unitOfWork.ICourse
                .FindRowAsync(s=>CourseIds.Contains(s.Id));

           var CourseDtos = query
                .OrderBy(u => u.Name)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(c => new CourseDto 
            {
                    Id = c.Id,
                    CreatedBy = c.CreatedBy,
                    CreatedDate = c.CreatedDate,
                    UpdatedBy = c.UpdatedBy,
                    UpdatedAt = c.UpdatedAt,
                    Name = c.Name,
                    Description = c.Description,
                    Prerequisites = c.Prerequisites,
                    Duration = c.Duration,
                    TrainingId = c.TrainingId

                }).ToList();
            return BaseResponse<List<CourseDto>>.SuccessResponse(CourseDtos);
        }
    }
}
