using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace MoviesRazorPages.Models
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<User> Users { get; set; }


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
            // Add movies
            Movies.Add(new Movie
            {
                Title = "The Dark Knight",
                Producer = "Christopher Nolan",
                Genre = "Action",
                YearOfIssue = new DateOnly(2008, 01, 23),
                Poster = "/Posters/dark_knight_poster.jpg",
                ShortDescription = "A superhero film directed by Christopher Nolan."
            });

            Movies.Add(new Movie
            {
                Title = "Pulp Fiction",
                Producer = "Quentin Tarantino",
                Genre = "Drama",
                YearOfIssue = new DateOnly(1994, 01, 23),
                Poster = "/Posters/pulp_fiction_poster.jpg",
                ShortDescription = "A crime film directed by Quentin Tarantino."
            });

            SaveChanges();
        }
    }
}
