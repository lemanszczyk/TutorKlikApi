using Microsoft.EntityFrameworkCore;
using Tutorklik.Models;

namespace Tutorklik.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }
        public DbSet<SuperHero> SuperHeroes => Set<SuperHero>();
        public DbSet<Annoucement> Annoucements  => Set<Annoucement>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<User> Users => Set<User>();

    }
}
