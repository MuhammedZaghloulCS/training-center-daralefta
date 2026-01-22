using Application.Common;
using Application.Features.SurveyQuestion.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.SurveyQuestion.Queries.Model
{
    public class GetAllSurveyQuestionsListQuery : IRequest<BaseResponse<List<SurveyQuestionListDTO>>>
    {
    }
}
