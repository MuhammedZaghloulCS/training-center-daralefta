using Application.Common;
using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using Application.Features.User.DTOs;
using Domain.Entities;
using Hangfire.Storage.Monitoring;
using Infrastructure.Abstractions.IUnitOfWork;
using Mapster;
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

            var s = await _unitOfWork.ISurvey.GetSurveyWithQuestion(request.Id);

            if (s == null)
            {
                return BaseResponse<SurveyDto>.NotFoundResponse("Survey not found");
            }

            var surveysDto = new SurveyDto
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
            };
            return BaseResponse<SurveyDto>.SuccessResponse(surveysDto, "loaded successfully");
        }
    }
}
