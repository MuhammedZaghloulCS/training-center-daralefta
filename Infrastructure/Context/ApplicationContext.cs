using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions options):base(options) { }
        public DbSet<Building> Building { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<Survey> Survey { get; set; }
        public DbSet<SurveyCategory> QuestionerCategories { get; set; }
    }
}
