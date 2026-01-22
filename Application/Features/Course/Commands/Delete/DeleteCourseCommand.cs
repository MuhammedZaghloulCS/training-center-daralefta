using Application.Common;
using MediatR;

namespace Application.Features.Course.Commands.Delete
{
    public class DeleteCourseCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
}
