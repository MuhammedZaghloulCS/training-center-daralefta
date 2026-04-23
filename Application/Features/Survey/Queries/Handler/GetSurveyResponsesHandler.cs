using Application.Common;
using Application.Features.Survey.DTOs;
using Application.Features.Survey.Queries.Model;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Survey.Queries.Handler
{
    public class GetSurveyResponsesHandler : IRequestHandler<GetSurveyResponsesQuery, BaseResponse<List<SurveyResponseDetailsDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetSurveyResponsesHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<BaseResponse<List<SurveyResponseDetailsDto>>> Handle(GetSurveyResponsesQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<SurveyResponseDetailsDto>>.BadRequestResponse("معلمات ترقيم الصفحات غير صالحة");
            }

            // Get survey with questions
            var survey = await _unitOfWork.ISurvey.NewFindRowAsync(
                s => s.Id == request.SurveyId,
                                orderBy: s => s.CreatedAt, acsending: false,
                s => s.Questions
            );

            var surveyData = survey.FirstOrDefault();
            if (surveyData == null)
            {
                return BaseResponse<List<SurveyResponseDetailsDto>>.FailureResponse("الاستبيان غير موجود");
            }

            // Get responses with answers
            var responses = await _unitOfWork.ISurveyResponse.NewFindRowAsync(
                sr => sr.SurveyId == request.SurveyId,
                
                                orderBy: s => s.CreatedDate, acsending: false,
                sr => sr.Answers
            );

            // Get user info
            var userIds = responses.Select(r => r.UserId).Distinct().ToList();
            var users = new List<ApplicationUser>();
            foreach (var userId in userIds)
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user != null)
                    users.Add(user);
            }

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var searchLower = request.Search.ToLower();
                var filteredUserIds = users
                    .Where(u => u.UserName.ToLower().Contains(searchLower) || 
                                u.Email.ToLower().Contains(searchLower))
                    .Select(u => u.Id)
                    .ToHashSet();
                
                responses = responses.Where(r => filteredUserIds.Contains(r.UserId)).ToList();
            }

            // Build result
            var result = responses.Select(r =>
            {
                var user = users.FirstOrDefault(u => u.Id == r.UserId);

                return new SurveyResponseDetailsDto
                {
                    ResponseId = r.Id,
                    UserName = user?.FullName  ?? "غير معروف",
                    UserEmail = user?.Email ?? "",
                    SubmittedAt = r.SubmittedAt,
                    Answers = r.Answers.Select(a =>
                    {
                        var question = surveyData.Questions?.FirstOrDefault(q => q.Id == a.questionId);
                        List<string>? options = null;
                        
                        // Parse options for MultipleChoice questions
                        if (question?.QuestionType == QuestionTypeEnum.MultipleChoice && !string.IsNullOrWhiteSpace(question.Options))
                        {
                            try
                            {
                                options = JsonSerializer.Deserialize<List<string>>(question.Options);
                            }
                            catch
                            {
                                options = null;
                            }
                        }
                        
                        return new AnswerDetailDto
                        {
                            QuestionId = a.questionId,
                            QuestionText = question?.QuestionText ?? "",
                            QuestionType = (int)(question?.QuestionType ?? QuestionTypeEnum.textual),
                            Answer = a.Answer ?? "",
                            Options = options
                        };
                    }).ToList()
                };
            }).OrderByDescending(r => r.SubmittedAt).ToList();

            // Get total count for pagination
            var totalCount = result.Count;

            // Apply pagination
            var pagedResult = result
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return BaseResponse<List<SurveyResponseDetailsDto>>.SuccessResponse(
                pagedResult,
                request.PageNumber,
                request.PageSize,
                totalCount,
                "تم تحميل الردود بنجاح"
            );
        }
    }
}
