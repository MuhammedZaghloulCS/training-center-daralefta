using Application.Common;
using Application.Features.Course.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Course.Commands.Update
{
    public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand, BaseResponse<CourseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCourseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<CourseDto>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.ICourse.GetByPkAsync(request.Id);
            if (course == null)
            {
                return BaseResponse<CourseDto>.NotFoundResponse("Course not found");
            }

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("Name is required");

            if (string.IsNullOrWhiteSpace(request.Description))
                errors.Add("Description is required");

            if (string.IsNullOrWhiteSpace(request.Prerequisites))
                errors.Add("Prerequisites is required");

            if (request.Duration < 0)
                errors.Add("Duration is invalid");

            if (request.TrainingId.HasValue && request.TrainingId.Value < 1)
                errors.Add("TrainingId is invalid");

            if (errors.Any())
                return BaseResponse<CourseDto>.FailureResponse("Validation failed", errors);

            course.Name = request.Name;
            course.Description = request.Description;
            course.Prerequisites = request.Prerequisites;
            course.Duration = request.Duration;
            course.TrainingId = request.TrainingId;
            course.UpdatedBy = request.UpdatedBy;
            course.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.ICourse.Update(course);
            await _unitOfWork.Complete();

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
                TrainingId = course.TrainingId
            };

            return BaseResponse<CourseDto>.SuccessResponse(dto, "Course updated successfully");
        }
    }
}
