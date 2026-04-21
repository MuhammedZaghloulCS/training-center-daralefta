using Application.Common;
using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using Domain.Enums;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Survey.Queries.Handler
{
    public class GetSurveyForResponseHandler : IRequestHandler<GetSurveyForResponseQuery, BaseResponse<SurveyWithQuestionsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSurveyForResponseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SurveyWithQuestionsDto>> Handle(GetSurveyForResponseQuery request, CancellationToken cancellationToken)
        {
            var userId = (request.UserId);
            
            // Check if user already responded
            var existingResponse = await _unitOfWork.ISurveyResponse.GetFirstByPropAsync(
                sr => sr.UserId == userId && sr.SurveyId == request.SurveyId
            );

            if (existingResponse != null)
            {
                return BaseResponse<SurveyWithQuestionsDto>.NotFoundResponse("You have already responded to this survey");
            }

            // Get survey with questions
            var survey = await _unitOfWork.ISurvey.GetFirstByPropAsync(
                s => s.Id == request.SurveyId && s.IsActive
            );

            if (survey == null)
            {
                return BaseResponse<SurveyWithQuestionsDto>.NotFoundResponse("Survey not found");
            }

            // Get questions
            var questions = await _unitOfWork.IQuestion.FindAsync(
                q => q.SurveyId == request.SurveyId
            );

            // Get training info if exists
            string trainingName = null;
            int? trainingId = null;
            var ts = (await _unitOfWork.ITrainingsSurveys.FindAsync(t => t.surveyId == request.SurveyId)).FirstOrDefault();
            if (ts != null)
            {
                trainingId = ts.trainingId;
                var training = await _unitOfWork.ITraining.GetByPkAsync(ts.trainingId.Value);
                trainingName = training?.Title;
            }

            var result = new SurveyWithQuestionsDto
            {
                Id = survey.Id,
                Name = survey.Name,
                Description = survey.Description,
                TrainingId = trainingId,
                TrainingName = trainingName,
                Questions = questions.OrderBy(q => q.SortOrder).Select(q => new QuestionForResponseDto
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText,
                    QuestionType = (int)q.QuestionType,
                    Options = q.Options,
                    IsRequired = q.IsRequired,
                    SortOrder = q.SortOrder
                }).ToList()
            };

            return BaseResponse<SurveyWithQuestionsDto>.SuccessResponse(result);
        }
    }
}
