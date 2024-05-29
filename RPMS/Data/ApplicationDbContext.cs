using Microsoft.EntityFrameworkCore;
using RPMS.Models;

namespace RPMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        DbSet<Resident> Residents { get; set; }

        DbSet<Address> Addresses { get; set; }

    }
}
