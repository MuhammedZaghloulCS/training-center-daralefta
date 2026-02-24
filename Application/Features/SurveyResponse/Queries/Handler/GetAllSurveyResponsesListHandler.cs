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
    public class GetAllSurveyResponsesListHandler : IRequestHandler<GetAllSurveyResponsesListQuery, BaseResponse<List<SurveyResponseListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSurveyResponsesListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<SurveyResponseListDTO>>> Handle(GetAllSurveyResponsesListQuery request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.ISurveyResponse.GetAllAsync(r => r.User, r => r.Survey, r => r.Answers);

            if (response == null || !response.Any()||response.All(r=>r.IsDeleted))
            {
                return BaseResponse<List<SurveyResponseListDTO>>.SuccessResponse(
                    new List<SurveyResponseListDTO>(),
                    "No survey response found"
                );
            }

            var data = response.Where(r => !r.IsDeleted).Select(r => new SurveyResponseListDTO
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
                "SurveyResponses retrieved successfully"
            );
        }
    }
}
