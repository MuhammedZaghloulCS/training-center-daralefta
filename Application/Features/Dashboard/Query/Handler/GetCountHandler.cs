using Application.Common;

using Application.Features.Dashboard.Query.Model;

using Domain.Entities;

using Infrastructure.Context;

using Infrastructure.Implementations.Repository;

using MediatR;

using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

using System;

using System.Collections.Generic;

using System.Text;



namespace Application.Features.Dashboard.Query.Handler

{

    public class GetCountHandler : IRequestHandler<GetCountsQuery, BaseResponse<Object>>

    {

        private ApplicationContext _context;

        private UserManager<ApplicationUser> _userManager;



        public GetCountHandler(ApplicationContext context, UserManager<ApplicationUser> userManager)

        {

            _context = context;

            _userManager = userManager;

        }

        public async Task<BaseResponse<object>> Handle(GetCountsQuery request, CancellationToken cancellationToken)

        {

            var buildingCounts = await _context.Building.CountAsync(p=>p.IsDeleted==false);

            var roomsCounts = await _context.Room.CountAsync(p => p.IsDeleted == false);

            var sessionsCounts = await _context.Session.CountAsync(p => p.IsDeleted == false);

            var coursesCounts = await _context.Course.CountAsync(p => p.IsDeleted == false);

            var trainingsCounts = await _context.Training.CountAsync(p => p.IsDeleted == false);

            var usersCounts = await _userManager.Users.CountAsync(p => p.IsDeleted == false);
            var surveysCounts = await _context.Survey.CountAsync();

            var counts= new

            {

                buildingCounts,

                roomsCounts,

                sessionsCounts,

                coursesCounts,

                trainingsCounts,

                usersCounts,
                surveysCounts

            };

            return BaseResponse<object>.SuccessResponse(counts);

        }

    }

}

