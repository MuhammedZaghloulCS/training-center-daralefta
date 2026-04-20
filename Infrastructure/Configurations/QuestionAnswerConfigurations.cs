using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class QuestionAnswerConfigurations : IEntityTypeConfiguration<QuestionAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionAnswer> builder)
        {


            builder.HasKey(a => a.Id);

            builder.HasIndex(a => new { a.SurveyResponseId, a.questionId })
                   .IsUnique();

            builder
                .HasOne(a => a.SurveyResponse)
                .WithMany(r => r.Answers)
                .HasForeignKey(a => a.SurveyResponseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(a => a.Question)
                .WithMany()
                .HasForeignKey(a => a.questionId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
