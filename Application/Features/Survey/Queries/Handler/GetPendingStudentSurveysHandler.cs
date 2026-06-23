using Application.Common;
using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
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
                return BaseResponse<List<StudentSurveyDto>>
                    .BadRequestResponse("بيانات ترقيم الصفحات غير صالحة");
            }

            var userId = request.UserId;

            // Trainings assigned to student
            var userTrainings = await _unitOfWork.IUserTrainingRepository.FindRowAsync(
                ut => ut.UserId == userId,
                ut => ut.Training
            );

            var trainingIds = userTrainings
                .Select(ut => ut.TrainingId)
                .ToList();

            // Surveys assigned through trainings
            var trainingSurveys = await _unitOfWork.ITrainingsSurveys.FindAsync(
                ts => trainingIds.Contains(ts.trainingId.Value)
            );

            var surveyIds = trainingSurveys
                .Select(ts => ts.surveyId.Value)
                .Distinct()
                .ToList();

            var surveys = await _unitOfWork.ISurvey.FindRowAsync(
                s => surveyIds.Contains(s.Id) && s.IsActive,
                s => s.Questions
            );

            // User responses
            var existingResponses = await _unitOfWork.ISurveyResponse.NewFindRowAsync(
                sr => sr.UserId == userId,
                orderBy: s => s.CreatedDate,
                acsending: false
            );

            var respondedSurveyIds = existingResponses
                .Select(sr => sr.SurveyId)
                .ToHashSet();

            // Pending surveys from trainings
            var pendingSurveys = surveys
                .Where(s => !respondedSurveyIds.Contains(s.Id))
                .ToList();

            // Surveys assigned directly to user
            var specifiedSurveyUsers = await _unitOfWork.ISurveyUsers
                .GetAllSurveysForUserAsync(
                    userId,
                    false
                );

            var specifiedSurveyIds = specifiedSurveyUsers
                .Select(x => x.SurveyId)
                .Where(id => id!=null)
                .Select(id => id)
                .Distinct()
                .ToList();

            // Load specified surveys with questions
            var specifiedSurveyEntities = await _unitOfWork.ISurvey.FindRowAsync(
                s => specifiedSurveyIds.Contains(s.Id)
                     && s.IsActive
                     && !respondedSurveyIds.Contains(s.Id),
                s => s.Questions
            );

            // Merge specified surveys + training surveys
            var allSurveys = specifiedSurveyEntities
                .Concat(pendingSurveys)
                .GroupBy(s => s.Id)
                .Select(g => g.First())
                .ToList();

            // Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var searchLower = request.Search.ToLower();

                allSurveys = allSurveys.Where(s =>
                {
                    var ts = trainingSurveys.FirstOrDefault(t => t.surveyId == s.Id);

                    var training = userTrainings
                        .FirstOrDefault(ut => ut.TrainingId == ts?.trainingId)
                        ?.Training;

                    var nameMatch = s.Name?.ToLower().Contains(searchLower) ?? false;
                    var trainingMatch = training?.Title?.ToLower().Contains(searchLower) ?? false;

                    return nameMatch || trainingMatch;
                }).ToList();
            }

            var totalCount = allSurveys.Count;

            var pagedSurveys = allSurveys
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var result = pagedSurveys.Select(s =>
            {
                var ts = trainingSurveys.FirstOrDefault(t => t.surveyId == s.Id);

                var training = userTrainings
                    .FirstOrDefault(ut => ut.TrainingId == ts?.trainingId)
                    ?.Training;

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

            return BaseResponse<List<StudentSurveyDto>>.SuccessResponse(
                result,
                request.PageNumber,
                request.PageSize,
                totalCount,
                "تم تحميل الاستبيانات المنتظرة بنجاح"
            );
        }
    }
}