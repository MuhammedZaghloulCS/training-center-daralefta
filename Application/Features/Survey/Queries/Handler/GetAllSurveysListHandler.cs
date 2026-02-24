using Application.Common;
using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Survey.Queries.Handler
{
    public class GetAllSurveysListHandler : IRequestHandler<GetAllSurveysListQuery, BaseResponse<List<SurveyListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSurveysListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<SurveyListDTO>>> Handle(GetAllSurveysListQuery request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.ISurvey.GetAllAsync(
                s => s.CreatedByUser,
                s => s.Training,
                s => s.SurveyCategory,
                s => s.SurveyQuestions,
                s => s.SurveyResponses);

            if (response == null || !response.Any()||response.All(r=>r.IsDeleted))
            {
                return BaseResponse<List<SurveyListDTO>>.SuccessResponse(
                    new List<SurveyListDTO>(),
                    "No survey found"
                );
            }

            var data = response.Where(r => !r.IsDeleted).Select(s => new SurveyListDTO
            {
                Id = s.Id,
                CreatedBy = s.CreatedBy,
                CreatedDate = s.CreatedDate,
                UpdatedBy = s.UpdatedBy,
                UpdatedAt = s.UpdatedAt,
                Title = s.Title,
                Description = s.Description,
                CreatedByUserId = s.CreatedByUserId,
                TrainingId = s.TrainingId,
                SurveyCategoryId = s.SurveyCategoryId
            }).ToList();

            return BaseResponse<List<SurveyListDTO>>.SuccessResponse(
                data,
                "Surveys retrieved successfully"
            );
        }
    }
}
