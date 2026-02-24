using Application.Common;
using Application.Features.SurveyQuestion.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SurveyQuestion.Commands.Update
{
    public class UpdateSurveyQuestionHandler : IRequestHandler<UpdateSurveyQuestionCommand, BaseResponse<SurveyQuestionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSurveyQuestionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SurveyQuestionDto>> Handle(UpdateSurveyQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _unitOfWork.ISurveyQuestion.GetByPkAsync(request.Id);
            if (question == null||question.IsDeleted)
            {
                return BaseResponse<SurveyQuestionDto>.NotFoundResponse("SurveyQuestion not found");
            }

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.QuestionText))
                errors.Add("QuestionText is required");

            if ((int)request.QuestionType < 1)
                errors.Add("QuestionType is invalid");

            if (request.SurveyId < 1)
                errors.Add("SurveyId is invalid");

            if (errors.Any())
                return BaseResponse<SurveyQuestionDto>.FailureResponse("Validation failed", errors);

            question.QuestionText = request.QuestionText;
            question.QuestionType = request.QuestionType;
            question.Hint = request.Hint ?? string.Empty;
            question.Active = request.Active;
            question.SurveyId = request.SurveyId;
            question.UpdatedBy = request.UpdatedBy;
            question.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.ISurveyQuestion.Update(question);
            await _unitOfWork.Complete();

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

            return BaseResponse<SurveyQuestionDto>.SuccessResponse(dto, "SurveyQuestion updated successfully");
        }
    }
}
