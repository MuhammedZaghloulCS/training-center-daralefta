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
            var trainings = new List<Training>();

            var baseDate = new DateTime(2025, 01, 01);

            for (int i = 1; i <= 30; i++)
            {
                trainings.Add(new Training
                {
                    Id = i,
                    Title = $"Training {i}",

                    StartDate = baseDate.AddDays(i),
                    EndDate = baseDate.AddDays(i + 5),

                    CreatedBy = "System",
                    CreatedDate = baseDate,

                    UpdatedBy = null,
                    UpdatedAt = null
                });
            }

            builder.HasData(trainings);
        }
    }
}
