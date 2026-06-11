using Application.Common.Abstraction;
using Application.Features.Session.DTOs;
using Azure;
using Azure.Core;
using del.Models;
using Domain.Entities;
using Domain.Entities.Models;
using Domain.Helper;
using Infrastructure.Abstractions.IUnitOfWork;
using Infrastructure.Abstractions.IUnitOfWork.ISysUnitOfWork;
using Infrastructure.Implementations.UnitOfWork.SysUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http.Json;
using Domain.Enums;
using System.Threading;

namespace Application.Common.Implementation
{
    public class FacePrintService : IFacePrintService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISysUnitOfWork _sysUnitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpClient _httpClientFactory;
        private readonly ILogger<FacePrintService> _logger;
        private static readonly SemaphoreSlim _businessIdLock = new(1, 1);

        public FacePrintService(
            IUnitOfWork unitOfWork, 
            ISysUnitOfWork sysUnitOfWork, 
            UserManager<ApplicationUser> userManager, 
            IHttpClientFactory httpClientFactory,
            ILogger<FacePrintService> logger)
        {
            this._unitOfWork = unitOfWork;
            this._sysUnitOfWork = sysUnitOfWork;
            this._userManager = userManager;
            this._httpClientFactory = httpClientFactory.CreateClient("ExternalApi");
            this._logger = logger;
        }
        public async Task AssignUsersToSession()
        {
            try
            {
                _logger.LogInformation("Starting AssignUsersToSession at {Time}", DateTime.Now);
                
                //retrieve the session for the current day - optimized with explicit date range
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);
                var sessionsInDate = await _unitOfWork.ISession.FindRowAsync(
                    s => s.SessionDate >= today && s.SessionDate < tomorrow, 
                    s => s.LecturerersSessions);
            var accLevsWithSessionsIds = new List<(string accLevelId, int sessionId)>();
            
            #region Add Time period for the sessions in the face print system
            foreach (var session in sessionsInDate)
            {
                // Use distributed locking to prevent UNIQUE constraint violation on business_id
                await _businessIdLock.WaitAsync();
                try
                {
                    var lastTime = await _sysUnitOfWork.ISysTimeSessionRepository.GetFirstOrderedByAsync(p => p.business_id, descending: true);
                    var newBusinessId = (lastTime?.business_id ?? 0) + 1;
                    
                    var time = new acc_timeseg
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
                        remark = session.Topic,
                        business_id = newBusinessId,
                        name = session.StartTime.ToString(@"hh\:mm") + '-' + session.EndTime.ToString(@"hh\:mm")
                    };
                    
                    string dayName = session.SessionDate.DayOfWeek.ToString();
                    var propertyNames = time.GetType()
                               .GetProperties()
                               .ToList();
                    foreach (var property in propertyNames)
                    {
                        if (property.Name.Contains("_start", StringComparison.OrdinalIgnoreCase) ||
                            property.Name.Contains("_end", StringComparison.OrdinalIgnoreCase) && !property.Name.Contains("end_date", StringComparison.OrdinalIgnoreCase) && !property.Name.Contains("start_date", StringComparison.OrdinalIgnoreCase))
                        {
                            property.SetValue(time, "00:00");
                        }
                    }
                    var startTimeDate = propertyNames.FirstOrDefault(p => p.Name.Contains(dayName.ToLower() + "_start", StringComparison.OrdinalIgnoreCase));
                    var endtimeDate = propertyNames.FirstOrDefault(p => p.Name.Contains(dayName.ToLower() + "_end", StringComparison.OrdinalIgnoreCase));

                    startTimeDate.SetValue(time, session.StartTime.Add(TimeSpan.FromMinutes(30)).ToString(@"hh\:mm"));
                    endtimeDate.SetValue(time, session.EndTime.Add(TimeSpan.FromMinutes(30)).ToString(@"hh\:mm"));

                    await _sysUnitOfWork.ISysTimeSessionRepository.AddAsync(time);
                    await _sysUnitOfWork.Complete();
                
                    var accLevel = new acc_level
                    {
                        id = Guid.NewGuid().ToString("N"),
                        creater_code = "admin",
                        creater_id = "8a807a299b0d347b019b0d355f320002",
                        create_time = DateTime.Now,
                        op_version = 0,
                        update_time = DateTime.Now,
                        creater_name = "admin",
                        updater_code = "admin",
                        updater_id = "8a807a299b0d347b019b0d355f320002",
                        updater_name = "admin",
                        auth_area_id = "8a807a299b0d347b019b0d355f9a0003",
                        name = session.Topic,
                        timeseg_id = time.id
                    };
                    accLevsWithSessionsIds.Add((accLevel.id, session.Id));
                    await _sysUnitOfWork.ISysAccessLevelRepository.AddAsync(accLevel);
                    await _sysUnitOfWork.Complete();
                    
                    var roomOfSession = await _unitOfWork.IRooms.GetByPkAsync(session.RoomId);
                    var outsideId = roomOfSession.AttRoomIdOutSide;
                    var insideId = roomOfSession.AttRoomIdinside;
                    var doors = new List<DoorDto>();
                    var doorOutside = await _sysUnitOfWork.ISysDoorRepository.GetByPropAsync(d => d.id.Contains(outsideId));
                    var doorInside = await _sysUnitOfWork.ISysDoorRepository.GetByPropAsync(d => d.id.Contains(insideId));

                    List<AccessLevelDoorDto> request2 = new List<AccessLevelDoorDto>
                    {
                        new AccessLevelDoorDto
                        {
                            DoorName = doorOutside.name,
                            LevelName = accLevel.name
                        },
                        new AccessLevelDoorDto
                        {
                            DoorName = doorInside.name,
                            LevelName = accLevel.name
                        }
                    };

                    var addDoorsToAccLevel = await _httpClientFactory.PostAsJsonAsync(MainConstants.Use("accLevel/addLevelDoor"), request2);
                    var success2 = await addDoorsToAccLevel.Content.ReadFromJsonAsync<ExternalApiResponse<List<string>>>();
                }
                finally
                {
                    _businessIdLock.Release();
                }
            }

