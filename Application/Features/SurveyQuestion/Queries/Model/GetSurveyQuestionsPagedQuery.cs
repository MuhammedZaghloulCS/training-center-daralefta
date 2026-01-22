using Application.Common;
using Application.Features.SurveyQuestion.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.SurveyQuestion.Queries.Model
{
    public class GetSurveyQuestionsPagedQuery : IRequest<BaseResponse<List<SurveyQuestionListDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
