using Application.Common;
using Application.Features.SurveyResponse.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SurveyResponse.Commands.Create
{
    public class CreateSurveyResponseHandler : IRequestHandler<CreateSurveyResponseCommand, BaseResponse<SurveyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSurveyResponseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SurveyResponseDto>> Handle(CreateSurveyResponseCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.CreatedBy))
                errors.Add("CreatedBy is required");

            if (request.UserId == Guid.Empty)
                errors.Add("UserId is required");

            if (request.SurveyId < 1)
                errors.Add("SurveyId is invalid");

            if (request.SubmittedAt == default)
                errors.Add("SubmittedAt is required");

            if (errors.Any())
                return BaseResponse<SurveyResponseDto>.FailureResponse("Validation failed", errors);

            var surveyResponse = new Domain.Entities.SurveyResponse
            {
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                UserId = request.UserId,
                SurveyId = request.SurveyId,
                SubmittedAt = request.SubmittedAt
            };

            await _unitOfWork.ISurveyResponse.AddAsync(surveyResponse);
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

            return BaseResponse<SurveyResponseDto>.SuccessResponse(dto, "SurveyResponse created successfully");
        }
    }
}
