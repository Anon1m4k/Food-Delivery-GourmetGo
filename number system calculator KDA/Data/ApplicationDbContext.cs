
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using number_system_calculator_KDA.Model;

namespace number_system_calculator_KDA.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            Database.Migrate();
        }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}