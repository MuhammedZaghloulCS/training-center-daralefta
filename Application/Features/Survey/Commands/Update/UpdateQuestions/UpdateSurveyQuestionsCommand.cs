using Application.Common;
using Application.Features.Survey.DTOs;
using MediatR;
using System;

namespace Application.Features.Survey.Commands.Update.updatequestions
{
    public class UpdateSurveyQuestionsCommandtwo : IRequest<BaseResponse<SurveyDto>>
    {
        public int Id { get; set; }

        public List<QuestionDto>? Questions { get; set; }

    }
}
