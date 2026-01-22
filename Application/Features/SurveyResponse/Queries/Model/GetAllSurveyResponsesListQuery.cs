using Application.Common;
using Application.Features.SurveyResponse.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.SurveyResponse.Queries.Model
{
    public class GetAllSurveyResponsesListQuery : IRequest<BaseResponse<List<SurveyResponseListDTO>>>
    {
    }
}
