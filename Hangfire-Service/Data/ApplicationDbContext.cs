using Hangfire_Service.Models;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Hangfire_Service.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
        {

        }
        public DbSet<CustomerDetails> CustomerDetails { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CustomerOrders> CustomerOrders { get; set; }

    }
}