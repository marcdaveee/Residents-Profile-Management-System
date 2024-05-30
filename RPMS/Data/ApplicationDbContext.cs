using Microsoft.EntityFrameworkCore;
using RPMS.Models;

namespace RPMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        public DbSet<Resident> Residents { get; set; }

        public DbSet<Address> Addresses { get; set; }

    }
}
