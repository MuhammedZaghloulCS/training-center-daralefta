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
                .OnDelete(DeleteBehavior.NoAction); 

            builder
                .HasOne(r => r.Survey)
                .WithMany(s => s.SurveyResponses)
                .HasForeignKey(r => r.SurveyId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
