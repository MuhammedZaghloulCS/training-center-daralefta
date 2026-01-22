using Application.Common;
using Application.Features.SurveyCategory.DTOs;
using MediatR;

namespace Application.Features.SurveyCategory.Queries.Model
{
    public class GetSurveyCategoryByIdQuery : IRequest<BaseResponse<SurveyCategoryDto>>
    {
        public int Id { get; set; }
    }
}
