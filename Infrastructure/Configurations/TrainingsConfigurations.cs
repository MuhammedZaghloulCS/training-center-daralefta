using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Infrastructure.Configurations
{
    public class TrainingsConfigurations : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
          builder.HasMany(t => t.Sessions)
                 .WithOne(s => s.Training)
                 .HasForeignKey(s => s.TrainingId)
                 .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(t => t.TrainingsSurveys)
                 .WithOne(ct => ct.Training)
                 .HasForeignKey(ct => ct.trainingId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
