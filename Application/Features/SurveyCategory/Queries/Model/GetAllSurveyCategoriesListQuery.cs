using Application.Common;
using Application.Features.SurveyCategory.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.SurveyCategory.Queries.Model
{
    public class GetAllSurveyCategoriesListQuery : IRequest<BaseResponse<List<SurveyCategoryListDTO>>>
    {
    }
}
