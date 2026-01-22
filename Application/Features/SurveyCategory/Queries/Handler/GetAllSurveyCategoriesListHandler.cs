using Application.Common;
using Application.Features.SurveyCategory.DTOs;
using Application.Features.SurveyCategory.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SurveyCategory.Queries.Handler
{
    public class GetAllSurveyCategoriesListHandler : IRequestHandler<GetAllSurveyCategoriesListQuery, BaseResponse<List<SurveyCategoryListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSurveyCategoriesListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<SurveyCategoryListDTO>>> Handle(GetAllSurveyCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.ISurveyCategory.GetAllAsync(c => c.Surveys);

            if (response == null || !response.Any())
            {
                return BaseResponse<List<SurveyCategoryListDTO>>.SuccessResponse(
                    new List<SurveyCategoryListDTO>(),
                    "No survey category found"
                );
            }

            var data = response.Select(c => new SurveyCategoryListDTO
            {
                Id = c.Id,
                CreatedBy = c.CreatedBy,
                CreatedDate = c.CreatedDate,
                UpdatedBy = c.UpdatedBy,
                UpdatedAt = c.UpdatedAt,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            return BaseResponse<List<SurveyCategoryListDTO>>.SuccessResponse(
                data,
                "SurveyCategories retrieved successfully"
            );
        }
    }
}
