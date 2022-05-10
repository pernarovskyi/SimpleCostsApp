using Microsoft.EntityFrameworkCore;
using SimpleCostsApplication.Models;

namespace SimpleCostsApplication.Context
{
    public class CostsContext : DbContext
    {
      public DbSet<Costs> Costs { get; set; }

        public CostsContext(DbContextOptions<CostsContext> options) : base(options) {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Costs>().ToTable("Costs");            
        }
    }

 
}
