using Application.Common;
using Application.Features.SurveyResponse.DTOs;
using Application.Features.SurveyResponse.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.SurveyResponse.Queries.Handler
{
    public class GetSurveyResponseByIdHandler : IRequestHandler<GetSurveyResponseByIdQuery, BaseResponse<SurveyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSurveyResponseByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SurveyResponseDto>> Handle(GetSurveyResponseByIdQuery request, CancellationToken cancellationToken)
        {
            var responses = await _unitOfWork.ISurveyResponse.FindRowAsync(r => r.Id == request.Id, r => r.User, r => r.Survey, r => r.Answers);
            var surveyResponse = responses.FirstOrDefault();

            if (surveyResponse == null || surveyResponse.IsDeleted)
            {
                return BaseResponse<SurveyResponseDto>.NotFoundResponse("SurveyResponse not found");
            }

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

            return BaseResponse<SurveyResponseDto>.SuccessResponse(dto, "SurveyResponse retrieved successfully");
        }
    }
}
