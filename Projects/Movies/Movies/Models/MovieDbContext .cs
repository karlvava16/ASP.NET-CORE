using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
            // Ensure the database is created
            if (Database.EnsureCreated())
            {
                // Add sample data if the database was just created
                AddSampleData();
            }
        }

        private void AddSampleData()
        {
            // Add genres
            Genres.Add(new Genre { Name = "Action" });
            Genres.Add(new Genre { Name = "Drama" });
            Genres.Add(new Genre { Name = "Comedy" });
            SaveChanges();

            // Add producers
            Producers.Add(new Producer { Name = "Steven Spielberg" });
            Producers.Add(new Producer { Name = "Christopher Nolan" });
            Producers.Add(new Producer { Name = "Quentin Tarantino" });
            SaveChanges();

            // Add movies
            Movies.Add(new Movie
            {
                Title = "The Dark Knight",
                Producer = Producers.First(p => p.Name == "Christopher Nolan"),
                Genre = Genres.First(g => g.Name == "Action"),
                YearOfIssue = 2008,
                Poster = "dark_knight_poster.jpg",
                ShortDescription = "A superhero film directed by Christopher Nolan."
            });

            Movies.Add(new Movie
            {
                Title = "Pulp Fiction",
                Producer = Producers.First(p => p.Name == "Quentin Tarantino"),
                Genre = Genres.First(g => g.Name == "Drama"),
                YearOfIssue = 1994,
                Poster = "pulp_fiction_poster.jpg",
                ShortDescription = "A crime film directed by Quentin Tarantino."
            });

            SaveChanges();
        }
    }
}
