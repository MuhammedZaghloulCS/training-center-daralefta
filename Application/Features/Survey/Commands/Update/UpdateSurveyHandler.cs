using Application.Common;
using Application.Features.Survey.DTOs;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Survey.Commands.Update
{
    public class UpdateSurveyHandler : IRequestHandler<UpdateSurveyCommand, BaseResponse<SurveyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSurveyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<SurveyDto>> Handle(UpdateSurveyCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            // =========================
            // Validation
            // =========================
            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("الأسم مطلوب");

            if (request.Name?.Length > 200)
                errors.Add("يجب ألا يتجاوز الأسم 200 حرف");

            if (request.Description != null && request.Description.Length > 500)
                errors.Add("يجب ألا يتجاوز الوصف 500 حرف");

            if (request.Questions != null)
            {
                if (request.Questions.Count > 50)
                    errors.Add("يجب ألا يتجاوز عدد الأسئلة 50 سؤال");

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
            }

            if (errors.Any())
                return BaseResponse<SurveyDto>.FailureResponse("فشل تعديل الإستبيان", errors);

            // =========================
            // Get Survey with Questions
            // =========================
            var survey = await _unitOfWork.ISurvey.GetSurveyWithQuestion(request.Id);

            if (survey == null)
                return BaseResponse<SurveyDto>.FailureResponse("الاستبيان غير موجود");

            // =========================
            // Update Parent
            // =========================
            survey.Name = request.Name;
            survey.Description = request.Description;
            survey.IsActive = request.IsActive;

            // =========================
            // Sync Questions (Update / Delete)
            // =========================
            var requestQuestions = request.Questions ?? new List<QuestionDto>();

            foreach (var existing in survey.Questions.ToList())
            {
                var updated = requestQuestions.FirstOrDefault(q => q.Id == existing.Id);

                if (updated == null)
                {
                    // Delete
                    survey.Questions.Remove(existing);
                }
                else
                {
                    // Update
                    existing.QuestionText = updated.QuestionText;
                    existing.QuestionType = updated.QuestionType;
                    existing.Options = updated.Options;
                    existing.IsRequired = updated.IsRequired;
                    existing.SortOrder = updated.SortOrder;
                }
            }

            // =========================
            // Add New Questions
            // =========================
            var newQuestions = requestQuestions
                .Where(q => q.Id == 0)
                .Select(q => new Question
                {
                    QuestionText = q.QuestionText,
                    QuestionType = q.QuestionType,
                    Options = q.Options,
                    IsRequired = q.IsRequired,
                    SortOrder = q.SortOrder
                });

            foreach (var q in newQuestions)
            {
                survey.Questions.Add(q);
            }
            // =========================
            // Save Changes
            // =========================
            await _unitOfWork.Complete();

            // =========================
            // Map to DTO
            // =========================
            var dto = new SurveyDto
            {
                Id = survey.Id,
                Name = survey.Name,
                Description = survey.Description,
                IsActive = survey.IsActive,
                Questions = survey.Questions.Select(q => new QuestionDto
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText,
                    QuestionType = q.QuestionType,
                    Options = q.Options,
                    IsRequired = q.IsRequired,
                    SortOrder = q.SortOrder
                }).ToList()
            };

            return BaseResponse<SurveyDto>.SuccessResponse(dto);
        }
    }
}