using Application.Common;
using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Survey.Queries.Handler
{
    public class GetSurveyByIdHandler : IRequestHandler<GetSurveyByIdQuery, BaseResponse<SurveyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSurveyByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SurveyDto>> Handle(GetSurveyByIdQuery request, CancellationToken cancellationToken)
        {
            var surveys = await _unitOfWork.ISurvey.FindRowAsync(
                s => s.Id == request.Id,
                s => s.CreatedByUser,
                s => s.Training,
                s => s.SurveyCategory,
                s => s.SurveyQuestions,
                s => s.SurveyResponses);

            var survey = surveys.FirstOrDefault();

            if (survey == null)
            {
                return BaseResponse<SurveyDto>.NotFoundResponse("Survey not found");
            }

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

            return BaseResponse<SurveyDto>.SuccessResponse(dto, "Survey retrieved successfully");
        }
    }
}
