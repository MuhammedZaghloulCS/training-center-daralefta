using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
          
            var room1 = new Room
            {
                Id = 1,
                Name = $"غرفة رقم {1}",
                Capacity = 10, 
                Location = $"الدور {  1}",
                BuildId =   1, // مباني من 1 إلى 5
                HaveProjector = 2 % 2 == 0,
                CreatedBy = "system",
                CreatedDate = new DateTime(2024, 1, 1),
                AttRoomIdOutSide= "8a807a299b0d347b019b0d983cc70712",
                AttRoomIdinside= "8a807a299b0d347b019b0d9df51b085d"
            };
            var room2 = new Room
            {
                Id = 2,
                Name = $"غرفة رقم {2}",
                Capacity = 10,
                Location = $"الدور {2}",
                BuildId = 1, // مباني من 1 إلى 5
                HaveProjector = 2 % 2 == 0,
                CreatedBy = "system",
                CreatedDate = new DateTime(2024, 1, 1),
                AttRoomIdOutSide = "8a807a299b1c0bad019b1cf94f3c0177",
                AttRoomIdinside = "8a807a299b1c0bad019b1cf987d702c6"

            };             var room3 = new Room
            {
                Id = 3,
                Name = $"غرفة رقم {3}",
                Capacity = 10,
                Location = $"الدور {3}",
                BuildId = 1, // مباني من 1 إلى 5
                HaveProjector = 2 % 2 == 0,
                CreatedBy = "system",
                CreatedDate = new DateTime(2024, 1, 1),
                AttRoomIdOutSide = "8a807a299b1c0bad019b1cf98bcd03c0",
                AttRoomIdinside = "8a807a299b1c0bad019b1cf98d7304f1"
            };

            var rooms = new List<Room> { room1, room2, room3 };
            builder.HasData(rooms);
        }
    }
}
