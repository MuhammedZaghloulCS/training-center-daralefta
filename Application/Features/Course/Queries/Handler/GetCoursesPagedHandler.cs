using Application.Common;
using Application.Features.Course.DTOs;
using Application.Features.Course.Queries.Model;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Course.Queries.Handler
{
    public class GetCoursesPagedHandler : IRequestHandler<GetCoursesPagedQuery, BaseResponse<List<CourseListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCoursesPagedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<CourseListDTO>>> Handle(GetCoursesPagedQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<CourseListDTO>>.BadRequestResponse("Invalid pagination parameters");
            }

         

            Expression<Func<Domain.Entities.Course, bool>> filter = c => !c.IsDeleted;

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                filter = c => !c.IsDeleted && (c.Name.Contains(request.SearchTerm) || c.Description.Contains(request.SearchTerm)||c.Duration.ToString()==request.SearchTerm);
            }


            var (items, totalCount) = await _unitOfWork.ICourse.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                filter,
                c => c.CreatedDate,
                false
                );

            if (items == null || !items.Any()||items.All(r=>r.IsDeleted))
            {
                return BaseResponse<List<CourseListDTO>>.SuccessResponse(
                    new List<CourseListDTO>(),
                    request.PageNumber,
                    request.PageSize,
                    totalCount,
                    "No course found");
            }

            var data = items.Select(c => new CourseListDTO
            {
                Id = c.Id,
                CreatedBy = c.CreatedBy,
                CreatedDate = c.CreatedDate,
                UpdatedBy = c.UpdatedBy,
                UpdatedAt = c.UpdatedAt,
                Name = c.Name,
                Description = c.Description,
                Prerequisites = c.Prerequisites,
                Duration = c.Duration
            }).ToList();

            return BaseResponse<List<CourseListDTO>>.SuccessResponse(
                data,
                request.PageNumber,
                request.PageSize,
                totalCount,
                "Courses retrieved successfully");
        }
    }
}
