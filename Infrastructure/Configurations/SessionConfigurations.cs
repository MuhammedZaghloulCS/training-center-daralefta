using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Configurations
{
    public class SessionConfigurations : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            var lecturerId = new Guid("df8a0113-3d20-4aed-b33d-2f5bef703863");

            // 👇 AccessLevelId لازم يكون موجود كـ seed في AccessLevel table
            var accessLevelId = "DEFAULT_ACCESS";

            builder.HasData(
                new Session
                {
                    Id = 1,
                    SessionDate = new DateTime(2024, 1, 1),
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(11, 0, 0),
                    Topic = "Session 1",
                    RoomId = 20,
                    CourseId = 1,
                    CreatedBy = "system",
                    lecturerId = lecturerId,
                    AccessLevelId = accessLevelId
                },
                new Session
                {
                    Id = 2,
                    SessionDate = new DateTime(2024, 1, 2),
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(11, 0, 0),
                    Topic = "Session 2",
                    RoomId = 21,
                    CourseId = 2,
                    CreatedBy = "system",
                    lecturerId = lecturerId,
                    AccessLevelId = accessLevelId
                },
                new Session
                {
                    Id = 3,
                    SessionDate = new DateTime(2024, 1, 3),
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(11, 0, 0),
                    Topic = "Session 3",
                    RoomId = 22,
                    CourseId = 3,
                    CreatedBy = "system",
                    lecturerId = lecturerId,
                    AccessLevelId = accessLevelId
                },
                new Session
                {
                    Id = 4,
                    SessionDate = new DateTime(2024, 1, 4),
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(11, 0, 0),
                    Topic = "Session 4",
                    RoomId = 23,
                    CourseId = 4,
                    CreatedBy = "system",
                    lecturerId = lecturerId,
                    AccessLevelId = accessLevelId
                },
                new Session
                {
                    Id = 5,
                    SessionDate = new DateTime(2024, 1, 5),
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(11, 0, 0),
                    Topic = "Session 5",
                    RoomId = 24,
                    CourseId = 5,
                    CreatedBy = "system",
                    lecturerId = lecturerId,
                    AccessLevelId = accessLevelId
                },

                new Session
                {
                    Id = 6,
                    SessionDate = new DateTime(2024, 1, 6),
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(12, 0, 0),
                    Topic = "Session 6",
                    RoomId = 25,
                    CourseId = 6,
                    CreatedBy = "system",
                    lecturerId = lecturerId,
                    AccessLevelId = accessLevelId
                },
                new Session
                {
                    Id = 7,
                    SessionDate = new DateTime(2024, 1, 7),
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(12, 0, 0),
                    Topic = "Session 7",
                    RoomId = 26,
                    CourseId = 7,
                    CreatedBy = "system",
                    lecturerId = lecturerId,
                    AccessLevelId = accessLevelId
                },
                new Session
                {
                    Id = 8,
                    SessionDate = new DateTime(2024, 1, 8),
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(12, 0, 0),
                    Topic = "Session 8",
                    RoomId = 27,
                    CourseId = 8,
                    CreatedBy = "system",
                    lecturerId = lecturerId,
                    AccessLevelId = accessLevelId
                },
                new Session
                {
                    Id = 9,
                    SessionDate = new DateTime(2024, 1, 9),
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(12, 0, 0),
                    Topic = "Session 9",
                    RoomId = 28,
                    CourseId = 9,
                    CreatedBy = "system",
                    lecturerId = lecturerId,
                    AccessLevelId = accessLevelId
                },
                new Session
                {
                    Id = 10,
                    SessionDate = new DateTime(2024, 1, 10),
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(12, 0, 0),
                    Topic = "Session 10",
                    RoomId = 29,
                    CourseId = 10,
                    CreatedBy = "system",
                    lecturerId = lecturerId,
                    AccessLevelId = accessLevelId
                }
            );
        }
    }
}
