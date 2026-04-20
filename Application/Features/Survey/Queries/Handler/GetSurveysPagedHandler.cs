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
    public class GetSurveysPagedHandler : IRequestHandler<GetSurveysPagedQuery, BaseResponse<List<SurveyDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSurveysPagedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<SurveyDto>>> Handle(GetSurveysPagedQuery request, CancellationToken cancellationToken)
        {


            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<SurveyDto>>.BadRequestResponse("معلمات ترقيم الصفحات غير صالحة");
            }
            Expression<Func<Domain.Entities.Survey, bool>> filter = null;

            if (!string.IsNullOrEmpty(request.Search))
            {
                var result = bool.TryParse(request.Search, out var isActive);

                filter = s => s.Name.Contains(request.Search) || s.Description.Contains(request.Search);
                if (result)
                {
                    filter = s => s.Name.Contains(request.Search) || s.Description.Contains(request.Search) || s.IsActive == isActive;
                }
            }

            var pagedSurveys = await _unitOfWork.ISurvey.GetPaginatedAsync(request.PageNumber, request.PageSize, filter, includeProperties: s => s.Questions);
            
            var surveyDTOs = pagedSurveys.items.Select(s => new SurveyDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                IsActive = s.IsActive,
                Questions = s.Questions.Select(q => new QuestionDto
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText,
                    QuestionType = q.QuestionType,
                    Options = q.Options,
                    IsRequired = q.IsRequired,
                    SortOrder = q.SortOrder

                }).ToList()
            }).ToList();


            return BaseResponse<List<SurveyDto>>.SuccessResponse(
                surveyDTOs,
                request.PageNumber,
                request.PageSize,
                pagedSurveys.totalCount,
                "تم تحميل البيانات بنجاح"
            );
        }
    }
}
