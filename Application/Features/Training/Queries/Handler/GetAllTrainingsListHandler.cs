using Application.Common;
using Application.Features.Training.DTOs;
using Application.Features.Training.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Training.Queries.Handler
{
    public class GetAllTrainingsListHandler : IRequestHandler<GetAllTrainingsListQuery, BaseResponse<List<TrainingListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTrainingsListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<TrainingListDTO>>> Handle(GetAllTrainingsListQuery request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.ITraining.GetAllAsync(t => t.Courses, t => t.Users, t => t.Surveys);

            if (response == null || !response.Any())
            {
                return BaseResponse<List<TrainingListDTO>>.SuccessResponse(
                    new List<TrainingListDTO>(),
                    "No training found"
                );
            }

            var data = response.Select(t => new TrainingListDTO
            {
                Id = t.Id,
                CreatedBy = t.CreatedBy,
                CreatedDate = t.CreatedDate,
                UpdatedBy = t.UpdatedBy,
                UpdatedAt = t.UpdatedAt,
                Title = t.Title,
                StartDate = t.StartDate,
                EndDate = t.EndDate
            }).ToList();

            return BaseResponse<List<TrainingListDTO>>.SuccessResponse(
                data,
                "Trainings retrieved successfully"
            );
        }
    }
}
