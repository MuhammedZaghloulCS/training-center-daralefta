using Application.Common;
using Application.Features.SurveyResponse.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// Alias for clarity
using CairoTime = Application.Common.DateTimeHelper;

namespace Application.Features.SurveyResponse.Commands.Update
{
    public class UpdateSurveyResponseHandler : IRequestHandler<UpdateSurveyResponseCommand, BaseResponse<SurveyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSurveyResponseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SurveyResponseDto>> Handle(UpdateSurveyResponseCommand request, CancellationToken cancellationToken)
        {
            var surveyResponse = await _unitOfWork.ISurveyResponse.GetByPkAsync(request.Id);
            if (surveyResponse == null|| surveyResponse.IsDeleted)
            {
                return BaseResponse<SurveyResponseDto>.NotFoundResponse("SurveyResponse not found");
            }

            var errors = new List<string>();

            if (request.UserId == Guid.Empty)
                errors.Add("UserId is required");

            if (request.SurveyId < 1)
                errors.Add("SurveyId is invalid");

            if (request.SubmittedAt == default)
                errors.Add("SubmittedAt is required");

            if (errors.Any())
                return BaseResponse<SurveyResponseDto>.FailureResponse("Validation failed", errors);

            surveyResponse.UserId = request.UserId;
            surveyResponse.SurveyId = request.SurveyId;
            surveyResponse.SubmittedAt = request.SubmittedAt;
            surveyResponse.UpdatedBy = request.UpdatedBy;
            surveyResponse.UpdatedAt = CairoTime.Now;

            _unitOfWork.ISurveyResponse.Update(surveyResponse);
            await _unitOfWork.Complete();

            var dto = new SurveyResponseDto
            {
                Id = surveyResponse.Id,
                CreatedBy = surveyResponse.CreatedBy,
                CreatedDate = surveyResponse.CreatedDate,
                UpdatedBy = surveyResponse.UpdatedBy,
                UpdatedAt = surveyResponse.UpdatedAt,
                UserId = surveyResponse.UserId,
                SurveyId = surveyResponse.SurveyId,
                SubmittedAt = surveyResponse.SubmittedAt
            };

            return BaseResponse<SurveyResponseDto>.SuccessResponse(dto, "SurveyResponse updated successfully");
        }
    }
}
