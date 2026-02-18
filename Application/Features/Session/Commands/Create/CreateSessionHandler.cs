using Application.Common;
using Application.Features.Session.DTOs;
using Azure;
using del.Models;
using Domain.Entities;
using Domain.Entities.Models;
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

namespace Application.Features.Session.Commands.Create
{
    public class CreateSessionHandler : IRequestHandler<CreateSessionCommand, BaseResponse<SessionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISysUnitOfWork _SysunitOfWork;
        private readonly HttpClient _httpClientFactory;
        private readonly UserManager<ApplicationUser> userManager;
        public CreateSessionHandler(IUnitOfWork unitOfWork, ISysUnitOfWork sysUnitOfWork, IHttpClientFactory httpClientFactory, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _SysunitOfWork = sysUnitOfWork;
            _httpClientFactory = httpClientFactory.CreateClient("ExternalApi");
            this.userManager = userManager;
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
            //set time period
            var lastTime=await _SysunitOfWork.ISysTimeSessionRepository.GetFirstOrderedByAsync(p => p.business_id,descending:true);
            var time = new acc_timeseg { 
                id= Guid.NewGuid().ToString(),
                business_id = lastTime.business_id + 1,
                name= request.StartTime.ToString(@"hh\:mm")+'-'+ request.EndTime.ToString(@"hh\:mm")
            };
           
            string dayName = request.SessionDate.DayOfWeek.ToString();
            var propertyNames = time.GetType()
                       .GetProperties()
                       .ToList();

            var startTimeDate = propertyNames.FirstOrDefault(p => p.Name.Contains(dayName.ToLower()+"_start", StringComparison.OrdinalIgnoreCase));
            var endtimeDate = propertyNames.FirstOrDefault(p => p.Name.Contains(dayName.ToLower()+"_end", StringComparison.OrdinalIgnoreCase));

            startTimeDate.SetValue(time, request.StartTime.Add(TimeSpan.FromMinutes(30)).ToString(@"hh\:mm"));
            endtimeDate.SetValue(time, request.EndTime.Add(TimeSpan.FromMinutes(30)).ToString(@"hh\:mm"));
            
            await _SysunitOfWork.ISysTimeSessionRepository.AddAsync(time);
            //set access level
            // List<AddAccessLevelRequest> requests = new List<AddAccessLevelRequest>();
            // requests.Add(new AddAccessLevelRequest
            //  { AreaName= "مركز تدريب دار الافتاء المصرية", Name= request.Topic, TimeSegName= time.name

            // });
            //var addAccLevel=await _httpClientFactory.PostAsJsonAsync(MainConstants.Use("accLevel/addLevel"), requests);
            // var success = await addAccLevel.Content.ReadFromJsonAsync<ExternalApiResponse<List<string>>>();
            // if (success.Message.Contains("Succeed: 0"))
            //     BaseResponse<SessionDto>.FailureResponse("Failed to create access level");
            var accLevel = new acc_level
            {
                id = Guid.NewGuid().ToString(),
                creater_code = "admin",
                creater_id = "8a807a299b0d347b019b0d355f320002",
                creater_name = "admin",
                updater_code = "admin",
                updater_id = "8a807a299b0d347b019b0d355f320002",
                updater_name = "admin",
                auth_area_id = "8a807a299b0d347b019b0d355f9a0003",
                name = request.Topic,
                timeseg_id = time.id
            };
            await _SysunitOfWork.ISysAccessLevelRepository.AddAsync(accLevel);
            await _SysunitOfWork.Complete();

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
                AccessLevelId= accLevel.id

            };
            //set access level with doors
                //get all doors names
                //get room first
                var room=await _unitOfWork.IRooms.GetByPkAsync(request.RoomId);
                var outsideId = room.AttRoomIdOutSide;
                var insideId=room.AttRoomIdinside;
                var doors =new List<DoorDto>();
            var doorOutside = await _SysunitOfWork.ISysDoorRepository.GetByPropAsync(d=>d.id.Contains(outsideId));
            var doorInside = await _SysunitOfWork.ISysDoorRepository.GetByPropAsync(d=>d.id.Contains(insideId));
                
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
            
            var addDoorsToAccLevel=await _httpClientFactory.PostAsJsonAsync(MainConstants.Use("accLevel/addLevelDoor"), request2);

            var success2 = await addDoorsToAccLevel.Content.ReadFromJsonAsync<ExternalApiResponse<List<string>>>();
            if (success2.Message.Contains("Succeed: 0"))
                BaseResponse<SessionDto>.FailureResponse("Failed to assign doors to access level");

            var users =await userManager.Users.Where(u => request.usersIds.Contains(u.Id)).ToListAsync();
            var userForSys = new List<ZkPersonCreateDto>();
            foreach(var user in users)
            {
                userForSys.Add(new ZkPersonCreateDto
                {
                    Pin=user.pin,
                    AccLevelIds=accLevel.id
                });

            }
            await _httpClientFactory.PostAsJsonAsync(MainConstants.Use("person/add"), userForSys);
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
