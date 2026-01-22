using Application.Common;
using Application.Features.SurveyQuestion.DTOs;
using MediatR;

namespace Application.Features.SurveyQuestion.Queries.Model
{
    public class GetSurveyQuestionByIdQuery : IRequest<BaseResponse<SurveyQuestionDto>>
    {
        public int Id { get; set; }
    }
}
