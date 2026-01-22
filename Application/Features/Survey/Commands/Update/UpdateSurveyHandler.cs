using Application.Common;
using Application.Features.Survey.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Survey.Commands.Update
{
    public class UpdateSurveyHandler : IRequestHandler<UpdateSurveyCommand, BaseResponse<SurveyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSurveyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SurveyDto>> Handle(UpdateSurveyCommand request, CancellationToken cancellationToken)
        {
            var survey = await _unitOfWork.ISurvey.GetByPkAsync(request.Id);
            if (survey == null)
            {
                return BaseResponse<SurveyDto>.NotFoundResponse("Survey not found");
            }

            var errors = new List<string>();

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

            survey.Title = request.Title;
            survey.Description = request.Description;
            survey.CreatedByUserId = request.CreatedByUserId;
            survey.TrainingId = request.TrainingId;
            survey.SurveyCategoryId = request.SurveyCategoryId;
            survey.UpdatedBy = request.UpdatedBy;
            survey.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.ISurvey.Update(survey);
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

            return BaseResponse<SurveyDto>.SuccessResponse(dto, "Survey updated successfully");
        }
    }
}
