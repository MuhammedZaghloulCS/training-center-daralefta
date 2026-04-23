using Application.Common;
using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Survey.Queries.Handler
{
    public class GetSurveysForTrainingHandler : IRequestHandler<GetSurveysForTrainingQuery, BaseResponse<List<SurveySummaryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSurveysForTrainingHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<SurveySummaryDto>>> Handle(GetSurveysForTrainingQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<SurveySummaryDto>>.BadRequestResponse("معلمات ترقيم الصفحات غير صالحة");
            }

            // Get surveys for this training
            var trainingSurveys = await _unitOfWork.ITrainingsSurveys.FindRowAsync(
                ts => ts.trainingId == request.TrainingId
            );

            var surveyIds = trainingSurveys.Select(ts => ts.surveyId).Distinct().ToList();

            // Get surveys with questions
            var surveys = await _unitOfWork.ISurvey.NewFindRowAsync(
                s => surveyIds.Contains(s.Id), orderBy: ts => ts.CreatedAt,acsending: false, includeProperties:
                s => s.Questions
            );

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var searchLower = request.Search.ToLower();
                surveys = surveys.Where(s => s.Name.ToLower().Contains(searchLower)).ToList();
            }

            // Get response counts
            var responses = await _unitOfWork.ISurveyResponse.NewFindRowAsync(
                sr => sr.TrainingId == request.TrainingId && surveyIds.Contains(sr.SurveyId),
                
                                orderBy: s => s.CreatedDate, acsending: false
            );

            // Build result
            var result = surveys.Select(s =>
            {
                var responseCount = responses.Count(r => r.SurveyId == s.Id);
                var latestResponse = responses.Where(r => r.SurveyId == s.Id).Max(r => (DateTime?)r.SubmittedAt);

                return new SurveySummaryDto
                {
                    SurveyId = s.Id,
                    SurveyName = s.Name,
                    QuestionCount = s.Questions?.Count ?? 0,
                    ResponseCount = responseCount,
                    CreatedAt = s.CreatedAt,
                    LatestResponseAt = latestResponse
                };
            }).OrderByDescending(s => s.LatestResponseAt ?? DateTime.MinValue).ToList();

            // Get total count for pagination
            var totalCount = result.Count;

            // Apply pagination
            var pagedResult = result
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return BaseResponse<List<SurveySummaryDto>>.SuccessResponse(
                pagedResult,
                request.PageNumber,
                request.PageSize,
                totalCount,
                "تم تحميل الاستبيانات بنجاح"
            );
        }
    }
}
