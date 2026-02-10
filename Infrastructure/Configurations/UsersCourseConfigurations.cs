using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class UsersCourseConfigurations : IEntityTypeConfiguration<UsersCourse>
    {
        public void Configure(EntityTypeBuilder<UsersCourse> builder)
        {
            builder.HasKey(uc => new { uc.UserId, uc.CourseId });
            builder.ToTable("UsersCourse");
            builder.HasOne(uc => uc.User)
                .WithMany(u => u.UsersCourse)
                .HasForeignKey(uc => uc.UserId);

            builder.HasOne(uc => uc.Course)
                .WithMany(c => c.UsersCourse)
                .HasForeignKey(uc => uc.CourseId);
        }
    }
}
