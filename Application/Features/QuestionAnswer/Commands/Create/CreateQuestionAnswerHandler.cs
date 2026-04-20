using Application.Common;
using Application.Features.QuestionAnswer.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.QuestionAnswer.Commands.Create
{
    public class CreateQuestionAnswerHandler : IRequestHandler<CreateQuestionAnswerCommand, BaseResponse<QuestionAnswerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateQuestionAnswerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<QuestionAnswerDto>> Handle(CreateQuestionAnswerCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.CreatedBy))
                errors.Add("CreatedBy is required");

            if (string.IsNullOrWhiteSpace(request.Answer))
                errors.Add("Answer is required");

            if (request.SurveyQuestionId < 1)
                errors.Add("SurveyQuestionId is invalid");

            if (request.SurveyResponseId < 1)
                errors.Add("SurveyResponseId is invalid");

            if (!errors.Any())
            {
                var existing = await _unitOfWork.ISurveyAnswer.FindRowAsync(a => a.SurveyResponseId == request.SurveyResponseId && a.questionId == request.SurveyQuestionId);
                if (existing.Any())
                {
                    errors.Add("Answer already exists for this question and response");
                }
            }

            if (errors.Any())
                return BaseResponse<QuestionAnswerDto>.FailureResponse("Validation failed", errors);

            var answer = new Domain.Entities.QuestionAnswer
            {
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                Answer = request.Answer,
                questionId = request.SurveyQuestionId,
                SurveyResponseId = request.SurveyResponseId
            };

            await _unitOfWork.ISurveyAnswer.AddAsync(answer);
            await _unitOfWork.Complete();

            var dto = new QuestionAnswerDto
            {
                Id = answer.Id,
                CreatedBy = answer.CreatedBy,
                CreatedDate = answer.CreatedDate,
                UpdatedBy = answer.UpdatedBy,
                UpdatedAt = answer.UpdatedAt,
                Answer = answer.Answer,
                SurveyQuestionId = answer.questionId,
                SurveyResponseId = answer.SurveyResponseId
            };

            return BaseResponse<QuestionAnswerDto>.SuccessResponse(dto, "QuestionAnswer created successfully");
        }
    }
}
