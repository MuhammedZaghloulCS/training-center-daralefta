using Application.Common;
using Application.Features.Course.DTOs;
using MediatR;

namespace Application.Features.Course.Commands.Update
{
    public class UpdateCourseCommand : IRequest<BaseResponse<CourseDto>>
    {
        public int Id { get; set; }
        public string? UpdatedBy { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Prerequisites { get; set; }
        public int Duration { get; set; }
        public int? TrainingId { get; set; }
    }
}
