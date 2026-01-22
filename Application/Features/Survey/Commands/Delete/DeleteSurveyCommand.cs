using Application.Common;
using MediatR;

namespace Application.Features.Survey.Commands.Delete
{
    public class DeleteSurveyCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
}
