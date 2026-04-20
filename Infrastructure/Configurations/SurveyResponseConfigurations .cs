using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class SurveyResponseConfigurations : IEntityTypeConfiguration<SurveyResponse>
    {
        public void Configure(EntityTypeBuilder<SurveyResponse> builder)
        {
            builder.HasKey(r => r.Id);

            builder
                .HasOne(r => r.User)
                .WithMany(u => u.SurveyResponses)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(r => r.Training)
                .WithMany(t => t.SurveyResponses)
                .HasForeignKey(r => r.TrainingId)
                .OnDelete(DeleteBehavior.SetNull);

            
        }
    }
}
