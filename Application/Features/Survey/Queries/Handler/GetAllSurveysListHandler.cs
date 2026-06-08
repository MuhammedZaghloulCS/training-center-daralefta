using Application.Common;
using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using Mapster;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Survey.Queries.Handler
{
    public class GetAllSurveysListHandler : IRequestHandler<GetAllSurveysListQuery, BaseResponse<List<SurveyDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSurveysListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<SurveyDto>>> Handle(GetAllSurveysListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Domain.Entities.Survey, bool>> filter = s => s.IsActive&&!!s.IsForSpecificUsers;

            if (!string.IsNullOrEmpty(request.Search))
            {
                filter = s => (s.Name.Contains(request.Search) || s.Description.Contains(request.Search)) && s.IsActive&&!s.IsForSpecificUsers ;
            }
            var surveys = await _unitOfWork.ISurvey.NewFindRowAsync(filter, orderBy: s => s.CreatedAt, acsending: false, s => s.Questions);
            var surveysDto = surveys.Adapt<List<SurveyDto>>();

            return BaseResponse<List<SurveyDto>>.SuccessResponse(surveysDto);
        }
    }
}
