using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class UserTrainingConfiguration
      : IEntityTypeConfiguration<UsersTrainings>
    {
        public void Configure(EntityTypeBuilder<UsersTrainings> builder)
        {
            builder.HasKey(x => new { x.UserId, x.TrainingId });

            builder.HasOne(x => x.User)
                .WithMany(u => u.UsersTrainings)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Training)
                .WithMany(t => t.UsersTrainings)
                .HasForeignKey(x => x.TrainingId);
        }
    }
}
