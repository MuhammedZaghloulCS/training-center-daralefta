using Application.Common;
using Application.Common.Abstraction;
using Application.Features.Session.DTOs;
using Azure;
using del.Models;
using Domain.Entities;
using Domain.Entities.Models;
using Domain.Enums;
using Domain.Helper;
using Infrastructure.Abstractions.IUnitOfWork;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using Infrastructure.Implementations.UnitOfWork.SysUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Session.Commands.Create
{
    public class CreateSessionHandler : IRequestHandler<CreateSessionCommand, BaseResponse<SessionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISysUnitOfWork _SysunitOfWork;
        private readonly HttpClient _httpClientFactory;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IFacePrintService _facePrintService;
        public CreateSessionHandler(IUnitOfWork unitOfWork,
            ISysUnitOfWork sysUnitOfWork,
            IHttpClientFactory httpClientFactory,
            UserManager<ApplicationUser> userManager,
            IFacePrintService facePrintService)
        {
            _unitOfWork = unitOfWork;
            _SysunitOfWork = sysUnitOfWork;
            _httpClientFactory = httpClientFactory.CreateClient("ExternalApi");
            this.userManager = userManager;
            this._facePrintService = facePrintService;
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
            if (request.SessionDate < DateTime.Now)
                errors.Add("Session Date should be in the future");

            if (request.EndTime <= request.StartTime)
                errors.Add("EndTime must be greater than StartTime");

            if (errors.Any())
                return BaseResponse<SessionDto>.FailureResponse("Validation failed", errors);
            //give lecturer the same privilages of rest of users
            request.usersIds.Add(request.LecturerId);
       

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
            };



            await _unitOfWork.ISession.AddAsync(session);
            await _unitOfWork.Complete();
            var usersSession = request.usersIds.Select(s => new UserSession { SessionId = session.Id, UserId = s });
            await _unitOfWork.IAssignUserSession.AddRangeIfNotExistsAsync(usersSession.ToList());
            await _unitOfWork.Complete();
            if (session.SessionDate.Date == DateTime.Today)
            {
                await _facePrintService.AssignUsersToSession();
            }
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
