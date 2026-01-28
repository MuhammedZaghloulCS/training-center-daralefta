using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class BuildingConfigurations : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.HasData(
        new Building
        {
            Id = 1,
            Name = "المبنى الإداري الرئيسي",
            Description = "يضم مكاتب الإدارة العليا والشؤون الإدارية والمالية",
            CreatedBy = "system",
            CreatedDate = new DateTime(2024, 1, 1)
        },
        new Building
        {
            Id = 2,
            Name = "مبنى القاعات الدراسية",
            Description = "مخصص للمحاضرات والدروس النظرية ويحتوي على قاعات مجهزة",
            CreatedBy = "system",
            CreatedDate = new DateTime(2024, 1, 1)
        },
        new Building
        {
            Id = 3,
            Name = "مبنى المعامل والتطبيقات",
            Description = "يحتوي على معامل الحاسب الآلي والمعامل العملية",
            CreatedBy = "system",
            CreatedDate = new DateTime(2024, 1, 1)
        },
        new Building
        {
            Id = 4,
            Name = "مبنى شؤون الطلاب",
            Description = "مسؤول عن تسجيل الطلاب وتقديم الخدمات الطلابية",
            CreatedBy = "system",
            CreatedDate = new DateTime(2024, 1, 1)
        },
        new Building
        {
            Id = 5,
            Name = "مبنى الخدمات",
            Description = "يضم الكافيتريا والخدمات العامة وقاعات الأنشطة",
            CreatedBy = "system",
            CreatedDate = new DateTime(2024, 1, 1)
        }
    );
        }
    }
}
