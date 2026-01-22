using Application.Common;
using MediatR;

namespace Application.Features.Training.Commands.Delete
{
    public class DeleteTrainingCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
}
