using Application.Common;
using Application.Features.Session.DTOs;
using Application.Features.Training.DTOs;
using Application.Features.User.DTOs;
using Application.Features.User.Queries.Model;
using Domain.Entities;
using Infrastructure.Abstractions.IUnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.User.Queries.Handler
{
    public class GetSessionUsersRangePaginatedHandler : IRequestHandler<GetSessionUsersRangePaginatedQuery, BaseResponse<List<SessionDto>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public GetSessionUsersRangePaginatedHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<List<SessionDto>>> Handle(GetSessionUsersRangePaginatedQuery request, CancellationToken cancellationToken)
        {

            var userExist= _userManager.Users.FirstOrDefault(u => u.Id == request.UserId&&!u.IsDeleted);

            if (userExist is null)
                return BaseResponse<List<SessionDto>>.NotFoundResponse("User not found for the provided ID.");


             var sessionIds = await _unitOfWork.IAssignUserSession
                .GetSessionIdsByUserId(request.UserId);



            
            if (sessionIds == null || !sessionIds.Any())
                return BaseResponse<List<SessionDto>>
                    .NotFoundResponse("No session assigned to this training.");

            var query=await _unitOfWork.ISession
                .FindRowAsync(s=>sessionIds.Contains(s.Id));

           var sessionDtos = query
                .OrderBy(u => u.Topic)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(session => new SessionDto 
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

                }).ToList();
            return BaseResponse<List<SessionDto>>.SuccessResponse(sessionDtos);
        }
    }
}
