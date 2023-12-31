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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=satori.db");//Security and scalability outside scope 
        }
    }
}
