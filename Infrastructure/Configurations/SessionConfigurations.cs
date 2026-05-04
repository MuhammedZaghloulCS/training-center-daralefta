using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Text.Json;

namespace Infrastructure.Configurations
{
    public class SessionConfigurations : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            // Add index on SessionDate for optimized date range queries
            builder.HasIndex(s => s.SessionDate)
                .HasDatabaseName("IX_Session_SessionDate");
            
            // Add composite index for common query patterns
            builder.HasIndex(s => new { s.SessionDate, s.RoomId })
                .HasDatabaseName("IX_Session_SessionDate_RoomId");

            // Configure filesPaths as JSON column
            builder.Property(s => s.filesPaths)
                .HasConversion(
                    v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => v == null ? null : JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
                );
        }
    }
}
