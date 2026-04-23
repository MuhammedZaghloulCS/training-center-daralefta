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
    public class GetStudentSurveysHandler : IRequestHandler<GetStudentSurveysQuery, BaseResponse<List<StudentSurveyDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStudentSurveysHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<StudentSurveyDto>>> Handle(GetStudentSurveysQuery request, CancellationToken cancellationToken)
        {
            var userId = (request.UserId);
            
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
            var existingResponses = await _unitOfWork.ISurveyResponse.NewFindRowAsync(
                sr => sr.UserId == userId,
                                orderBy: s => s.CreatedDate, acsending: false

            );
            var respondedSurveyIds = existingResponses.Select(sr => sr.SurveyId).ToHashSet();

            // Build result
            var result = surveys.Select(s =>
            {
                var ts = trainingSurveys.FirstOrDefault(t => t.surveyId == s.Id);
                var training = userTrainings.FirstOrDefault(ut => ut.TrainingId == ts?.trainingId)?.Training;

                return new StudentSurveyDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    QuestionCount = s.Questions?.Count ?? 0,
                    HasResponded = respondedSurveyIds.Contains(s.Id),
                    TrainingId = ts?.trainingId,
                    TrainingName = training?.Title
                };
            }).ToList();

            return BaseResponse<List<StudentSurveyDto>>.SuccessResponse(result);
        }
    }
}
