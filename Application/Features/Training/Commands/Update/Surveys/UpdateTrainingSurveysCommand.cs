using Application.Common;
using Application.Features.Training.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Training.Commands.Update.Surveys
{
    public class UpdateTrainingSurveysCommand : IRequest<BaseResponse<TrainingDto>>
    {
        public int Id { get; set; }
        public List<int> SurveyIds { get; set; }
    }
}
