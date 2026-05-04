using Domain.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.Repository
{
    public class DashboardInfoRepository
    {
        private ApplicationContext _context;
        private UserManager<ApplicationUser> _userManager;

        public DashboardInfoRepository(ApplicationContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<object>GetCounts()
        {
            var buildingCounts = await _context.Building.CountAsync();
            var roomsCounts = await _context.Room.CountAsync();
            var sessionsCounts = await _context.Session.CountAsync();
            var coursesCounts = await _context.Course.CountAsync();
            var trainingsCounts = await _context.Training.CountAsync();
            var usersCounts = await _userManager.Users.CountAsync();
            var surveysCounts = await _context.Survey.CountAsync();
            return new
            {
                buildingCounts,
                roomsCounts,
                sessionsCounts,
                coursesCounts,
                trainingsCounts,
                usersCounts,
                surveysCounts   
            };
        }
    }
}
