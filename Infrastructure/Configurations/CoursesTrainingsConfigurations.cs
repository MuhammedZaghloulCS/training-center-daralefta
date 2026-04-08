using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class CoursesTrainingsConfigurations : IEntityTypeConfiguration<CoursesTrainings>
    {
        public void Configure(EntityTypeBuilder<CoursesTrainings> builder)
        {
            builder.HasKey(ct => new { ct.CourseId, ct.TrainingId });

            builder.HasOne(ct => ct.Course)
                   .WithMany(c => c.CoursesTrainings)
                   .HasForeignKey(ct => ct.CourseId);
            builder.HasOne(ct => ct.Training)
                     .WithMany(ct => ct.CoursesTrainings)
                     .HasForeignKey(ct => ct.TrainingId);
        }
    }
}
