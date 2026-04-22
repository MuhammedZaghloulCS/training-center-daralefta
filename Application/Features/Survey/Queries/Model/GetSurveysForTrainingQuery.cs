using Application.Common;
using Application.Features.Survey.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Survey.Queries.Model
{
    public class GetSurveysForTrainingQuery : IRequest<BaseResponse<List<SurveySummaryDto>>>
    {
        public int TrainingId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
    }
}