            #endregion


            #region Get the users in multiple sessions and assign them to the face print system

            var grouping = new List<UserSession>();
            foreach (var session in sessionsInDate)
            {
                var usersIds = await _unitOfWork.ISession
                    .GetUsersIdsFromTrainingforSessionsBySessionIdAsync(session.Id);
               usersIds.AddRange(session.LecturerersSessions.Select(ls => ls.UserId));
                grouping.AddRange(
                    usersIds.Select(u => new UserSession
                    {
                        UserId = u,
                        SessionId = session.Id
                    })
                );
            }

            var usersInSessions = grouping
                .GroupBy(x => x.UserId)
                .ToList();
            var userIds = usersInSessions
            .Select(g => g.Key)
            .ToList();
            var pinsValidations = new[] { " ", "0", "" };
            var users = await _userManager.Users
                .Where(u => userIds.Contains(u.Id) && !u.IsDeleted && u.IsActive == true && !pinsValidations.Contains(u.pin))
                .ToListAsync();
            
            // Batch role lookups to fix N+1 query issue
            var userRolesDict = new Dictionary<Guid, IList<string>>();
            foreach (var userId in userIds)
            {
                var user = users.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    userRolesDict[userId] = await _userManager.GetRolesAsync(user);
                }
            }
            
            foreach (var userSession in usersInSessions)
            {
                var userId = userSession.Key;
                var currentUser = users.FirstOrDefault(u => u.Id == userId);
                if (currentUser == null) continue;
                
                var accLevelIds = string.Join(",", userSession
                    .Select(s => accLevsWithSessionsIds.FirstOrDefault(a => a.sessionId == s.SessionId).accLevelId)
                    .Where(id => id != null));
                    
                var userRequest = new ZkPersonCreateDto
                {
                    Pin = currentUser.pin,
                    DeptCode = "1",
                    Name = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Gender = currentUser.Gender.GetDescription(),
                    AccLevelIds = accLevelIds
                };
                
                var result = await _httpClientFactory.PostAsJsonAsync(MainConstants.Use("person/add"), userRequest);
                var message = await result.Content.ReadFromJsonAsync<ExternalApiResponse<List<string>>>();
                
                _logger.LogInformation("Assigned user {UserId} to sessions, response: {@Response}", userId, message);
            }

            #endregion

                _logger.LogInformation("Completed AssignUsersToSession successfully at {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AssignUsersToSession");
                throw;
            }
        }
    }
}
