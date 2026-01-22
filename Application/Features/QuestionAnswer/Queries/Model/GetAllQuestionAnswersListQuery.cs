using Application.Common;
using Application.Features.QuestionAnswer.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.QuestionAnswer.Queries.Model
{
    public class GetAllQuestionAnswersListQuery : IRequest<BaseResponse<List<QuestionAnswerListDTO>>>
    {
    }
}
