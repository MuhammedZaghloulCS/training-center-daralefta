using Application.Common;
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

        public UpdateSessionHandler(
            IUnitOfWork unitOfWork,
            ISysUnitOfWork sysUnitOfWork,
            IHttpClientFactory httpClientFactory,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _SysunitOfWork = sysUnitOfWork;
            _httpClientFactory = httpClientFactory.CreateClient("ExternalApi");
            _userManager = userManager;
        }

        public async Task<BaseResponse<SessionDto>> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
        {
            // ==================== Fetch Session ====================
            var session = await _unitOfWork.ISession.GetByPkAsync(request.Id);
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

            if (errors.Any())
                return BaseResponse<SessionDto>.FailureResponse("Validation failed", errors);

            // ==================== Update Time Segment ====================
            var accLevel = await _SysunitOfWork.ISysAccessLevelRepository.GetByPropAsync(a => a.id == session.AccessLevelId);

            if (accLevel != null)
            {
                var timeSegToUpdate = await _SysunitOfWork.ISysTimeSessionRepository.GetByPropAsync(t => t.id == accLevel.timeseg_id);

                if (timeSegToUpdate != null)
                {
                    // Reset all _start/_end properties to "00:00"
                    var propertyNames = timeSegToUpdate.GetType().GetProperties().ToList();
                    foreach (var property in propertyNames)
                    {
                        if ((property.Name.Contains("_start", StringComparison.OrdinalIgnoreCase) ||
                             property.Name.Contains("_end", StringComparison.OrdinalIgnoreCase))
                            && !property.Name.Contains("end_date", StringComparison.OrdinalIgnoreCase)
                            && !property.Name.Contains("start_date", StringComparison.OrdinalIgnoreCase))
                        {
                            property.SetValue(timeSegToUpdate, "00:00");
                        }
                    }

                    // Set the new day's start/end
                    string dayName = request.SessionDate.DayOfWeek.ToString().ToLower();
                    var startTimeProp = propertyNames.FirstOrDefault(p => p.Name.Contains(dayName + "_start", StringComparison.OrdinalIgnoreCase));
                    var endTimeProp = propertyNames.FirstOrDefault(p => p.Name.Contains(dayName + "_end", StringComparison.OrdinalIgnoreCase));

                    startTimeProp?.SetValue(timeSegToUpdate, request.StartTime.Add(TimeSpan.FromMinutes(30)).ToString(@"hh\:mm"));
                    endTimeProp?.SetValue(timeSegToUpdate, request.EndTime.Add(TimeSpan.FromMinutes(30)).ToString(@"hh\:mm"));

                    timeSegToUpdate.name = request.StartTime.ToString(@"hh\:mm") + '-' + request.EndTime.ToString(@"hh\:mm");
                    timeSegToUpdate.remark = request.Topic;
                    timeSegToUpdate.update_time = DateTime.Now;
                    timeSegToUpdate.updater_code = "admin";
                    timeSegToUpdate.updater_id = "8a807a299b0d347b019b0d355f320002";
                    timeSegToUpdate.updater_name = "admin";

                    _SysunitOfWork.ISysTimeSessionRepository.Update(timeSegToUpdate);
                }

                // ==================== Update Access Level Name (if topic changed) ====================
                if (!accLevel.name.Equals(request.Topic, StringComparison.OrdinalIgnoreCase))
                {
                    string newName = request.Topic;
                    var existing = await _SysunitOfWork.ISysAccessLevelRepository.GetByPropAsync(a => a.name.Equals(request.Topic));
                    if (existing?.name is not null && existing.id != accLevel.id)
                        newName = newName + Guid.NewGuid().ToString("N")[..5];

                    accLevel.name = newName;
                    accLevel.update_time = DateTime.Now;
                    accLevel.updater_code = "admin";
                    accLevel.updater_id = "8a807a299b0d347b019b0d355f320002";
                    accLevel.updater_name = "admin";

                    _SysunitOfWork.ISysAccessLevelRepository.Update(accLevel);
                }

                // ==================== Update Doors (if room changed) ====================
                if (session.RoomId != request.RoomId)
                {
                    var newRoom = await _unitOfWork.IRooms.GetByPkAsync(request.RoomId);
                    var doorOutside = await _SysunitOfWork.ISysDoorRepository.GetByPropAsync(d => d.id.Contains(newRoom.AttRoomIdOutSide));
                    var doorInside = await _SysunitOfWork.ISysDoorRepository.GetByPropAsync(d => d.id.Contains(newRoom.AttRoomIdinside));

                    // Replace doors on the access level via external API
                    var doorsRequest = new List<AccessLevelDoorDto>
                    {
                        new AccessLevelDoorDto { DoorName = doorOutside.name, LevelName = accLevel.name },
                        new AccessLevelDoorDto { DoorName = doorInside.name,  LevelName = accLevel.name }
                    };

                    var addDoorsResponse = await _httpClientFactory.PostAsJsonAsync(MainConstants.Use("accLevel/addLevelDoor"), doorsRequest);
                    var addDoorsResult = await addDoorsResponse.Content.ReadFromJsonAsync<ExternalApiResponse<List<string>>>();

                    if (addDoorsResult.Message.Contains("Succeed: 0"))
                        return BaseResponse<SessionDto>.FailureResponse("Failed to assign doors to access level");
                }
            }

            // ==================== Sync Users ====================
            // Current assigned users
            var currentUserIds = await _unitOfWork.IAssignUserSession.GetUsersIdsBySessionId( session.Id);
   

            var requestedUserIds = request.usersIds ?? new List<Guid>();

            var usersToAdd = requestedUserIds.Except(currentUserIds).ToList();
            var usersToRemove = currentUserIds.Except(requestedUserIds).ToList();

            // --- Remove users ---
            if (usersToRemove.Any())
            {
                // Remove UserSession records
                var sessionRecordsToRemove = currentUserIds.Select(uid => new UserSession { SessionId = session.Id, UserId = uid });
                _unitOfWork.IAssignUserSession.RemoveRange(sessionRecordsToRemove.ToList());

                // Remove from acc_level_person
                if (accLevel != null)
                {
                    var removedAppUsers = await _userManager.Users
                        .Where(u => usersToRemove.Contains(u.Id))
                        .ToListAsync();

                    var removedPins = removedAppUsers.Select(u => u.pin).ToList();
                    var removedSysPersons = await _SysunitOfWork.ISysPersonRepository.GetAllByPropAsync(p => removedPins.Contains(p.pin));
                    var removedSysIds = removedSysPersons.Select(p => p.id).ToList();

                    var accPersonsToRemove = await _SysunitOfWork.ISysAccessLevelPersonRepository
                        .GetAllByPropAsync(ap => ap.level_id == accLevel.id && removedSysIds.Contains(ap.pers_person_id));

                    _SysunitOfWork.ISysAccessLevelPersonRepository.RemoveRange(accPersonsToRemove.ToList());
                }
            }

            // --- Add new users ---
            if (usersToAdd.Any())
            {
                var newAppUsers = await _userManager.Users
                    .Where(u => usersToAdd.Contains(u.Id) && !u.IsDeleted)
                    .ToListAsync();

                // Add to sys if not already there
                var allSysPersonPins = (await _SysunitOfWork.ISysPersonRepository.GetAllAsync()).Select(u => u.pin).ToList();

                var lastSysPerson = await _SysunitOfWork.ISysPersonRepository
                    .GetFirstOrderedByAsync<int>(p => Convert.ToInt32(p.pin), descending: true);

                var lastUserWithPin = await _userManager.Users
                    .Where(u => u.pin != null)
                    .OrderByDescending(u => Convert.ToInt64(u.pin))
                    .FirstOrDefaultAsync();

                int sysPin = int.TryParse(lastSysPerson?.pin, out var s) ? s : 0;
                int userPin = int.TryParse(lastUserWithPin?.pin, out var u) ? u : 0;
                var lastId = Math.Max(sysPin, userPin);

                var diffUsers = newAppUsers.Where(u => !allSysPersonPins.Contains(u.pin)).ToList();
                var newSysPersons = diffUsers.Select(old => new pers_person
                {
                    id = Guid.NewGuid().ToString("N"),
                    create_time = DateTime.Now,
                    creater_code = "admin",
                    creater_id = "8a807a299b0d347b019b0d355f320002",
                    creater_name = "admin",
                    op_version = 0,
                    update_time = DateTime.Now,
                    updater_code = "admin",
                    updater_id = "8a807a299b0d347b019b0d355f320002",
                    updater_name = "admin",
                    auth_dept_id = "8a807a299b0d347b019b0d355fb10004",
                    enabled_credential = true,
                    exception_flag = 0,
                    gender = old.Gender.GetDescription(),
                    id_card = "",
                    id_card_physical_no = "",
                    is_from = "PERS_USER_MANUALLY_ADDED",
                    is_sendmail = false,
                    last_name = old.LastName,
                    mobile_phone = old.PhoneNumber,
                    name = old.FirstName,
                    name_spell = old.FullName,
                    number_pin = lastId,
                    person_pwd = old.UserName,
                    person_type = 0,
                    pin = (++lastId).ToString(),
                    pin_letter = false,
                    self_pwd = old.UserName,
                    send_app = true,
                    send_sms = false,
                    status = 0
                }).ToList();

                if (newSysPersons.Any())
                {
                    await _SysunitOfWork.ISysPersonRepository.AddRangeAsync(newSysPersons);
                    await _SysunitOfWork.Complete();
                }

                // Link new users to access level
                if (accLevel != null)
                {
                    var newUserPins = newAppUsers.Select(u => u.pin).ToList();
                    var newSysPersonsAll = await _SysunitOfWork.ISysPersonRepository.GetAllByPropAsync(p => newUserPins.Contains(p.pin));

                    var newAccPersons = newSysPersonsAll.Select(p => new acc_level_person
                    {
                        id = Guid.NewGuid().ToString("N"),
                        create_time = DateTime.Now,
                        creater_code = "admin",
                        creater_id = "8a807a299b0d347b019b0d355f320002",
                        creater_name = "admin",
                        op_version = 0,
                        update_time = DateTime.Now,
                        updater_code = "admin",
                        updater_id = "8a807a299b0d347b019b0d355f320002",
                        updater_name = "admin",
                        pers_person_id = p.id,
                        level_id = accLevel.id
                    }).ToList();

                    await _SysunitOfWork.ISysAccessLevelPersonRepository.AddRangeAsync(newAccPersons);
                }

                // Add new UserSession records
                var newUserSessions = usersToAdd.Select(uid => new UserSession { SessionId = session.Id, UserId = uid });
                await _unitOfWork.IAssignUserSession.AddRangeIfNotExistsAsync(newUserSessions.ToList());
            }

            // ==================== Update Session Entity ====================
            session.SessionDate = request.SessionDate;
            session.StartTime = request.StartTime;
            session.EndTime = request.EndTime;
            session.Topic = request.Topic;
            session.RoomId = request.RoomId;
            session.CourseId = request.CourseId;
            session.lecturerId = request.LecturerId;
            session.UpdatedBy = request.UpdatedBy;
            session.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.ISession.Update(session);

            await _SysunitOfWork.Complete();
            await _unitOfWork.Complete();

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
                LecturerId = session.lecturerId
            };

            return BaseResponse<SessionDto>.SuccessResponse(dto, "Session updated successfully");
        }
    }
}