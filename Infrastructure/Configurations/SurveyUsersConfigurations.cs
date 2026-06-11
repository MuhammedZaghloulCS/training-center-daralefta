using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class SurveyUsersConfigurations : IEntityTypeConfiguration<Domain.Entities.SurveyUsers>
    {
        public void Configure(EntityTypeBuilder<SurveyUsers> builder)
        {
            builder.HasKey(su => new { su.UserId, su.SurveyId });
            builder.HasOne(su => su.Survey)
                   .WithMany(s => s.SurveyUsers)
                   .HasForeignKey(su => su.SurveyId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(su => su.User)
                   .WithMany(s=>s.SurveyUsers)
                   .HasForeignKey(su => su.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
