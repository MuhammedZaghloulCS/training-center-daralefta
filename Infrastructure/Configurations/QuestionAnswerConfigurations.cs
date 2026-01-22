using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class QuestionAnswerConfigurations : IEntityTypeConfiguration<QuestionAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionAnswer> builder)
        {
            builder.ToTable("SurveyAnswers");

            builder.HasKey(a => a.Id);

            builder.HasIndex(a => new { a.SurveyResponseId, a.SurveyQuestionId })
                   .IsUnique();

            builder
                .HasOne(a => a.SurveyResponse)
                .WithMany(r => r.Answers)
                .HasForeignKey(a => a.SurveyResponseId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder
                .HasOne(a => a.SurveyQuestion)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.SurveyQuestionId)
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}
