using Application.Common;
using Application.Features.Training.DTOs;
using MediatR;

namespace Application.Features.Training.Queries.Model
{
    public class GetTrainingByIdQuery : IRequest<BaseResponse<TrainingDto>>
    {
        public int Id { get; set; }
    }
}
