using Application.Common;
using Application.Features.Training.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Training.Queries.Model
{
    public class GetTrainingsWithCoursesPagedQuery : IRequest<BaseResponse<List<TrainingWithCoursesDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchTerm { get; set; }
    }
}
