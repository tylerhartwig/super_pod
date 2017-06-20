using FindYourPod.Models;
using Microsoft.EntityFrameworkCore;

namespace PodContext.Data
{
    public class PodContext : DbContext
    {
        public PodContext(DbContextOptions<PodContext> options) : base(options)
        {
        }
        public DbSet<Fin> Fins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fin>().ToTable("Fin");
        }
    }
}