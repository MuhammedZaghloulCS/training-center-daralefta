using Application.Common;
using Application.Features.Course.DTOs;
using Application.Features.Course.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Course.Queries.Handler
{
    public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, BaseResponse<CourseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCourseByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var courses = await _unitOfWork.ICourse.FindRowAsync(c => c.Id == request.Id, c => c.Sessions);
            var course = courses.FirstOrDefault();

            if (course == null||course.IsDeleted)
            {
                return BaseResponse<CourseDto>.NotFoundResponse("Course not found");
            }

            var dto = new CourseDto
            {
                Id = course.Id,
                CreatedBy = course.CreatedBy,
                CreatedDate = course.CreatedDate,
                UpdatedBy = course.UpdatedBy,
                UpdatedAt = course.UpdatedAt,
                Name = course.Name,
                Description = course.Description,
                Prerequisites = course.Prerequisites,
                Duration = course.Duration,
            };

            return BaseResponse<CourseDto>.SuccessResponse(dto, "Course retrieved successfully");
        }
    }
}
