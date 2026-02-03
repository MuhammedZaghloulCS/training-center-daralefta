using Application.Common;
using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Survey.Queries.Handler
{
    public class GetSurveysPagedHandler : IRequestHandler<GetSurveysPagedQuery, BaseResponse<List<SurveyListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSurveysPagedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<SurveyListDTO>>> Handle(GetSurveysPagedQuery request, CancellationToken cancellationToken)
        {

            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<SurveyListDTO>>.BadRequestResponse("Invalid pagination parameters");
            }

            Expression<Func<Domain.Entities.Survey, bool>>? trainingPredicate = null;
            if (request.TrainingId.HasValue) { 
                trainingPredicate = s => s.TrainingId == request.TrainingId.Value;
            }

            Expression<System.Func<Domain.Entities.Survey, bool>>? searchPredicate = null;
            if(!string.IsNullOrEmpty(request.Search))
            {
                searchPredicate = s => s.Title.Contains(request.Search) || s.Description.Contains(request.Search) ;
            }



            var (items, totalCount) = await _unitOfWork.ISurvey.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                trainingPredicate,
                s => s.Id,
                true,
                s => s.CreatedByUser,
                s => s.Training,
                s => s.SurveyCategory,
                s => s.SurveyQuestions,
                s => s.SurveyResponses);

            if (items == null || !items.Any())
            {
                return BaseResponse<List<SurveyListDTO>>.SuccessResponse(
                    new List<SurveyListDTO>(),
                    request.PageNumber,
                    request.PageSize,
                    totalCount,
                    "No survey found");
            }

            var data = items.Select(s => new SurveyListDTO
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
                request.PageNumber,
                request.PageSize,
                totalCount,
                "Surveys retrieved successfully");
        }
    }
}
