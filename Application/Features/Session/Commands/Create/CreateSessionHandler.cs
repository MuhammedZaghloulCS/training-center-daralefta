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
            if(request.Topic.Length>100)
                errors.Add("الموضوع لا يجب ان يتعدى 100 حرف");
            if (string.IsNullOrWhiteSpace(request.Topic))
                errors.Add("الموضوع مطلوب");

            if (request.RoomId < 1)
                errors.Add("رقم الغرفة خاطئ");

            if (request.CourseId < 1)
                errors.Add("رقم الكورس خاطئ");
            if (request.TrainingId < 1)
                errors.Add("رقم التدريب خاطئ");

            if (request.SessionDate == default)
                errors.Add("تاريح الجلسة مطلوب");
            if (request.StartTime == default)
                errors.Add("وقت البدأ مطلوب");
            if (request.EndTime == default)
                errors.Add("وقت البدأ مطلوب");
            if (request.LecturersIds.Count()==0)
                errors.Add("يجب تعيين محاضر علي الأقل");
            if (request.EndTime <= request.StartTime)
                errors.Add("وقت الانتهاء يجب ان يكون بعد وقت البدأ");

            if (errors.Any())
                return BaseResponse<SessionDto>.FailureResponse("Validation failed", errors);
            //give lecturer the same privilages of rest of users
            

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
                TrainingId = request.TrainingId,
                LecturerersSessions = request.LecturersIds.Select(s => new UserSession { UserId = s }).ToList()
            };



            await _unitOfWork.ISession.AddAsync(session);
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
           
            };

            return BaseResponse<SessionDto>.SuccessResponse(dto, "Session created successfully");
        }
    }
}
