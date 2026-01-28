using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace Infrastructure.Configurations
{
    public class CoursesConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            var courses = Enumerable.Range(1, 20).Select(i => new Course
            {
                Id = i,
                Name = $"دورة رقم {i}",
                Description = $"وصف مختصر للدورة رقم {i}",
                Prerequisites = i % 3 == 0 ? "أساسيات الحاسوب" : "لا يوجد",
                Duration = 10 + (i % 20),
                TrainingId = null,
                CreatedBy = "system",
                CreatedDate = new DateTime(2024, 1, 1)
            }).ToArray();

            builder.HasData(courses);
        }
    }
}
