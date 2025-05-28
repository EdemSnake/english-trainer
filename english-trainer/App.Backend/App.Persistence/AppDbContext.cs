using App.Application.Interfaces;
using App.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Persistence
{
    public class AppDbContext :  DbContext, IAppDbContext
    {
      
        public DbSet<User> Users { get; set; }
        public DbSet<TextPassage> TextPassages { get; set; }
        public DbSet<AssessmentResult> AssessmentResults { get; set; }
        public DbSet<WordScore> WordScores { get; set; }
        public DbSet<PhonemeScore> PhonemeScores { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
   
}
