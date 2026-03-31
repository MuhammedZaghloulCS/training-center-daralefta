using Application.Common;
using Application.Features.Course.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Course.Commands.Create
{
    public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, BaseResponse<CourseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCourseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<CourseDto>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("الاسم مطلوب");

            if (string.IsNullOrWhiteSpace(request.Description))
                errors.Add("الوصف مطلوب");

            if (string.IsNullOrWhiteSpace(request.Prerequisites))
                errors.Add("المتطلبات مطلوبة");

            if (request.Duration < 0)
                errors.Add("المدة مطلوبة");
            if (request.Duration > 120)
                errors.Add("المدة لا يمكن أن تتعدي 120 ساعة");

            if (request.TrainingId.HasValue && request.TrainingId.Value < 1)
                errors.Add("التدريب غير  معروف");

            if (errors.Any())
                return BaseResponse<CourseDto>.FailureResponse("Validation failed", errors);

            var course = new Domain.Entities.Course
            {
                CreatedBy="system",
                CreatedDate = DateTime.UtcNow,
                Name = request.Name,
                Description = request.Description,
                Prerequisites = request.Prerequisites,
                Duration = request.Duration,
                TrainingId = request.TrainingId
            };

            await _unitOfWork.ICourse.AddAsync(course);
            await _unitOfWork.Complete();

            var dto = new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Prerequisites = course.Prerequisites,
                Duration = course.Duration,
                TrainingId = course.TrainingId
            };

            return BaseResponse<CourseDto>.SuccessResponse(dto, "Course created successfully");
        }
    }
}
