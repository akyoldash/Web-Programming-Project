using Microsoft.EntityFrameworkCore;

namespace Web_Programming_Project.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB; database=MovieDatabase; integrated security=true");

            
        }
      

        public DbSet<Movie> Movies { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Admin> Admins { get; set; }
    }
}
