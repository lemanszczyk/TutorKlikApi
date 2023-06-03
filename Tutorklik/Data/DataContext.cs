using Hero_Api;
using Microsoft.EntityFrameworkCore;

namespace Tutorklik.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }
        public DbSet<SuperHero> SuperHeroes => Set<SuperHero>();
    }
}
