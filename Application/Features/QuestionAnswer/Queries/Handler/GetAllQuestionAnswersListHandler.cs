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
    public class GetAllQuestionAnswersListHandler : IRequestHandler<GetAllQuestionAnswersListQuery, BaseResponse<List<QuestionAnswerListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllQuestionAnswersListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<QuestionAnswerListDTO>>> Handle(GetAllQuestionAnswersListQuery request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.ISurveyAnswer.GetAllAsync(a => a.SurveyQuestion, a => a.SurveyResponse);

            if (response == null || !response.Any())
            {
                return BaseResponse<List<QuestionAnswerListDTO>>.SuccessResponse(
                    new List<QuestionAnswerListDTO>(),
                    "No question answer found"
                );
            }

            var data = response.Select(a => new QuestionAnswerListDTO
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
                "QuestionAnswers retrieved successfully"
            );
        }
    }
}
