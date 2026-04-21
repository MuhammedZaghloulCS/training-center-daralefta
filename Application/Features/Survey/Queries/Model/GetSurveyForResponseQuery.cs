using Application.Common;
using Application.Features.Survey.DTOs;
using MediatR;

namespace Application.Features.Survey.Queries.Model
{
    public class GetSurveyForResponseQuery : IRequest<BaseResponse<SurveyWithQuestionsDto>>
    {
        public int SurveyId { get; set; }
        public Guid UserId { get; set; }
    }
}
