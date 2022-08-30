using CustomerAutomation.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerAutomation.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Customer> Customers { get; set; }

    }
}
