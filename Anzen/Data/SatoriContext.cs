using Anzen.Models;
using Microsoft.EntityFrameworkCore;

namespace Anzen.Data
{
    public class SatoriContext : DbContext
    {
        public SatoriContext(DbContextOptions<SatoriContext> options)
            : base(options)
        {
        }

        public DbSet<Submission> Submission { get; set; } = null!;
        public DbSet<Status> Status { get; set; } = null!;
        public DbSet<Coverage> Coverage { get; set; } = null!;
        public DbSet<CoverageSubmission> Submission_Coverage { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=satori.db");//Security and scalability outside scope 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Status)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Submission>()
                .HasMany(s => s.Coverages)
                .WithMany(c => c.Submissions);

            modelBuilder.Entity<CoverageSubmission>()
                .HasKey(sc => new { sc.SubmissionId, sc.CoverageId });
        }
    }
}
