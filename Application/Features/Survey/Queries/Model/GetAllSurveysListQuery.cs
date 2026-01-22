using Application.Common;
using Application.Features.Survey.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Survey.Queries.Model
{
    public class GetAllSurveysListQuery : IRequest<BaseResponse<List<SurveyListDTO>>>
    {
    }
}
