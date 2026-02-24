using Application.Common;
using Application.Features.SurveyQuestion.DTOs;
using Application.Features.SurveyQuestion.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SurveyQuestion.Queries.Handler
{
    public class GetSurveyQuestionByIdHandler : IRequestHandler<GetSurveyQuestionByIdQuery, BaseResponse<SurveyQuestionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSurveyQuestionByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SurveyQuestionDto>> Handle(GetSurveyQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var questions = await _unitOfWork.ISurveyQuestion.FindRowAsync(q => q.Id == request.Id, q => q.Survey, q => q.Answers);
            var question = questions.FirstOrDefault();

            if (question == null||question.IsDeleted)
            {
                return BaseResponse<SurveyQuestionDto>.NotFoundResponse("SurveyQuestion not found");
            }

            var dto = new SurveyQuestionDto
            {
                Id = question.Id,
                CreatedBy = question.CreatedBy,
                CreatedDate = question.CreatedDate,
                UpdatedBy = question.UpdatedBy,
                UpdatedAt = question.UpdatedAt,
                QuestionText = question.QuestionText,
                QuestionType = question.QuestionType,
                Hint = question.Hint,
                Active = question.Active,
                SurveyId = question.SurveyId
            };

            return BaseResponse<SurveyQuestionDto>.SuccessResponse(dto, "SurveyQuestion retrieved successfully");
        }
    }
}
