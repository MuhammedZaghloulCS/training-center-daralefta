using Application.Common;
using Application.Features.QuestionAnswer.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.QuestionAnswer.Queries.Model
{
    public class GetQuestionAnswersPagedQuery : IRequest<BaseResponse<List<QuestionAnswerListDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
