using Application.Common;
using MediatR;

namespace Application.Features.SurveyCategory.Commands.Delete
{
    public class DeleteSurveyCategoryCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
}
