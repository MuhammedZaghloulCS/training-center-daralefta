using Application.Common;
using Application.Features.Survey.DTOs;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Survey.Commands.Handler
{
    public class SubmitSurveyResponseHandler : IRequestHandler<SubmitSurveyResponseCommand, BaseResponse<SurveyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public SubmitSurveyResponseHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<BaseResponse<SurveyResponseDto>> Handle(SubmitSurveyResponseCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            
            // Get user for full name
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var createdBy = user?.FullName ?? userId.ToString();
            
            // Check if user already responded
            var existingResponse = await _unitOfWork.ISurveyResponse.GetFirstByPropAsync(
                sr => sr.UserId == userId && sr.SurveyId == request.SurveyId
            );

            if (existingResponse != null)
            {
                return BaseResponse<SurveyResponseDto>.NotFoundResponse("You have already responded to this survey");
            }

            // Verify survey exists and is active
            var survey = await _unitOfWork.ISurvey.GetFirstByPropAsync(
                s => s.Id == request.SurveyId && s.IsActive
            );

            if (survey == null)
            {
                return BaseResponse<SurveyResponseDto>.NotFoundResponse("Survey not found or inactive");
            }

            // Create survey response
            var surveyResponse = new Domain.Entities.SurveyResponse
            {
                UserId = userId,
                SurveyId = request.SurveyId,
                TrainingId = request.TrainingId,
                SubmittedAt = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            await _unitOfWork.ISurveyResponse.AddAsync(surveyResponse);
            await _unitOfWork.Complete();

            // Add answers
            var answers = request.Answers.Select(a => new Domain.Entities.QuestionAnswer
            {
                SurveyResponseId = surveyResponse.Id,
                questionId = a.QuestionId,
                Answer = a.Answer,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = createdBy
            }).ToList();

            await _unitOfWork.IQuestionAnswer.AddRangeAsync(answers);
            await _unitOfWork.Complete();

            var result = new SurveyResponseDto
            {
                Id = surveyResponse.Id,
                UserId = surveyResponse.UserId.ToString(),
                SurveyId = surveyResponse.SurveyId,
                TrainingId = surveyResponse.TrainingId,
                SubmittedAt = surveyResponse.SubmittedAt,
                Answers = request.Answers
            };

            return BaseResponse<SurveyResponseDto>.SuccessResponse(result, "Survey submitted successfully");
        }
    }
}
