using Application.Common;
using Application.Features.SurveyCategory.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.SurveyCategory.Queries.Model
{
    public class GetSurveyCategoriesPagedQuery : IRequest<BaseResponse<List<SurveyCategoryListDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
