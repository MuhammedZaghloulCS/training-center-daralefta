using Application.Common;
using MediatR;

namespace Application.Features.SurveyResponse.Commands.Delete
{
    public class DeleteSurveyResponseCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
}
