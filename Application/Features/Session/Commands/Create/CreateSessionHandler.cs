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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


// Alias for clarity
using CairoTime = Application.Common.DateTimeHelper;

namespace Application.Features.Session.Commands.Create
{
    public class CreateSessionHandler : IRequestHandler<CreateSessionCommand, BaseResponse<SessionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISysUnitOfWork _SysunitOfWork;
        private readonly HttpClient _httpClientFactory;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IFacePrintService _facePrintService;
        private readonly ILogger<CreateSessionHandler> _logger;
        private readonly IWebHostEnvironment _env;
       
        public CreateSessionHandler(IUnitOfWork unitOfWork,
            ISysUnitOfWork sysUnitOfWork,
            IHttpClientFactory httpClientFactory,
            UserManager<ApplicationUser> userManager,
            IFacePrintService facePrintService,
            ILogger<CreateSessionHandler> logger,
            IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _SysunitOfWork = sysUnitOfWork;
            _httpClientFactory = httpClientFactory.CreateClient("ExternalApi");
            this.userManager = userManager;
            this._facePrintService = facePrintService;
            _logger = logger;
            _env = env;
        }

        public async Task<BaseResponse<SessionDto>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating session with topic: {Topic}", request.Topic);
                
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
            var now = DateTime.Now;

            // 1. تاريخ في الماضي
            if (request.SessionDate.Date < now.Date)
            {
                errors.Add("لا يمكن إنشاء جلسة بتاريخ في الماضي");
            }

            // 2. لو نفس اليوم → تحقق من الوقت
            if (request.SessionDate.Date == now.Date)
            {
                if (request.StartTime <= now.TimeOfDay)
                {
                    errors.Add("وقت البدء لا يمكن أن يكون في الماضي");
                }
            }
            var training = await _unitOfWork.ITraining.GetByPkAsync(request.TrainingId);
            if (training == null)
                errors.Add("التدريب غير موجود");
            if (training != null)
            {
                if (request.SessionDate.Date < training.StartDate.Date || request.SessionDate.Date > training.EndDate.Date)
                    errors.Add("تاريخ الجلسة يجب أن يكون ضمن فترة التدريب");
            }
            if (errors.Any())
                return BaseResponse<SessionDto>.FailureResponse("خطأ في البيانات", errors);
            
            var filesPaths = new List<string>();
                if (request.formFiles != null && request.formFiles.Count > 0)
                {  
                    foreach (var file in request.formFiles)
                    {
                        try
                        {
                            // Save to external storage folder (two levels up - outside project)
                            var storagePath = Path.GetFullPath(Path.Combine(_env.ContentRootPath, "..", "..", "TrainingCenterStorage"));
                            var uploads = Path.Combine(storagePath, "sessiondata");
                            if (!Directory.Exists(uploads))
                            {
                                Directory.CreateDirectory(uploads);
                            }

                            var extension = Path.GetExtension(file.FileName);
                            var originalName = Path.GetFileNameWithoutExtension(file.FileName);
                            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                            var fileName = $"{originalName}_{timestamp}{extension}";
                            var filePath = Path.Combine(uploads, fileName);
                            
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            // Store relative URL path
                            filesPaths.Add($"/storage/sessiondata/{fileName}");
                        }
                        catch (Exception ex)
                        {
                            // Log error but continue without image
                            Console.WriteLine($"Error saving image: {ex.Message}");
                        }
                    }

                }
                
                var session = new Domain.Entities.Session
            {
                CreatedBy = "System",
                CreatedDate = CairoTime.Now,
                SessionDate = request.SessionDate,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Topic = request.Topic,
                RoomId = request.RoomId,
                CourseId = request.CourseId,
                TrainingId = request.TrainingId,
                filesPaths=filesPaths,
                LecturerersSessions = request.LecturersIds.Select(s => new UserSession { UserId = s }).ToList()
            };



            await _unitOfWork.ISession.AddAsync(session);
            await _unitOfWork.Complete();

            _logger.LogInformation("Session {SessionId} created successfully", session.Id);

            if (session.SessionDate.Date == DateTime.Today)
            {
                _logger.LogInformation("Session {SessionId} is today, triggering AssignUsersToSession", session.Id);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating session with topic: {Topic}", request.Topic);
                throw;
            }
        }
    }
}
