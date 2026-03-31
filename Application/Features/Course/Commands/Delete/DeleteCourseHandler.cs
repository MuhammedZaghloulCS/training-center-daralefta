using Application.Common;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Course.Commands.Delete
{
    public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.ICourse.GetByPkAsync(request.Id);
            if (course == null||course.IsDeleted)
            {
                return BaseResponse<bool>.NotFoundResponse("Course not found");
            }
            course.IsDeleted=true;
            _unitOfWork.ICourse.Update(course);
            var userCourse = await _unitOfWork.IUserCourseRepository.FindRowAsync(us => us.CourseId == request.Id);
            _unitOfWork.IUserCourseRepository.DeleteRange(userCourse);
            await _unitOfWork.Complete();

            return BaseResponse<bool>.SuccessResponse(true, "Deleted successfully");
        }
    }
}
