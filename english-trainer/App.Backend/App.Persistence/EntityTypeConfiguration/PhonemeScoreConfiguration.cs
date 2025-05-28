using App.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Persistence.EntityTypeConfiguration
{
    public class PhonemeScoreConfiguration : IEntityTypeConfiguration<PhonemeScore>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PhonemeScore> builder)
        {
            builder.ToTable("PHONEME_SCORES");
            builder.HasKey(ps => ps.Id);
           
            builder.HasOne(ps => ps.WordScore)
                   .WithMany(ws => ws.PhonemeScores)
                   .HasForeignKey(ps => ps.WordScoreId);

            builder.Property(ps => ps.Phoneme)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(ps => ps.AccuracyScore).IsRequired();
        }
    }
  
}
