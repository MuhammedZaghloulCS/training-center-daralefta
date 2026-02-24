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
    public class GetSurveyQuestionsPagedHandler : IRequestHandler<GetSurveyQuestionsPagedQuery, BaseResponse<List<SurveyQuestionListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSurveyQuestionsPagedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<SurveyQuestionListDTO>>> Handle(GetSurveyQuestionsPagedQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<SurveyQuestionListDTO>>.BadRequestResponse("Invalid pagination parameters");
            }

            var (items, totalCount) = await _unitOfWork.ISurveyQuestion.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                null,
                q => q.Id,
                true,
                q => q.Survey,
                q => q.Answers);

            if (items == null || !items.Any()||items.All(r=>r.IsDeleted))
            {
                return BaseResponse<List<SurveyQuestionListDTO>>.SuccessResponse(
                    new List<SurveyQuestionListDTO>(),
                    request.PageNumber,
                    request.PageSize,
                    totalCount,
                    "No survey question found");
            }

            var data = items.Where(r => !r.IsDeleted).Select(q => new SurveyQuestionListDTO
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
                request.PageNumber,
                request.PageSize,
                totalCount,
                "SurveyQuestions retrieved successfully");
        }
    }
}
