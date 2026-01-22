using Application.Common;
using Application.Features.SurveyQuestion.DTOs;
using Application.Features.SurveyQuestion.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SurveyQuestion.Queries.Handler
{
    public class GetAllSurveyQuestionsListHandler : IRequestHandler<GetAllSurveyQuestionsListQuery, BaseResponse<List<SurveyQuestionListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSurveyQuestionsListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<SurveyQuestionListDTO>>> Handle(GetAllSurveyQuestionsListQuery request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.ISurveyQuestion.GetAllAsync(q => q.Survey, q => q.Answers);

            if (response == null || !response.Any())
            {
                return BaseResponse<List<SurveyQuestionListDTO>>.SuccessResponse(
                    new List<SurveyQuestionListDTO>(),
                    "No survey question found"
                );
            }

            var data = response.Select(q => new SurveyQuestionListDTO
            {
                Id = q.Id,
                CreatedBy = q.CreatedBy,
                CreatedDate = q.CreatedDate,
                UpdatedBy = q.UpdatedBy,
                UpdatedAt = q.UpdatedAt,
                QuestionText = q.QuestionText,
                QuestionType = q.QuestionType,
                Hint = q.Hint,
                Active = q.Active,
                SurveyId = q.SurveyId
            }).ToList();

            return BaseResponse<List<SurveyQuestionListDTO>>.SuccessResponse(
                data,
                "SurveyQuestions retrieved successfully"
            );
        }
    }
}
