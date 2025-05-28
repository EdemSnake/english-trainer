using App.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Persistence.EntityTypeConfiguration
{
    public class AssessmentResultConfiguration : IEntityTypeConfiguration<AssessmentResult>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AssessmentResult> builder)
        {
            builder.ToTable("ASSESSMENT_RESULTS");
            builder.HasKey(ar => ar.Id);

            builder.HasOne(ar => ar.User)
                   .WithMany(u => u.AssessmentResults)
                   .HasForeignKey(ar => ar.UserId);

            builder.HasOne(ar => ar.TextPassage)
                   .WithMany(tp => tp.AssessmentResults)
                   .HasForeignKey(ar => ar.TextPassageId);


            builder.Property(ar => ar.AccuracyScore).IsRequired();
            builder.Property(ar => ar.FluencyScore).IsRequired();
            builder.Property(ar => ar.ProsodyScore).IsRequired();
            builder.Property(ar => ar.PronunciationScore).IsRequired();

            builder.Property(ar => ar.UserAudioUrl).HasMaxLength(500);
            builder.Property(ar => ar.FullResultJson).HasColumnType("jsonb");
            builder.Property(ar => ar.AssessedAt)
                   .IsRequired()
                   .HasDefaultValueSql("NOW()");

            builder.HasMany(ar => ar.WordScores)
                   .WithOne(ws => ws.AssessmentResult)
                   .HasForeignKey(ws => ws.AssessmentResultId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
        
    
}
