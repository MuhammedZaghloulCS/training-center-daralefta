using Application.Common;
using Application.Features.Survey.DTOs;
using Domain.Entities.Models;
using Domain.Enums;
using Infrastructure.Abstractions.IUnitOfWork;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// Alias for clarity
using CairoTime = Application.Common.DateTimeHelper;

namespace Application.Features.Survey.Commands.Create
{
    public class CreateSurveyHandler : IRequestHandler<CreateSurveyCommand, BaseResponse<SurveyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSurveyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SurveyDto>> Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("الأسم مطلوب");
            if (request.Name?.Length > 200)
                errors.Add("يجب ألا يتجاوز الأسم 200 حرف");

            if (errors.Any())
                return BaseResponse<SurveyDto>.FailureResponse("فشل إنشاء استبيان", errors);

            if (request.Description != null && request.Description.Length > 500)
                errors.Add("يجب ألا يتجاوز الوصف 500 حرف");
            if (request.Questions != null && request.Questions.Any())
            {
                if (request.Questions.Count > 50)
                { errors.Add("يجب ألا يتجاوز عدد الأسئلة 50 سؤال");
                    if (errors.Any())
                        return BaseResponse<SurveyDto>.FailureResponse("فشل إضافة الأسئلة", errors);
                }
                foreach (var question in request.Questions)
                {
                    if (string.IsNullOrWhiteSpace(question.QuestionText))
                        errors.Add("نص السؤال مطلوب");
                    if (question.QuestionText?.Length > 500)
                        errors.Add("يجب ألا يتجاوز نص السؤال 500 حرف");
                    if (question.QuestionType == QuestionTypeEnum.MultipleChoice &&
    string.IsNullOrWhiteSpace(question.Options))
                    {
                        errors.Add("يجب أن يحتوي السؤال على خيارات");
                    }

                    // Check for duplicate options in MultipleChoice questions
                    if (question.QuestionType == QuestionTypeEnum.MultipleChoice && !string.IsNullOrWhiteSpace(question.Options))
                    {
                        try
                        {
                            var optionsList = System.Text.Json.JsonSerializer.Deserialize<List<string>>(question.Options);
                            if (optionsList != null && optionsList.Count > 0)
                            {
                                var normalizedOptions = optionsList.Select(o => o.Trim().ToLower()).ToList();
                                var distinctOptions = normalizedOptions.Distinct().ToList();
                                if (distinctOptions.Count < normalizedOptions.Count)
                                {
                                    errors.Add($"السؤال '{question.QuestionText}' يحتوي على خيارات مكررة");
                                }
                            }
                        }
                        catch
                        {
                            // If JSON parsing fails, ignore this validation
                        }
                    }

                }
                if (errors.Any())
                    return BaseResponse<SurveyDto>.FailureResponse("فشل إضافة الأسئلة", errors);
            }

            if (errors.Any())
                return BaseResponse<SurveyDto>.FailureResponse("فشل إنشاء استبيان", errors);
            var survey = new Domain.Entities.Survey
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive,
                CreatedAt = CairoTime.Now,
                
            };

            if (request.Questions != null && request.Questions.Any())
            {
                foreach (var question in request.Questions)
                {
                    var newQuestion= new Domain.Entities.Question
                    {
                        QuestionText = question.QuestionText,
                        IsRequired = question.IsRequired,
                        Options = question.QuestionType == QuestionTypeEnum.MultipleChoice
                        ? question.Options
                        : null,
                        QuestionType =question.QuestionType,
                        SortOrder = question.SortOrder,
                        Survey = survey
                    };

                    survey.Questions.Add(newQuestion);
                }
            }

            await _unitOfWork.ISurvey.AddAsync(survey);
            await _unitOfWork.Complete();

            var dto = survey.Adapt<SurveyDto>();

            return BaseResponse<SurveyDto>.SuccessResponse(dto, "Survey created successfully");
        }
    }
}
