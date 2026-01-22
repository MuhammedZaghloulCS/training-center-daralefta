using Application.Common;
using Application.Features.Survey.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Survey.Queries.Model
{
    public class GetSurveysPagedQuery : IRequest<BaseResponse<List<SurveyListDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
