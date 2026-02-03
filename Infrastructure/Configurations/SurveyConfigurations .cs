using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{

    public class SurveyConfigurations : IEntityTypeConfiguration<Survey>
        {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.HasKey(s => s.Id);

            builder
                .HasOne(s => s.CreatedByUser)
                .WithMany(u => u.CreatedSurveys)
                .HasForeignKey(s => s.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(s => s.Training)
                .WithMany(t => t.Surveys)
                .HasForeignKey(s => s.TrainingId)
                .OnDelete(DeleteBehavior.NoAction);

            // 🔹 Seeding
            var date = new DateTime(2025, 5, 1);
            var surveys = new List<Survey>();

            for (int i = 1; i <= 30; i++)
            {
                surveys.Add(new Survey
                {
                    Id = i,
                    Title = $"Survey {i}",
                    Description = $"Description for Survey {i}",

                    CreatedBy = "System",
                    CreatedDate = date.AddDays(50),

                    TrainingId = (i % 5) + 1,        
                });
            }

            builder.HasData(surveys);
        }


    }

}
