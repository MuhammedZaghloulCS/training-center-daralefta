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
    public class GetPendingStudentSurveysHandler : IRequestHandler<GetPendingStudentSurveysQuery, BaseResponse<List<StudentSurveyDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPendingStudentSurveysHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<StudentSurveyDto>>> Handle(GetPendingStudentSurveysQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<StudentSurveyDto>>.BadRequestResponse("Ù Ø¹Ù Ù Ø§Øª ØªØ±Ù Ù Ù Ø§ÙØµÙ Ø­Ø§Øª ØºÙ Ø± ØµØ§Ù ØØ©");
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
            var surveys = await _unitOfWork.ISurvey.FindRowAsync(
                s => surveyIds.Contains(s.Id) && s.IsActive,
                s => s.Questions
            );

            // Get user's existing responses
            var existingResponses = await _unitOfWork.ISurveyResponse.FindRowAsync(
                sr => sr.UserId == userId
            );
            var respondedSurveyIds = existingResponses.Select(sr => sr.SurveyId).ToHashSet();

            // Filter to only pending surveys (not responded)
            var pendingSurveys = surveys.Where(s => !respondedSurveyIds.Contains(s.Id)).ToList();

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var searchLower = request.Search.ToLower();
                pendingSurveys = pendingSurveys.Where(s =>
                {
                    var ts = trainingSurveys.FirstOrDefault(t => t.surveyId == s.Id);
                    var training = userTrainings.FirstOrDefault(ut => ut.TrainingId == ts?.trainingId)?.Training;
                    
                    var nameMatch = s.Name.ToLower().Contains(searchLower);
                    var trainingMatch = training?.Title?.ToLower().Contains(searchLower) ?? false;
                    
                    return nameMatch || trainingMatch;
                }).ToList();
            }

            // Get total count for pagination
            var totalCount = pendingSurveys.Count;

            // Apply pagination
            var pagedSurveys = pendingSurveys
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            // Build result
            var result = pagedSurveys.Select(s =>
            {
                var ts = trainingSurveys.FirstOrDefault(t => t.surveyId == s.Id);
                var training = userTrainings.FirstOrDefault(ut => ut.TrainingId == ts?.trainingId)?.Training;

                return new StudentSurveyDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    QuestionCount = s.Questions?.Count ?? 0,
                    HasResponded = false,
                    TrainingId = ts?.trainingId,
                    TrainingName = training?.Title
                };
            }).ToList();

            return BaseResponse<List<StudentSurveyDto>>.SuccessResponse(
                result,
                request.PageNumber,
                request.PageSize,
                totalCount,
                "ØªÙ  ØªØ Ø Ù Ù Ø§ÙØ§Ø³ØªØ¨ÙØ§ÙØ§Øª Ø§ÙÙ ÙØªØ¸Ø±Ø© Ø¨Ù Ø§Ù Ø§Ø­"
            );
        }
    }
}
