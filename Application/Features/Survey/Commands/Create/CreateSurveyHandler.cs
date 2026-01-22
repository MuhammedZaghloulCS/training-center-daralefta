using Application.Common;
using Application.Features.Survey.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Survey.Commands.Create
{
    public class CreateSurveyHandler : IRequestHandler<CreateSurveyCommand, BaseResponse<SurveyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSurveyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SurveyDto>> Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.CreatedBy))
                errors.Add("CreatedBy is required");

            if (string.IsNullOrWhiteSpace(request.Title))
                errors.Add("Title is required");

            if (string.IsNullOrWhiteSpace(request.Description))
                errors.Add("Description is required");

            if (request.CreatedByUserId == Guid.Empty)
                errors.Add("CreatedByUserId is required");

            if (request.TrainingId < 1)
                errors.Add("TrainingId is invalid");

            if (request.SurveyCategoryId < 1)
                errors.Add("SurveyCategoryId is invalid");

            if (errors.Any())
                return BaseResponse<SurveyDto>.FailureResponse("Validation failed", errors);

            var survey = new Domain.Entities.Survey
            {
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                Title = request.Title,
                Description = request.Description,
                CreatedByUserId = request.CreatedByUserId,
                TrainingId = request.TrainingId,
                SurveyCategoryId = request.SurveyCategoryId
            };

            await _unitOfWork.ISurvey.AddAsync(survey);
            await _unitOfWork.Complete();

            var dto = new SurveyDto
            {
                Id = survey.Id,
                CreatedBy = survey.CreatedBy,
                CreatedDate = survey.CreatedDate,
                UpdatedBy = survey.UpdatedBy,
                UpdatedAt = survey.UpdatedAt,
                Title = survey.Title,
                Description = survey.Description,
                CreatedByUserId = survey.CreatedByUserId,
                TrainingId = survey.TrainingId,
                SurveyCategoryId = survey.SurveyCategoryId
            };

            return BaseResponse<SurveyDto>.SuccessResponse(dto, "Survey created successfully");
        }
    }
}
