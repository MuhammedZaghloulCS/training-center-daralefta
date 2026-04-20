using Application.Common;
using Application.Features.Survey.DTOs;
using MediatR;

namespace Application.Features.Survey.Queries.Model
{
    public class GetSurveyByIdQuery : IRequest<BaseResponse<Domain.Entities.Survey>>
    {
        public int Id { get; set; }
    }
}
