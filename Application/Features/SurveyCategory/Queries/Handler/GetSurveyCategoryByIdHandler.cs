using Application.Common;
using Application.Features.SurveyCategory.DTOs;
using Application.Features.SurveyCategory.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SurveyCategory.Queries.Handler
{
    public class GetSurveyCategoryByIdHandler : IRequestHandler<GetSurveyCategoryByIdQuery, BaseResponse<SurveyCategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSurveyCategoryByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SurveyCategoryDto>> Handle(GetSurveyCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.ISurveyCategory.FindRowAsync(c => c.Id == request.Id, c => c.Surveys);
            var surveyCategory = categories.FirstOrDefault();

            if (surveyCategory == null)
            {
                return BaseResponse<SurveyCategoryDto>.NotFoundResponse("SurveyCategory not found");
            }

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

            return BaseResponse<SurveyCategoryDto>.SuccessResponse(dto, "SurveyCategory retrieved successfully");
        }
    }
}
