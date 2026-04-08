using Application.Common;
using Application.Features.Training.DTOs;
using Application.Features.Training.Queries.Model;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using Mapster;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Training.Queries.Handler
{
    public class GetTrainingsPagedHandler : IRequestHandler<GetTrainingsPagedQuery, BaseResponse<List<TrainingDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTrainingsPagedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<TrainingDto>>> Handle(GetTrainingsPagedQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 1 || request.PageSize < 1)
            {
                return BaseResponse<List<TrainingDto>>.BadRequestResponse("Invalid pagination parameters");
            }


            Expression<Func<Domain.Entities.Training, bool>> searchPredicate = s => !s.IsDeleted;

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                if (int.TryParse(search, out var id))
                {
                    searchPredicate = s => !s.IsDeleted && s.Id == id;
                }
                else
                {
                    searchPredicate = s => !s.IsDeleted && s.Title.Contains(search);
                }
            }

            var paged = await _unitOfWork.ITraining.GetTrainingsWithAllCoursesAndSessionsAndLecturersAndStudentsPagedAsync(
                request.PageNumber,
                request.PageSize,
                searchPredicate);

    
            var items = paged.Item1;
            var totalCount = paged.totalNumber;

            if (items == null || !items.Any()||items.All(r=>r.IsDeleted))
            {
                return BaseResponse<List<TrainingDto>>.SuccessResponse(
                    new List<TrainingDto>(),
                    request.PageNumber,
                    request.PageSize,
                    totalCount,
                    "No training found");
            }

            List<TrainingDto> data = new List<TrainingDto>();

            foreach (var training in items)
            {
                var t=new TrainingDto
                {
                    Id = training.Id,
                    CreatedBy = training.CreatedBy,
                    CreatedDate = training.CreatedDate,
                    UpdatedBy = training.UpdatedBy,
                    UpdatedAt = training.UpdatedAt,
                    Title = training.Title,
                    StartDate = training.StartDate,
                    EndDate = training.EndDate,
                    Sessions = training.Sessions?.ToList() ?? new List<Domain.Entities.Session>(),
                    CoursesTrainings = training.CoursesTrainings?.ToList() ?? new List<Domain.Entities.CoursesTrainings>(),
                    UsersTrainings = training.UsersTrainings?.ToList() ?? new List<Domain.Entities.UsersTrainings>(),
                };
                data.Add(t);
            }

            return BaseResponse<List<TrainingDto>>.SuccessResponse(
                data,
                request.PageNumber,
                request.PageSize,
                totalCount,
                "Trainings retrieved successfully");
        }
    }
}
