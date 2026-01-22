using Application.Common;
using Application.Features.SurveyCategory.DTOs;
using MediatR;

namespace Application.Features.SurveyCategory.Commands.Create
{
    public class CreateSurveyCategoryCommand : IRequest<BaseResponse<SurveyCategoryDto>>
    {
        public string CreatedBy { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
