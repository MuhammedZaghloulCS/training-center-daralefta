using Application.Common;
using Application.Features.Course.DTOs;
using Application.Features.Training.DTOs;
using Application.Features.Training.Queries.Model;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Features.Training.Queries.Handler
{
    public class GetTrainingsWithCoursesPagedHandler : IRequestHandler<GetTrainingsWithCoursesPagedQuery, BaseResponse<List<TrainingWithCoursesDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTrainingsWithCoursesPagedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<List<TrainingWithCoursesDto>>> Handle(GetTrainingsWithCoursesPagedQuery request, CancellationToken cancellationToken)
        {
           if (request == null)
            {
                return (BaseResponse<List<TrainingWithCoursesDto>>.BadRequestResponse("طلب غير صالح"));
            }

           if (request.PageNumber <= 0 || request.PageSize <= 0)
            {
                return (BaseResponse<List<TrainingWithCoursesDto>>.BadRequestResponse("رقم الصفحة وحجم الصفحة يجب أن يكونا أكبر من صفر"));
            }
           if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                Expression<Func<Domain.Entities.Training, bool>> searchExpression = t =>
                    t.Title.Contains(request.SearchTerm) ;

                var filteredTrainings = await _unitOfWork.ITraining.GetTrainingsWithAllCoursesPagedAsync(  request.PageSize,request.PageNumber, searchExpression);
                if (filteredTrainings.Item1 == null || filteredTrainings.totalNumber == 0)
                {
                    return BaseResponse<List<TrainingWithCoursesDto>>.NotFoundResponse("لم يتم العثور على أي دورات تدريبية مطابقة لمصطلح البحث");
                }
                List<TrainingWithCoursesDto> trainingsWithCoursesDtos = new List<TrainingWithCoursesDto>();
                foreach (var training in filteredTrainings.Item1)
                {
                    var response = training.Adapt<TrainingWithCoursesDto>();
                    response.Courses = training.CoursesTrainings.Select(c => new CourseIdWithNameDto
                    {
                        Id = c.CourseId,
                        Name = c.Course.Name
                    }).ToList();
                    trainingsWithCoursesDtos.Add(response);
                }
                return BaseResponse<List<TrainingWithCoursesDto>>.SuccessResponse(trainingsWithCoursesDtos, request.PageNumber, pageSize:request.PageSize,filteredTrainings.totalNumber);
            }

            var trainingsWithCourses = await _unitOfWork.ITraining.GetTrainingsWithAllCoursesPagedAsync(request.PageSize, request.PageNumber);
            if (trainingsWithCourses.Item1 == null || trainingsWithCourses.totalNumber== 0)
            {
                return BaseResponse<List<TrainingWithCoursesDto>>.NotFoundResponse("لم يتم العثور على أي دورات تدريبية مطابقة لمصطلح البحث");
            }
            List<TrainingWithCoursesDto> data = new List<TrainingWithCoursesDto>();

            foreach (var training in trainingsWithCourses.Item1)
            {
                var response = training.Adapt<TrainingWithCoursesDto>();
                response.Courses = training.CoursesTrainings.Select(c => new CourseIdWithNameDto
                {
                    Id = c.CourseId,
                    Name = c.Course.Name
                }).ToList();
                data.Add(response);
            }
            return BaseResponse<List<TrainingWithCoursesDto>>.SuccessResponse(data, request.PageNumber, request.PageSize,trainingsWithCourses.totalNumber );
        }
    }
}
