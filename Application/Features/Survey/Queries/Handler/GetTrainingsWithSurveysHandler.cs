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
    public class GetTrainingsWithSurveysHandler : IRequestHandler<GetTrainingsWithSurveysQuery, BaseResponse<List<TrainingWithSurveysDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTrainingsWithSurveysHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<TrainingWithSurveysDto>>> Handle(GetTrainingsWithSurveysQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<TrainingWithSurveysDto>>.BadRequestResponse("معلمات ترقيم الصفحات غير صالحة");
            }

            // Get all trainings that have surveys assigned
            var trainingSurveys = await _unitOfWork.ITrainingsSurveys.FindRowAsync(
                ts => ts.trainingId.HasValue
            );

            var trainingIds = trainingSurveys.Select(ts => ts.trainingId.Value).Distinct().ToList();

            // Get trainings
            var trainings = await _unitOfWork.ITraining.NewFindRowAsync(
                t => trainingIds.Contains(t.Id)
                ,
                                orderBy: s => s.CreatedDate, acsending: false
            );

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var searchLower = request.Search.ToLower();
                trainings = trainings.Where(t => t.Title.ToLower().Contains(searchLower)).ToList();
            }

            // Get response counts per training
            var trainingIdsFiltered = trainings.Select(t => t.Id).ToList();
            var responses = await _unitOfWork.ISurveyResponse.NewFindRowAsync(
                sr => sr.TrainingId.HasValue && trainingIdsFiltered.Contains(sr.TrainingId.Value),orderBy:t=>t.CreatedDate,acsending: false
            );

            // Build result
            var result = trainings.Select(t =>
            {
                var surveysForTraining = trainingSurveys.Where(ts => ts.trainingId == t.Id).ToList();
                var responsesForTraining = responses.Where(r => r.TrainingId == t.Id).ToList();

                return new TrainingWithSurveysDto
                {
                    TrainingId = t.Id,
                    TrainingTitle = t.Title,
                    SurveyCount = surveysForTraining.Select(ts => ts.surveyId).Distinct().Count(),
                    ResponseCount = responsesForTraining.Count
                };
            }).ToList();

            // Get total count for pagination
            var totalCount = result.Count;

            // Apply pagination
            var pagedResult = result
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return BaseResponse<List<TrainingWithSurveysDto>>.SuccessResponse(
                pagedResult,
                request.PageNumber,
                request.PageSize,
                totalCount,
                "تم تحميل التدريبات بنجاح"
            );
        }
    }
}
