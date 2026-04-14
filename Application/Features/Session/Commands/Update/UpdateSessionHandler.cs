using Application.Common;
using Application.Common.Abstraction;
using Application.Features.Session.DTOs;
using del.Models;
using Domain.Entities;
using Domain.Entities.Models;
using Domain.Enums;
using Domain.Helper;
using Infrastructure.Abstractions.IUnitOfWork;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Session.Commands.Update
{
    public class UpdateSessionHandler : IRequestHandler<UpdateSessionCommand, BaseResponse<SessionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISysUnitOfWork _SysunitOfWork;
        private readonly HttpClient _httpClientFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFacePrintService _facePrintService;

        public UpdateSessionHandler(
            IUnitOfWork unitOfWork,
            ISysUnitOfWork sysUnitOfWork,
            IHttpClientFactory httpClientFactory,
            UserManager<ApplicationUser> userManager,
            IFacePrintService facePrintService
            )
        {
            _unitOfWork = unitOfWork;
            _SysunitOfWork = sysUnitOfWork;
            _httpClientFactory = httpClientFactory.CreateClient("ExternalApi");
            _userManager = userManager;
            _facePrintService = facePrintService;
        }

        public async Task<BaseResponse<SessionDto>> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
        {
            // ==================== Fetch Session ====================
            var session = await _unitOfWork.ISession.GetSessionById(request.Id,s=>s.LecturerersSessions);
            if (session == null || session.IsDeleted)
                return BaseResponse<SessionDto>.NotFoundResponse("Session not found");

            // ==================== Validation ====================
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
            if(request.LecturersIds.Count()==0)
                errors.Add("يجب تعيين محاضر واحد على الأقل للجلسة");

            if (errors.Any())
                return BaseResponse<SessionDto>.FailureResponse("Validation failed", errors);

            // ==================== Update Session Entity ====================
            session.SessionDate = request.SessionDate;
            session.StartTime = request.StartTime;
            session.EndTime = request.EndTime;
            session.Topic = request.Topic;
            session.RoomId = request.RoomId;
            session.CourseId = request.CourseId;
            session.UpdatedBy = request.UpdatedBy;
            session.UpdatedAt = DateTime.UtcNow;

            var existingUserIds = session.LecturerersSessions
    .Select(x => x.UserId)
    .ToList();

            // to add
            var toAdd = request.LecturersIds.Except(existingUserIds);

            // to remove
            var toRemove = session.LecturerersSessions
                .Where(x => !request.LecturersIds.Contains(x.UserId))
                .ToList();

            // remove
            foreach (var item in toRemove)
                session.LecturerersSessions.Remove(item);

            // add
            foreach (var userId in toAdd)
            {
                session.LecturerersSessions.Add(new UserSession
                {
                    UserId = userId,
                    SessionId = session.Id
                });
            }
            _unitOfWork.ISession.Update(session);

            await _unitOfWork.Complete();

            if (session.SessionDate.Date == DateTime.Now.Date)
            {
                await _facePrintService.AssignUsersToSession();
                
            }

            // ==================== Return DTO ====================
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
                CourseId = session.CourseId,
                LecturesrIds = session.LecturerersSessions.Select(l => l.UserId).ToList(),
            };

            return BaseResponse<SessionDto>.SuccessResponse(dto, "Session updated successfully");
        }
    }
}