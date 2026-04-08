using Application.Common;
using Application.Features.Course.DTOs;
using Application.Features.Training.DTOs;
using Application.Features.Training.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Training.Queries.Handler
{
    public class GetTrainingWithCoursesHandler : IRequestHandler<GetTrainingWithCoursesQuery, BaseResponse<Application.Features.Training.DTOs.TrainingWithCoursesDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetTrainingWithCoursesHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Application.Features.Training.DTOs.TrainingWithCoursesDto>> Handle(GetTrainingWithCoursesQuery request, CancellationToken cancellationToken)
        {
            var training = _unitOfWork.ITraining.GetTrainingWithAllCoursesAsync(request.TrainingId);

            if (training == null)
            {
                return BaseResponse<Application.Features.Training.DTOs.TrainingWithCoursesDto>.NotFoundResponse("التدريب مش موجود");
            }

            var response = training.Adapt<TrainingWithCoursesDto>();
            response.Courses = training.CoursesTrainings.Select(c => new CourseIdWithNameDto
            {
                Id = c.CourseId,
                Name = c.Course.Name
            }).ToList();
            return BaseResponse<Application.Features.Training.DTOs.TrainingWithCoursesDto>.SuccessResponse(response);
        }
    }
}
