using Application.Common;
using Application.Features.Survey.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Survey.Queries.Model
{
    public class GetAllSpecifiedSurveysQuery :IRequest<BaseResponse<List<SurveyDto>>>
    {
    }
}
