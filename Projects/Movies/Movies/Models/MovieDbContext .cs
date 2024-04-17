using Microsoft.EntityFrameworkCore;

namespace Movies.Models
{

    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Producer> Producers { get; set; }

        public MovieDbContext(DbContextOptions<MovieDbContext> options)
           : base(options)
        {
            if (Database.EnsureCreated())
            {
               
                SaveChanges();
            }
        }
    }

}
