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
    public class GetAllCoursesListHandler : IRequestHandler<GetAllCoursesListQuery, BaseResponse<List<CourseListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCoursesListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<CourseListDTO>>> Handle(GetAllCoursesListQuery request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.ICourse.FindRowAsync(c=>c.IsDeleted==false,c => c.Sessions);

            if (response == null || !response.Any()||response.All(r=>r.IsDeleted))
            {
                return BaseResponse<List<CourseListDTO>>.SuccessResponse(
                    new List<CourseListDTO>(),
                    "No course found"
                );
            }

            var data = response.OrderByDescending(c=>c.CreatedDate).Where(r=>!r.IsDeleted).Select(c => new CourseListDTO
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
              
            }).ToList();

            return BaseResponse<List<CourseListDTO>>.SuccessResponse(
                data,
                "Courses retrieved successfully"
            );
        }
    }
}
