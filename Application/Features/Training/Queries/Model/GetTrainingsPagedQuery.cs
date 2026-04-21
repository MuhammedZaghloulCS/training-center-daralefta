using Application.Common;
using Application.Features.Training.DTOs;
using Infrastructure.Implementations.Repository;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Training.Queries.Model
{
    public class GetTrainingsPagedQuery : IRequest<BaseResponse<List<TrainingPagedDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; } = string.Empty;
    }
}
