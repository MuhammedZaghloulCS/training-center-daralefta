using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Training.Queries.Model
{
    public class GetTrainingWithCoursesQuery : IRequest<BaseResponse<Application.Features.Training.DTOs.TrainingWithCoursesDto>>
    {
        public int TrainingId { get; set; }
    }
}
