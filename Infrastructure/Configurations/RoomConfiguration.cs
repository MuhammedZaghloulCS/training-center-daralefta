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
            /*builder.HasData(

    // مبنى إداري (Id = 1)
    new Room
    {
        Id = 1,
        Name = "قاعة الاجتماعات الكبرى",
        Capacity = 30,
        Location = "الدور الأول - المبنى الإداري",
        BuildId = 1,
        HaveProjector = true,
        CreatedBy = "system",
        CreatedDate = new DateTime(2024, 1, 1)
    },
    new Room
    {
        Id = 2,
        Name = "مكتب شؤون الموظفين",
        Capacity = 10,
        Location = "الدور الأرضي - المبنى الإداري",
        BuildId = 1,
        HaveProjector = false,
        CreatedBy = "system",
        CreatedDate = new DateTime(2024, 1, 1)
    },

    // مبنى القاعات الدراسية (Id = 2)
    new Room
    {
        Id = 3,
        Name = "قاعة محاضرات 1",
        Capacity = 80,
        Location = "الدور الثاني - مبنى القاعات الدراسية",
        BuildId = 2,
        HaveProjector = true,
        CreatedBy = "system",
        CreatedDate = new DateTime(2024, 1, 1)
    },
    new Room
    {
        Id = 4,
        Name = "قاعة محاضرات 2",
        Capacity = 60,
        Location = "الدور الأول - مبنى القاعات الدراسية",
        BuildId = 2,
        HaveProjector = true,
        CreatedBy = "system",
        CreatedDate = new DateTime(2024, 1, 1)
    },

    // مبنى المعامل (Id = 3)
    new Room
    {
        Id = 5,
        Name = "معمل حاسب آلي 1",
        Capacity = 25,
        Location = "الدور الأرضي - مبنى المعامل",
        BuildId = 3,
        HaveProjector = true,
        CreatedBy = "system",
        CreatedDate = new DateTime(2024, 1, 1)
    },
    new Room
    {
        Id = 6,
        Name = "معمل شبكات",
        Capacity = 20,
        Location = "الدور الأول - مبنى المعامل",
        BuildId = 3,
        HaveProjector = false,
        CreatedBy = "system",
        CreatedDate = new DateTime(2024, 1, 1)
    },

    // مبنى شؤون الطلاب (Id = 4)
    new Room
    {
        Id = 7,
        Name = "مكتب تسجيل الطلاب",
        Capacity = 15,
        Location = "الدور الأرضي - مبنى شؤون الطلاب",
        BuildId = 4,
        HaveProjector = false,
        CreatedBy = "system",
        CreatedDate = new DateTime(2024, 1, 1)
    },

    // مبنى الخدمات (Id = 5)
    new Room
    {
        Id = 8,
        Name = "قاعة أنشطة طلابية",
        Capacity = 50,
        Location = "الدور الأول - مبنى الخدمات",
        BuildId = 5,
        HaveProjector = true,
        CreatedBy = "system",
        CreatedDate = new DateTime(2024, 1, 1)
    }
);*/
            var rooms = Enumerable.Range(1, 120).Select(i => new Room
            {
                Id = i,
                Name = $"غرفة رقم {i}",
                Capacity = 10 + (i % 50), // سعات مختلفة
                Location = $"الدور {(i % 5) + 1}",
                BuildId = (i % 5) + 1, // مباني من 1 إلى 5
                HaveProjector = i % 2 == 0,
                CreatedBy = "system",
                CreatedDate = new DateTime(2024, 1, 1)
            }).ToArray();

            builder.HasData(rooms);
        }
    }
}
