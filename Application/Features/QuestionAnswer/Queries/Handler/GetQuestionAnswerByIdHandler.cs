using Application.Common;
using Application.Features.QuestionAnswer.DTOs;
using Application.Features.QuestionAnswer.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.QuestionAnswer.Queries.Handler
{
    public class GetQuestionAnswerByIdHandler : IRequestHandler<GetQuestionAnswerByIdQuery, BaseResponse<QuestionAnswerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetQuestionAnswerByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<QuestionAnswerDto>> Handle(GetQuestionAnswerByIdQuery request, CancellationToken cancellationToken)
        {
            var answers = await _unitOfWork.ISurveyAnswer.FindRowAsync(a => a.Id == request.Id, a => a.SurveyQuestion, a => a.SurveyResponse);
            var answer = answers.FirstOrDefault();

            if (answer == null)
            {
                return BaseResponse<QuestionAnswerDto>.NotFoundResponse("QuestionAnswer not found");
            }

            var dto = new QuestionAnswerDto
            {
                Id = answer.Id,
                CreatedBy = answer.CreatedBy,
                CreatedDate = answer.CreatedDate,
                UpdatedBy = answer.UpdatedBy,
                UpdatedAt = answer.UpdatedAt,
                Answer = answer.Answer,
                SurveyQuestionId = answer.SurveyQuestionId,
                SurveyResponseId = answer.SurveyResponseId
            };

            return BaseResponse<QuestionAnswerDto>.SuccessResponse(dto, "QuestionAnswer retrieved successfully");
        }
    }
}
