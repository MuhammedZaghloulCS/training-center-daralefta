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
            //var response = await _unitOfWork.ITraining.GetAllAsync(t => t.Courses,t => t.UsersTrainings,t=>t.Surveys);
            var response = await _unitOfWork.ITraining.GetAllAsync(t => t.UsersTrainings,t=>t.Sessions,t=>t.CoursesTrainings);

            if (response == null || !response.Any()||response.All(r=>r.IsDeleted))
            {
                return BaseResponse<List<TrainingListDTO>>.SuccessResponse(
                    new List<TrainingListDTO>(),
                    "No training found"
                );
            }
            List<Guid> lecIds = new List<Guid>();

            foreach (var training in response)
            {
                foreach (var session in training.Sessions)
                {
                    if (!session.IsDeleted)
                    {
                        var sessionLecturers = await _unitOfWork.ISession.GetByPkAsync(session.Id,t=>t.LecturerersSessions);
                        lecIds.AddRange(sessionLecturers.LecturerersSessions.Select(t=>t.UserId));
                    }
                }
            }

            var data = response.OrderByDescending(c => c.CreatedDate).Where(r => !r.IsDeleted).Select( t => new TrainingListDTO
            {
                Id = t.Id,
                CreatedBy = t.CreatedBy,
                CreatedDate = t.CreatedDate,
                UpdatedBy = t.UpdatedBy,
                UpdatedAt = t.UpdatedAt,
                Title = t.Title,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                sessionCount = t.Sessions.Count(s => !s.IsDeleted),
                LecturersIds = lecIds.Distinct().ToList(),

            }).ToList();

            return BaseResponse<List<TrainingListDTO>>.SuccessResponse(
                data,
                "Trainings retrieved successfully"
            );
        }
    }
}
