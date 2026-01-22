using Application.Common;
using Application.Features.SurveyResponse.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.SurveyResponse.Queries.Model
{
    public class GetSurveyResponsesPagedQuery : IRequest<BaseResponse<List<SurveyResponseListDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
