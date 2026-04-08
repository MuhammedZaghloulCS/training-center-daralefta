using Application.Common;
using Application.Features.Course.DTOs;
using MediatR;

namespace Application.Features.Course.Commands.Create
{
    public class CreateCourseCommand : IRequest<BaseResponse<CourseDto>>
    {
 
        public string Name { get; set; }
        public string Description { get; set; }
        public string Prerequisites { get; set; }
        public int Duration { get; set; }
    
        public string? CreatedBy { get; set; }
    }
}
