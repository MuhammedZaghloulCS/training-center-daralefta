using Application.Common;
using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Survey.Queries.Handler
{
    public class GetCompletedStudentSurveysHandler : IRequestHandler<GetCompletedStudentSurveysQuery, BaseResponse<List<StudentSurveyDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _context;

        public GetCompletedStudentSurveysHandler(IUnitOfWork unitOfWork, ApplicationContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<BaseResponse<List<StudentSurveyDto>>> Handle(GetCompletedStudentSurveysQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<StudentSurveyDto>>.BadRequestResponse("معلومات ترقيم الصفحات غير صالحة");
            }

            var userId = request.UserId;

            // Get all trainings the student is enrolled in
            var userTrainings = await _unitOfWork.IUserTrainingRepository.FindRowAsync(
                ut => ut.UserId == userId,
                ut => ut.Training
            );

            var trainingIds = userTrainings.Select(ut => ut.TrainingId).ToList();

            // Get all surveys for these trainings
            var trainingSurveys = await _unitOfWork.ITrainingsSurveys.FindAsync(
                ts => trainingIds.Contains(ts.trainingId.Value)
            );

            var surveyIds = trainingSurveys.Select(ts => ts.surveyId.Value).Distinct().ToList();

            // Get surveys with questions
            var surveys = await _unitOfWork.ISurvey.NewFindRowAsync(
                s =>  s.IsActive,
                orderBy: s => s.CreatedAt, acsending: false,

                s => s.Questions
            );

            // Get user's existing responses
            var existingResponses = await _unitOfWork.ISurveyResponse.NewFindRowAsync(
                sr => sr.UserId == userId,
                                orderBy: s => s.CreatedDate, acsending: false

            );
            var respondedSurveyIds = existingResponses.Select(sr => sr.SurveyId).ToHashSet();
            
            // Filter to only completed surveys (responded)
            var completedSurveys = surveys.Where(s => respondedSurveyIds.Contains(s.Id)).ToList();

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var searchLower = request.Search.ToLower().Trim();

                completedSurveys = completedSurveys.Where(s =>
                {
                    // Fix: استخدم .HasValue و .Value للمقارنة الصح مع nullable
                    var ts = trainingSurveys.FirstOrDefault(t => t.surveyId.HasValue && t.surveyId.Value == s.Id);

                    var training = ts != null && ts.trainingId.HasValue
                        ? userTrainings.FirstOrDefault(ut => ut.TrainingId == ts.trainingId.Value)?.Training
                        : null;

                    var nameMatch = s.Name?.ToLower().Contains(searchLower) ?? false;
                    var trainingMatch = training?.Title?.ToLower().Contains(searchLower) ?? false;

                    return nameMatch || trainingMatch;
                }).ToList();
            }

            // Get total count for pagination
            var totalCount = completedSurveys.Count;

            // Apply pagination
            var pagedSurveys = completedSurveys
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            // Build result
            var result = pagedSurveys.Select(s =>
            {
                var ts = trainingSurveys.FirstOrDefault(t => t.surveyId.HasValue && t.surveyId.Value == s.Id);
                var training = ts != null && ts.trainingId.HasValue
                    ? userTrainings.FirstOrDefault(ut => ut.TrainingId == ts.trainingId.Value)?.Training
                    : null;

                return new StudentSurveyDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    QuestionCount = s.Questions?.Count ?? 0,
                    HasResponded = true,
                    TrainingId = ts?.trainingId,
                    TrainingName = training?.Title
                };
            }).ToList();

            return BaseResponse<List<StudentSurveyDto>>.SuccessResponse(
                result,
                request.PageNumber,
                request.PageSize,
                totalCount,
                "تم تحميل الاستبيانات المكتملة بنجاح"
            );
        }
    }
}