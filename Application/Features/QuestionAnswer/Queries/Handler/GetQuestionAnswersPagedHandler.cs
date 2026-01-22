using Application.Common;
using Application.Features.QuestionAnswer.DTOs;
using Application.Features.QuestionAnswer.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.QuestionAnswer.Queries.Handler
{
    public class GetQuestionAnswersPagedHandler : IRequestHandler<GetQuestionAnswersPagedQuery, BaseResponse<List<QuestionAnswerListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetQuestionAnswersPagedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<QuestionAnswerListDTO>>> Handle(GetQuestionAnswersPagedQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<QuestionAnswerListDTO>>.BadRequestResponse("Invalid pagination parameters");
            }

            var (items, totalCount) = await _unitOfWork.ISurveyAnswer.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                null,
                a => a.Id,
                true,
                a => a.SurveyQuestion,
                a => a.SurveyResponse);

            if (items == null || !items.Any())
            {
                return BaseResponse<List<QuestionAnswerListDTO>>.SuccessResponse(
                    new List<QuestionAnswerListDTO>(),
                    request.PageNumber,
                    request.PageSize,
                    totalCount,
                    "No question answer found");
            }

            var data = items.Select(a => new QuestionAnswerListDTO
            {
                Id = a.Id,
                CreatedBy = a.CreatedBy,
                CreatedDate = a.CreatedDate,
                UpdatedBy = a.UpdatedBy,
                UpdatedAt = a.UpdatedAt,
                Answer = a.Answer,
                SurveyQuestionId = a.SurveyQuestionId,
                SurveyResponseId = a.SurveyResponseId
            }).ToList();

            return BaseResponse<List<QuestionAnswerListDTO>>.SuccessResponse(
                data,
                request.PageNumber,
                request.PageSize,
                totalCount,
                "QuestionAnswers retrieved successfully");
        }
    }
}
