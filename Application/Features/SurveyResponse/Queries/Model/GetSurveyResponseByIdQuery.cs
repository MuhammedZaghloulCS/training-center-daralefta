using Application.Common;
using Application.Features.SurveyResponse.DTOs;
using MediatR;

namespace Application.Features.SurveyResponse.Queries.Model
{
    public class GetSurveyResponseByIdQuery : IRequest<BaseResponse<SurveyResponseDto>>
    {
        public int Id { get; set; }
    }
}
