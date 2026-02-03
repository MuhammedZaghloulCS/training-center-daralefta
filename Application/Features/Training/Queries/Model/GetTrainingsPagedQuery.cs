using Application.Common;
using Application.Features.Training.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Training.Queries.Model
{
    public class GetTrainingsPagedQuery : IRequest<BaseResponse<List<TrainingListDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; } = string.Empty;
    }
}
