using App.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Persistence.EntityTypeConfiguration
{
    public class TextPassageConfiguration : IEntityTypeConfiguration<TextPassage>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TextPassage> builder)
        {
            builder.ToTable("TEXT_PASSAGES");
            builder.HasKey(tp => tp.Id);

            builder.Property(tp => tp.Content)
                   .IsRequired()
                   .HasColumnType("text");

            builder.Property(tp => tp.DifficultyLevel)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(tp => tp.NativeSpeakerAudioUrl)
                   .HasMaxLength(500);

            builder.Property(tp => tp.CreatedAt)
                   .HasDefaultValueSql("NOW()");

            builder.HasMany(tp => tp.AssessmentResults)
                   .WithOne(ar => ar.TextPassage)
                   .HasForeignKey(ar => ar.TextPassageId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
    
}
