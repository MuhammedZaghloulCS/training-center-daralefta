using Application.Common;
using Application.Features.Course.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Course.Queries.Model
{
    public class GetAllCoursesListQuery : IRequest<BaseResponse<List<CourseListDTO>>>
    {
    }
}
