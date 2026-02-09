using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class SessionConfigurations : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {

            var lecturerId = new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863");

            builder.HasData(
                new Session { Id = 1, SessionDate = new DateTime(2024, 1, 1), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 0, 0), Topic = "Session 1", RoomId = 20, CourseId = 1, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 2, SessionDate = new DateTime(2024, 1, 2), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 0, 0), Topic = "Session 2", RoomId = 21, CourseId = 2, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 3, SessionDate = new DateTime(2024, 1, 3), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 0, 0), Topic = "Session 3", RoomId = 22, CourseId = 3, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 4, SessionDate = new DateTime(2024, 1, 4), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 0, 0), Topic = "Session 4", RoomId = 23, CourseId = 4, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 5, SessionDate = new DateTime(2024, 1, 5), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 0, 0), Topic = "Session 5", RoomId = 24, CourseId = 5, CreatedBy = "system", lecturerId = lecturerId },

                new Session { Id = 6, SessionDate = new DateTime(2024, 1, 6), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 0, 0), Topic = "Session 6", RoomId = 25, CourseId = 6, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 7, SessionDate = new DateTime(2024, 1, 7), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 0, 0), Topic = "Session 7", RoomId = 26, CourseId = 7, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 8, SessionDate = new DateTime(2024, 1, 8), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 0, 0), Topic = "Session 8", RoomId = 27, CourseId = 8, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 9, SessionDate = new DateTime(2024, 1, 9), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 0, 0), Topic = "Session 9", RoomId = 28, CourseId = 9, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 10, SessionDate = new DateTime(2024, 1, 10), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 0, 0), Topic = "Session 10", RoomId = 29, CourseId = 10, CreatedBy = "system", lecturerId = lecturerId },

                new Session { Id = 11, SessionDate = new DateTime(2024, 1, 11), StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(13, 0, 0), Topic = "Session 11", RoomId = 30, CourseId = 11, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 12, SessionDate = new DateTime(2024, 1, 12), StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(13, 0, 0), Topic = "Session 12", RoomId = 31, CourseId = 12, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 13, SessionDate = new DateTime(2024, 1, 13), StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(13, 0, 0), Topic = "Session 13", RoomId = 32, CourseId = 13, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 14, SessionDate = new DateTime(2024, 1, 14), StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(13, 0, 0), Topic = "Session 14", RoomId = 33, CourseId = 14, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 15, SessionDate = new DateTime(2024, 1, 15), StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(13, 0, 0), Topic = "Session 15", RoomId = 34, CourseId = 15, CreatedBy = "system", lecturerId = lecturerId },

                new Session { Id = 16, SessionDate = new DateTime(2024, 1, 16), StartTime = new TimeSpan(12, 0, 0), EndTime = new TimeSpan(14, 0, 0), Topic = "Session 16", RoomId = 35, CourseId = 16, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 17, SessionDate = new DateTime(2024, 1, 17), StartTime = new TimeSpan(12, 0, 0), EndTime = new TimeSpan(14, 0, 0), Topic = "Session 17", RoomId = 36, CourseId = 17, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 18, SessionDate = new DateTime(2024, 1, 18), StartTime = new TimeSpan(12, 0, 0), EndTime = new TimeSpan(14, 0, 0), Topic = "Session 18", RoomId = 37, CourseId = 18, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 19, SessionDate = new DateTime(2024, 1, 19), StartTime = new TimeSpan(12, 0, 0), EndTime = new TimeSpan(14, 0, 0), Topic = "Session 19", RoomId = 38, CourseId = 19, CreatedBy = "system", lecturerId = lecturerId },
                new Session { Id = 20, SessionDate = new DateTime(2024, 1, 20), StartTime = new TimeSpan(12, 0, 0), EndTime = new TimeSpan(14, 0, 0), Topic = "Session 20", RoomId = 39, CourseId = 20, CreatedBy = "system", lecturerId = lecturerId }
            );


        }
    }
}
