using Application.Common;
using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Features.Survey.Queries.Handler
{
    public class GetAllSurveysForUserByIdHandler : IRequestHandler<GetAllSurveysForUserByIdQuery, BaseResponse<List<SurveyDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSurveysForUserByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        async Task<BaseResponse<List<SurveyDto>>> IRequestHandler<GetAllSurveysForUserByIdQuery, BaseResponse<List<SurveyDto>>>.Handle(GetAllSurveysForUserByIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Domain.Entities.Survey, bool>> filter = s => s.IsActive;

            if (!string.IsNullOrEmpty(request.Search))
            {
                filter = s => (s.Name.Contains(request.Search) || s.Description.Contains(request.Search)) && s.IsActive ;
            }
            var surveyUser = await _unitOfWork.ISurveyUsers.GetAllSurveysForUserAsync(request.UserId,request.IsCompleted,includeProperties: su=>su.Survey);
            var surveys = surveyUser.Select(su => su.Survey).Where(filter.Compile()).ToList();
            var surveysDto = surveys.Adapt<List<SurveyDto>>();

            return BaseResponse<List<SurveyDto>>.SuccessResponse(surveysDto);
        }
    }
}
