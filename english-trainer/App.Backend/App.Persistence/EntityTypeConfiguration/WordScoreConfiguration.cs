using App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Persistence.EntityTypeConfiguration
{
    public class WordScoreConfiguration : IEntityTypeConfiguration<WordScore>
    {
        public void Configure(EntityTypeBuilder<WordScore> builder)
        {
            builder.ToTable("WORD_SCORES");
            builder.HasKey(ws => ws.Id);

            builder.HasOne(ws => ws.AssessmentResult)
                .WithMany(ar => ar.WordScores)
                .HasForeignKey(ws => ws.AssessmentResultId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(ws => ws.Word)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(ws => ws.AccuracyScore).IsRequired();
            builder.Property(ws => ws.ErrorType)
                .HasMaxLength(100);
            builder.Property(ws => ws.Offset).IsRequired();
            builder.Property(ws => ws.Duration).IsRequired();
            builder.HasMany(ws => ws.PhonemeScores)
                .WithOne(ps => ps.WordScore)
                .HasForeignKey(ps => ps.WordScoreId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
   
}
