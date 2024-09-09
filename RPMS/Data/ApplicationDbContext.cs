using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RPMS.Models;

namespace RPMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        public DbSet<Resident> Residents { get; set; }
  
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Street> Streets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Resident>().HasOne(r => r.Street).WithMany(s => s.Residents).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Street>().HasOne(s => s.Address).WithMany(s => s.Streets).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Address>().HasMany(a => a.Streets).WithOne(s => s.Address).OnDelete(DeleteBehavior.Restrict);

            
        }

    }
}
