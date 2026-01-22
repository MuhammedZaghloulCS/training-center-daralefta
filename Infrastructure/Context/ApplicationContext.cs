using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.Context
{
    // Use the IdentityDbContext overload that accepts TUser, TRole, and TKey.
    // ApplicationUser uses Guid as the key type, so TRole should be IdentityRole<Guid> and TKey = Guid.
    public class ApplicationContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        //CTOR
        public ApplicationContext(DbContextOptions options) : base(options) { }

        #region Fields
        public DbSet<Building> Building { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<Survey> Survey { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyCategory> SurveyCategories { get; set; }
        public DbSet<QuestionAnswer> SurveyAnswers { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
