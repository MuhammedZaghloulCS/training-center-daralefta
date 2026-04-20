using Domain.Entities;
using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Infrastructure.Configurations
{

    public class SurveyConfigurations : IEntityTypeConfiguration<Survey>
        {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {


            builder
            .HasMany(s => s.Questions)
            .WithOne(q => q.Survey)
            .HasForeignKey(q => q.SurveyId)
            .OnDelete(DeleteBehavior.Cascade);




        }


    }

}
