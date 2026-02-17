using Application.Common;
using Application.Features.Session.DTOs;
using del.Models;
using Infrastructure.Abstractions.IUnitOfWork;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Session.Commands.Create
{
    public class CreateSessionHandler : IRequestHandler<CreateSessionCommand, BaseResponse<SessionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISysUnitOfWork _SysunitOfWork;

        public CreateSessionHandler(IUnitOfWork unitOfWork, ISysUnitOfWork sysUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _SysunitOfWork = sysUnitOfWork;
        }

        public async Task<BaseResponse<SessionDto>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Topic))
                errors.Add("Topic is required");

            if (request.RoomId < 1)
                errors.Add("RoomId is invalid");

            if (request.CourseId < 1)
                errors.Add("CourseId is invalid");

            if (request.SessionDate == default)
                errors.Add("SessionDate is required");

            if (request.EndTime <= request.StartTime)
                errors.Add("EndTime must be greater than StartTime");

            if (errors.Any())
                return BaseResponse<SessionDto>.FailureResponse("Validation failed", errors);


            var time = new acc_timeseg { 
                id= Guid.NewGuid().ToString()

            };

            string dayName = request.SessionDate.DayOfWeek.ToString();
            var propertyNames = time.GetType()
                       .GetProperties()
                       .ToList();

            var startTimeDate = propertyNames.FirstOrDefault(p => p.Name.Contains(dayName.ToLower()+"_start", StringComparison.OrdinalIgnoreCase));
            var endtimeDate = propertyNames.FirstOrDefault(p => p.Name.Contains(dayName.ToLower()+"_end", StringComparison.OrdinalIgnoreCase));

            startTimeDate.SetValue(time, request.StartTime.ToString());
            endtimeDate.SetValue(time, request.EndTime.ToString());
            
            await _SysunitOfWork.ISysTimeSessionRepository.AddAsync(time);
            var accessLevel = new acc_level
            {
                id = Guid.NewGuid().ToString(),
                start_date= request.SessionDate,
                name = request.Topic,
                end_date=request.SessionDate.AddDays(1),
                timeseg_id=time.id
            };
           
            await _SysunitOfWork.ISysAccessLevelRepository.AddAsync(accessLevel);
            var session = new Domain.Entities.Session
            {
                CreatedBy = "System",
                CreatedDate = DateTime.UtcNow,
                SessionDate = request.SessionDate,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Topic = request.Topic,
                RoomId = request.RoomId,
                CourseId = request.CourseId,
                lecturerId = request.LecturerId,
                AccessLevelId = accessLevel.id

            };

            await _unitOfWork.ISession.AddAsync(session);
            await _unitOfWork.Complete();
            await _SysunitOfWork.Complete();


            var dto = new SessionDto
            {
                Id = session.Id,
                CreatedBy = session.CreatedBy,
                CreatedDate = session.CreatedDate,
                UpdatedBy = session.UpdatedBy,
                UpdatedAt = session.UpdatedAt,
                SessionDate = session.SessionDate,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Topic = session.Topic,
                RoomId = session.RoomId,
                CourseId = session?.CourseId,
                LecturerId = session?.lecturerId
            };

            return BaseResponse<SessionDto>.SuccessResponse(dto, "Session created successfully");
        }
    }
}
