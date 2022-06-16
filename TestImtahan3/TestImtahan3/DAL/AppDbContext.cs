using Microsoft.EntityFrameworkCore;
using TestImtahan3.Models;

namespace TestImtahan3.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
    }
}
