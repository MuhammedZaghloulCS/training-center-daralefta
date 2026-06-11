using Application.Common;
using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Application.Features.Survey.Queries.Handler
{
    public class GetAllSpecifiedSurveyHandler : IRequestHandler<GetAllSpecifiedSurveysQuery, BaseResponse<List<SurveyDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSpecifiedSurveyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<List<SurveyDto>>> Handle(GetAllSpecifiedSurveysQuery request, CancellationToken cancellationToken)
        {
            var surveys = await _unitOfWork.ISurvey.FindRowAsync(predicate:s=>s.IsForSpecificUsers);
            var surveysDto = surveys.Adapt<List<SurveyDto>>();

            return BaseResponse<List<SurveyDto>>.SuccessResponse(surveysDto);
        }
    }
}
