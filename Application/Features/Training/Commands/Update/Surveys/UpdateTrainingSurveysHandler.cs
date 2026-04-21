using Application.Common;
using Application.Features.Training.DTOs;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;

namespace Application.Features.Training.Commands.Update.Surveys
{
    internal class UpdateTrainingSurveysHandler : IRequestHandler<UpdateTrainingSurveysCommand, BaseResponse<TrainingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTrainingSurveysHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<TrainingDto>> Handle(UpdateTrainingSurveysCommand request, CancellationToken cancellationToken)
        {
            var training = await _unitOfWork.ITraining.GetByPkAsync(request.Id);
            if (training == null || training.IsDeleted)
            {
                return BaseResponse<TrainingDto>.NotFoundResponse("Training not found");
            }

            // Get existing surveys for this training
            var existingSurveys = await _unitOfWork.ITrainingsSurveys.FindAsync(ts => ts.trainingId == request.Id);
            
            // Delete old surveys
            _unitOfWork.ITrainingsSurveys.DeleteRange(existingSurveys);
            await _unitOfWork.Complete();

            // Add new surveys
            var newSurveys = request.SurveyIds.Select(surveyId => new TrainingsSurveys
            {
                trainingId = request.Id,
                surveyId = surveyId
            }).ToList();

            await _unitOfWork.ITrainingsSurveys.AddRangeAsync(newSurveys);
            await _unitOfWork.Complete();

            var dto = new TrainingDto
            {
                Id = training.Id,
                Title = training.Title,
                StartDate = training.StartDate,
                EndDate = training.EndDate
            };

            return BaseResponse<TrainingDto>.SuccessResponse(dto, "Surveys updated successfully");
        }
    }
}
