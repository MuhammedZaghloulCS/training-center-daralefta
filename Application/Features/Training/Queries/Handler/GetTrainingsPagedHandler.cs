using Application.Common;
using Application.Features.Training.DTOs;
using Application.Features.Training.Queries.Model;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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


            Expression<Func<Domain.Entities.Training, bool>> searchPredicate = null;

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                // جرّب parse من غير ما يطلع exception
                if (int.TryParse(search, out var id))
                {
                    // لو رقم → ابحث بالـ ID
                    searchPredicate = s => s.Id == id;
                }
                else
                {
                    // لو نص → ابحث نصي
                    searchPredicate = s =>
                        s.Title.Contains(search);
                       
                }
            }

            // Avoid deconstructing a tuple in the same statement as 'await' to prevent ENC0046.
            var paged = await _unitOfWork.ITraining.GetPaginatedAsync(
                request.PageNumber,
                request.PageSize,
                searchPredicate,
                t => t.Id,
                true,
                t => t.Courses,
                t => t.UsersTrainings,
                t => t.Surveys);

            var items = paged.items;
            var totalCount = paged.totalCount;

            if (items == null || !items.Any()||items.All(r=>r.IsDeleted))
            {
                return BaseResponse<List<TrainingListDTO>>.SuccessResponse(
                    new List<TrainingListDTO>(),
                    request.PageNumber,
                    request.PageSize,
                    totalCount,
                    "No training found");
            }

            var data = items.Where(r => !r.IsDeleted).Select(t => new TrainingListDTO
            {
                Id = t.Id,
                CreatedBy = t.CreatedBy,
                CreatedDate = t.CreatedDate,
                UpdatedBy = t.UpdatedBy,
                UpdatedAt = t.UpdatedAt,
                Title = t.Title,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
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
