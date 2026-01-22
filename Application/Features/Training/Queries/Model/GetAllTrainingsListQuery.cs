using Application.Common;
using Application.Features.Training.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Training.Queries.Model
{
    public class GetAllTrainingsListQuery : IRequest<BaseResponse<List<TrainingListDTO>>>
    {
    }
}
