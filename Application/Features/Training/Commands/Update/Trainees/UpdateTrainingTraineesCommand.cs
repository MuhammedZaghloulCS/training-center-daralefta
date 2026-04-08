using Application.Common;
using Application.Features.Training.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Training.Commands.Update.Trainees
{
      public class UpdateTrainingTraineesCommand : IRequest<BaseResponse<TrainingDto>>
    {
        public int Id { get; set; }
        public List<Guid> UserIds { get; set; }

    }
}

