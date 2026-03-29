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
            CreatedDate = new DateTime(2024, 1, 1),
            SysBuildingId= "8a807a299b0d347b019b0d355f9a0003"
        }
      
    );
        }
    }
}
