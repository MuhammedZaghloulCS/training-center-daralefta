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
    public class GetSurveyCategoriesPagedHandler : IRequestHandler<GetSurveyCategoriesPagedQuery, BaseResponse<List<SurveyCategoryListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSurveyCategoriesPagedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<SurveyCategoryListDTO>>> Handle(GetSurveyCategoriesPagedQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<SurveyCategoryListDTO>>.BadRequestResponse("Invalid pagination parameters");
            }

            var (items, totalCount) = await _unitOfWork.ISurveyCategory.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                null,
                c => c.Id,
                true,
                c => c.Surveys);

            if (items == null || !items.Any())
            {
                return BaseResponse<List<SurveyCategoryListDTO>>.SuccessResponse(
                    new List<SurveyCategoryListDTO>(),
                    request.PageNumber,
                    request.PageSize,
                    totalCount,
                    "No survey category found");
            }

            var data = items.Select(c => new SurveyCategoryListDTO
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
                request.PageNumber,
                request.PageSize,
                totalCount,
                "SurveyCategories retrieved successfully");
        }
    }
}
