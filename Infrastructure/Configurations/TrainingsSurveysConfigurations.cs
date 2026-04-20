using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class TrainingsSurveysConfigurations : IEntityTypeConfiguration<Domain.Entities.TrainingsSurveys>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.TrainingsSurveys> builder)
        {
            builder.HasKey(ts => new { ts.trainingId, ts.surveyId });
        }
    }
}
