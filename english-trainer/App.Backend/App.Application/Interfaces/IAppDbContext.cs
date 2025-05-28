using App.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public  interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<TextPassage> TextPassages { get; set; }
        DbSet<AssessmentResult> AssessmentResults { get; set; }
        DbSet<WordScore> WordScores { get; set; }
        DbSet<PhonemeScore> PhonemeScores { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
