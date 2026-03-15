using Application.Common;
using Application.Features.Training.DTOs;
using MediatR;
using System;

namespace Application.Features.Training.Commands.Create
{
    public class CreateTrainingCommand : IRequest<BaseResponse<TrainingDto>>
    {
        public string CreatedBy { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> CoursesIds { get; set; }
    }
}
