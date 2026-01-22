using Application.Common;
using Application.Features.SurveyResponse.DTOs;
using Application.Features.SurveyResponse.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SurveyResponse.Queries.Handler
{
    public class GetSurveyResponsesPagedHandler : IRequestHandler<GetSurveyResponsesPagedQuery, BaseResponse<List<SurveyResponseListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSurveyResponsesPagedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<SurveyResponseListDTO>>> Handle(GetSurveyResponsesPagedQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<SurveyResponseListDTO>>.BadRequestResponse("Invalid pagination parameters");
            }

            var (items, totalCount) = await _unitOfWork.ISurveyResponse.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                null,
                r => r.Id,
                true,
                r => r.User,
                r => r.Survey,
                r => r.Answers);

            if (items == null || !items.Any())
            {
                return BaseResponse<List<SurveyResponseListDTO>>.SuccessResponse(
                    new List<SurveyResponseListDTO>(),
                    request.PageNumber,
                    request.PageSize,
                    totalCount,
                    "No survey response found");
            }

            var data = items.Select(r => new SurveyResponseListDTO
            {
                Id = r.Id,
                CreatedBy = r.CreatedBy,
                CreatedDate = r.CreatedDate,
                UpdatedBy = r.UpdatedBy,
                UpdatedAt = r.UpdatedAt,
                UserId = r.UserId,
                SurveyId = r.SurveyId,
                SubmittedAt = r.SubmittedAt
            }).ToList();

            return BaseResponse<List<SurveyResponseListDTO>>.SuccessResponse(
                data,
                request.PageNumber,
                request.PageSize,
                totalCount,
                "SurveyResponses retrieved successfully");
        }
    }
}
