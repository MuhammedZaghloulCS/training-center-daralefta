using Application.Common;
using Application.Features.QuestionAnswer.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.QuestionAnswer.Commands.Update
{
    public class UpdateQuestionAnswerHandler : IRequestHandler<UpdateQuestionAnswerCommand, BaseResponse<QuestionAnswerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateQuestionAnswerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<QuestionAnswerDto>> Handle(UpdateQuestionAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = await _unitOfWork.ISurveyAnswer.GetByPkAsync(request.Id);
            if (answer == null)
            {
                return BaseResponse<QuestionAnswerDto>.NotFoundResponse("QuestionAnswer not found");
            }

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Answer))
                errors.Add("Answer is required");

            if (request.SurveyQuestionId < 1)
                errors.Add("SurveyQuestionId is invalid");

            if (request.SurveyResponseId < 1)
                errors.Add("SurveyResponseId is invalid");

            if (!errors.Any())
            {
                var existing = await _unitOfWork.ISurveyAnswer.FindRowAsync(a => a.SurveyResponseId == request.SurveyResponseId && a.SurveyQuestionId == request.SurveyQuestionId && a.Id != request.Id);
                if (existing.Any())
                {
                    errors.Add("Answer already exists for this question and response");
                }
            }

            if (errors.Any())
                return BaseResponse<QuestionAnswerDto>.FailureResponse("Validation failed", errors);

            answer.Answer = request.Answer;
            answer.SurveyQuestionId = request.SurveyQuestionId;
            answer.SurveyResponseId = request.SurveyResponseId;
            answer.UpdatedBy = request.UpdatedBy;
            answer.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.ISurveyAnswer.Update(answer);
            await _unitOfWork.Complete();

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

            return BaseResponse<QuestionAnswerDto>.SuccessResponse(dto, "QuestionAnswer updated successfully");
        }
    }
}
