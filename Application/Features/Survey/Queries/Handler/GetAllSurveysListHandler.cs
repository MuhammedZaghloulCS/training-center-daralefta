
using Application.Common;
using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using Application.Features.User.DTOs;
using Infrastructure.Abstractions.IUnitOfWork;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
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
            Expression<Func<Domain.Entities.Survey, bool>> filter = s => s.IsActive&&!s.IsForSpecificUsers;

            if (!string.IsNullOrEmpty(request.Search))
            {
                filter = s => (s.Name.Contains(request.Search) || s.Description.Contains(request.Search)) && s.IsActive&&!s.IsForSpecificUsers ;
            }
            var surveys = await _unitOfWork.ISurvey.NewFindRowAsync2(
      filter,
      include: q => q
          .Include(s => s.Questions)
          .Include(s => s.SurveyUsers)
              .ThenInclude(su => su.User),
      orderBy: s => s.CreatedAt,
      acsending: false
  );

           

            //var userIds = surveys.Where(s => s.IsForSpecificUsers).SelectMany(s => s.SurveyUsers.Select(su => su.UserId)).Distinct().ToList();
            //var users = await _unitOfWork.ISurveyUsers.GetAllSpecifiedSurveysAsync(s=>userIds.Contains(s.UserId),s=>s.User);
            var surveysDto = surveys.Select(s => new SurveyDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt,
                IsForSpecificUsers = s.IsForSpecificUsers,

                Questions = s.Questions.Adapt<List<QuestionDto>>(),

                SpecificUsers = s.SurveyUsers
    .Select(su => new SimpleUserDto
    {
        Id = su.User.Id,
        FullName = su.User.FullName,
        UserName = su.User.UserName
    })
    .ToList()
            }).ToList();

            return BaseResponse<List<SurveyDto>>.SuccessResponse(surveysDto);
        }
    }
}
