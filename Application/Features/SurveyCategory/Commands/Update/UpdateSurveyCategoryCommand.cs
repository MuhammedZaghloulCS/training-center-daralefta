using Application.Common;
using Application.Features.SurveyCategory.DTOs;
using MediatR;

namespace Application.Features.SurveyCategory.Commands.Update
{
    public class UpdateSurveyCategoryCommand : IRequest<BaseResponse<SurveyCategoryDto>>
    {
        public int Id { get; set; }
        public string? UpdatedBy { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
