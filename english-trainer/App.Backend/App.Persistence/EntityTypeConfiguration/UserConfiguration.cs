using App.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Persistence.EntityTypeConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USERS");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                  .IsRequired()
                  .HasMaxLength(100);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(u => u.PasswordHash)
                   .IsRequired();

            builder.Property(u => u.CreatedAt)
                   .HasDefaultValueSql("NOW()");

            builder.Property(u => u.UpdatedAt)
                   .IsRequired(false);

            // «1 User : M AssessmentResults»

            builder.HasMany(u => u.AssessmentResults)
                 .WithOne(ar => ar.User)
                 .HasForeignKey(ar => ar.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
