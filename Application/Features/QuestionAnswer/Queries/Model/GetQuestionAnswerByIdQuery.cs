using Application.Common;
using Application.Features.QuestionAnswer.DTOs;
using MediatR;

namespace Application.Features.QuestionAnswer.Queries.Model
{
    public class GetQuestionAnswerByIdQuery : IRequest<BaseResponse<QuestionAnswerDto>>
    {
        public int Id { get; set; }
    }
}
