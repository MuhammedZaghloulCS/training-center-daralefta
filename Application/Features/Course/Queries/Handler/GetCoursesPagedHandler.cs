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

            if (request.trainingId.HasValue)
            {
                Expression<Func<Domain.Entities.Course, bool>> trainingFilter = c => c.TrainingId == request.trainingId.Value && !c.IsDeleted;
                var (trainingItems, trainingTotalCount) = await _unitOfWork.ICourse.GetPaginatedAsync(
                    request.PageNumber,
                    request.PageSize,
                    trainingFilter,
                    c => c.Id,
                    true,
                    c => c.Sessions,
                    c => c.Training);
                if (trainingItems == null || !trainingItems.Any() || trainingItems.All(r => r.IsDeleted))
                {
                    return BaseResponse<List<CourseListDTO>>.SuccessResponse(
                        new List<CourseListDTO>(),
                        request.PageNumber,
                        request.PageSize,
                        trainingTotalCount,
                        "No course found for the specified training");
                }
                var trainingData = trainingItems.Select(c => new CourseListDTO
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
                    trainingData,
                    request.PageNumber,
                    request.PageSize,
                    trainingTotalCount,
                    "Courses retrieved successfully for the specified training");
            }

            Expression<Func<Domain.Entities.Course, bool>> filter = c => !c.IsDeleted;
            var (items, totalCount) = await _unitOfWork.ICourse.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                filter,
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
