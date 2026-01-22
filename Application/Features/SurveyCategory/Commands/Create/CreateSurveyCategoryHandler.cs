using Application.Common;
using Application.Features.SurveyCategory.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SurveyCategory.Commands.Create
{
    public class CreateSurveyCategoryHandler : IRequestHandler<CreateSurveyCategoryCommand, BaseResponse<SurveyCategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSurveyCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SurveyCategoryDto>> Handle(CreateSurveyCategoryCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.CreatedBy))
                errors.Add("CreatedBy is required");

            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("Name is required");

            if (string.IsNullOrWhiteSpace(request.Description))
                errors.Add("Description is required");

            if (errors.Any())
                return BaseResponse<SurveyCategoryDto>.FailureResponse("Validation failed", errors);

            var surveyCategory = new Domain.Entities.SurveyCategory
            {
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                Name = request.Name,
                Description = request.Description
            };

            await _unitOfWork.ISurveyCategory.AddAsync(surveyCategory);
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

            return BaseResponse<SurveyCategoryDto>.SuccessResponse(dto, "SurveyCategory created successfully");
        }
    }
}
