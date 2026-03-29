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
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http.Json;
using Domain.Enums;

namespace Application.Common.Implementation
{
    public class FacePrintService : IFacePrintService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISysUnitOfWork _sysUnitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpClient _httpClientFactory;

        public FacePrintService(IUnitOfWork unitOfWork, ISysUnitOfWork sysUnitOfWork, UserManager<ApplicationUser> userManager, IHttpClientFactory httpClientFactory)
        {
            this._unitOfWork = unitOfWork;
            this._sysUnitOfWork = sysUnitOfWork;
            this._userManager = userManager;
            this._httpClientFactory = httpClientFactory.CreateClient("ExternalApi");

        }
        public async Task AssignUsersToSession()
        {
            //retrieve the session for the current day
            var sessionsInLastDate = await _unitOfWork.ISession.FindRowAsync(s => s.SessionDate.Date <DateTime.Now.Date, s => s.UserSessions);
            try
            {

          
            foreach (var session in sessionsInLastDate)
            {
                var lastTimeSession = await _sysUnitOfWork.ISysTimeSessionRepository.GetByPropAsync(t=>t.remark==session.Topic);
                if (lastTimeSession != null)
                {
                    _sysUnitOfWork.ISysTimeSessionRepository.Delete(lastTimeSession);
                }
                var accLevelToDelete = await _sysUnitOfWork.ISysAccessLevelRepository.GetByPropAsync(a => a.name == session.Topic);
                if (accLevelToDelete != null)
                {
                    _sysUnitOfWork.ISysAccessLevelRepository.Delete(accLevelToDelete);
                }
            }
            await _sysUnitOfWork.Complete();
            }
            catch (Exception ex)
            {
                // Handle exceptions that may occur during the deletion process
                Console.WriteLine($"An error occurred while deleting old time sessions and access levels: {ex.Message}");
            }

            var sessionsInDate = await _unitOfWork.ISession.FindRowAsync(s => s.SessionDate.Date == DateTime.Now.Date, s => s.UserSessions);
            var accLevsWithSessionsIds = new List<(string accLevelId, int sessionId)>();
            
            #region Add Time period for the sessions in the face print system
            foreach (var session in sessionsInDate)
            {
                var lastTime = await _sysUnitOfWork.ISysTimeSessionRepository.GetFirstOrderedByAsync(p => p.business_id, descending: true);
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
                    business_id = lastTime.business_id + 1,
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


            #endregion


            #region Get the users in multiple sessions and assign them to the face print system

            var usersInSessions = sessionsInDate.SelectMany(s => s.UserSessions).GroupBy(us => us.UserId).ToList();
    
            var userIds = usersInSessions
            .Select(g => g.Key)
            .ToList();
            var users=await _userManager.Users.Where(u=>userIds.Contains(u.Id)&&!u.IsDeleted).ToListAsync();
            foreach (var userSession in usersInSessions)
            {
                var userId = userSession.Key;
                var currentUser = users.FirstOrDefault(u => u.Id == userId);
                var accLevelIds = "";
                foreach (var session in userSession)
                {
                  var accLevelId = accLevsWithSessionsIds.FirstOrDefault(a => a.sessionId == session.SessionId).accLevelId;
                    if (accLevelId != null)
                    {
                        accLevelIds += accLevelId + ",";
                    }
                }
                if (accLevelIds.EndsWith(","))
                {
                    accLevelIds = accLevelIds.Remove(accLevelIds.Length - 1);
                }
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

             
            }

            #endregion

        }
    }
}
