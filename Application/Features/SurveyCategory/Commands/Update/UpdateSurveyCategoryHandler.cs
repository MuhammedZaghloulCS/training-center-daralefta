using Application.Common;
using Application.Features.SurveyCategory.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// Alias for clarity
using CairoTime = Application.Common.DateTimeHelper;

namespace Application.Features.SurveyCategory.Commands.Update
{
    public class UpdateSurveyCategoryHandler : IRequestHandler<UpdateSurveyCategoryCommand, BaseResponse<SurveyCategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSurveyCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SurveyCategoryDto>> Handle(UpdateSurveyCategoryCommand request, CancellationToken cancellationToken)
        {
            var surveyCategory = await _unitOfWork.ISurveyCategory.GetByPkAsync(request.Id);
            if (surveyCategory == null || surveyCategory.IsDeleted)
            {
                return BaseResponse<SurveyCategoryDto>.NotFoundResponse("SurveyCategory not found");
            }

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("Name is required");

            if (string.IsNullOrWhiteSpace(request.Description))
                errors.Add("Description is required");

            if (errors.Any())
                return BaseResponse<SurveyCategoryDto>.FailureResponse("Validation failed", errors);

            surveyCategory.Name = request.Name;
            surveyCategory.Description = request.Description;
            surveyCategory.UpdatedBy = request.UpdatedBy;
            surveyCategory.UpdatedAt = CairoTime.Now;

            _unitOfWork.ISurveyCategory.Update(surveyCategory);
            await _unitOfWork.Complete();

            var dto = new SurveyCategoryDto
            {
                Id = surveyCategory.Id,
                CreatedBy = surveyCategory.CreatedBy,
                CreatedDate = surveyCategory.CreatedDate,
                UpdatedBy = surveyCategory.UpdatedBy,
                UpdatedAt = surveyCategory.UpdatedAt,
                Name = surveyCategory.Name,
                Description = surveyCategory.Description
            };

            return BaseResponse<SurveyCategoryDto>.SuccessResponse(dto, "SurveyCategory updated successfully");
        }
    }
}
