using Application.Common;
using Application.Features.Course.DTOs;
using Application.Features.Course.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
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

            var (items, totalCount) = await _unitOfWork.ICourse.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                null,
                c => c.Id,
                true,
                c => c.Sessions,
                c => c.Training);

            if (items == null || !items.Any()||items.All(r=>r.IsDeleted))
            {
                return BaseResponse<List<CourseListDTO>>.SuccessResponse(
                    new List<CourseListDTO>(),
                    request.PageNumber,
                    request.PageSize,
                    totalCount,
                    "No course found");
            }

            var data = items.Where(r=>!r.IsDeleted).Select(c => new CourseListDTO
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

            return BaseResponse<List<CourseListDTO>>.SuccessResponse(
                data,
                request.PageNumber,
                request.PageSize,
                totalCount,
                "Courses retrieved successfully");
        }
    }
}
