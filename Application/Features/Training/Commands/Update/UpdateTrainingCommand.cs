using Application.Common;
using Application.Features.Training.DTOs;
using MediatR;
using System;

namespace Application.Features.Training.Commands.Update
{
    public class UpdateTrainingCommand : IRequest<BaseResponse<TrainingDto>>
    {
        public int Id { get; set; }
        public string? UpdatedBy { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> CoursesIds { get; set; }

    }
}
