using Application.Common;
using Application.Features.SurveyQuestion.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SurveyQuestion.Commands.Create
{
    public class CreateSurveyQuestionHandler : IRequestHandler<CreateSurveyQuestionCommand, BaseResponse<SurveyQuestionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSurveyQuestionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SurveyQuestionDto>> Handle(CreateSurveyQuestionCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.CreatedBy))
                errors.Add("CreatedBy is required");

            if (string.IsNullOrWhiteSpace(request.QuestionText))
                errors.Add("QuestionText is required");

            if ((int)request.QuestionType < 1)
                errors.Add("QuestionType is invalid");

            if (request.SurveyId < 1)
                errors.Add("SurveyId is invalid");

            if (errors.Any())
                return BaseResponse<SurveyQuestionDto>.FailureResponse("Validation failed", errors);

            var question = new Domain.Entities.SurveyQuestion
            {
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                QuestionText = request.QuestionText,
                QuestionType = request.QuestionType,
                Hint = request.Hint ?? string.Empty,
                Active = request.Active,
                SurveyId = request.SurveyId
            };

            await _unitOfWork.ISurveyQuestion.AddAsync(question);
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

            return BaseResponse<SurveyQuestionDto>.SuccessResponse(dto, "SurveyQuestion created successfully");
        }
    }
}
