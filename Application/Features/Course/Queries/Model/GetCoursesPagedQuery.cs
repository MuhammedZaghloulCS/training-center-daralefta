using Application.Common;
using Application.Features.Course.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Course.Queries.Model
{
    public class GetCoursesPagedQuery : IRequest<BaseResponse<List<CourseListDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        
        public string? SearchTerm { get; set; }

    }
}
