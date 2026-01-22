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
    public class GetTrainingsPagedHandler : IRequestHandler<GetTrainingsPagedQuery, BaseResponse<List<TrainingListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTrainingsPagedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<TrainingListDTO>>> Handle(GetTrainingsPagedQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<TrainingListDTO>>.BadRequestResponse("Invalid pagination parameters");
            }

            var (items, totalCount) = await _unitOfWork.ITraining.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                null,
                t => t.Id,
                true,
                t => t.Courses,
                t => t.Users,
                t => t.Surveys);

            if (items == null || !items.Any())
            {
                return BaseResponse<List<TrainingListDTO>>.SuccessResponse(
                    new List<TrainingListDTO>(),
                    request.PageNumber,
                    request.PageSize,
                    totalCount,
                    "No training found");
            }

            var data = items.Select(t => new TrainingListDTO
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
                request.PageNumber,
                request.PageSize,
                totalCount,
                "Trainings retrieved successfully");
        }
    }
}
