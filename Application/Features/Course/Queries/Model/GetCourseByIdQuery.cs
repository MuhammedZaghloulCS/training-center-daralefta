using Application.Common;
using Application.Features.Course.DTOs;
using MediatR;

namespace Application.Features.Course.Queries.Model
{
    public class GetCourseByIdQuery : IRequest<BaseResponse<CourseDto>>
    {
        public int Id { get; set; }
    }
}
