using Microsoft.EntityFrameworkCore;
using MoviesRazorPages.Models;

namespace MoviesRazorPages.Repositories
{
    public interface IMovieRepository
    {
        IQueryable<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        void AddMovie(Movie movie);
        void UpdateMovie(Movie movie);
        void DeleteMovie(int id);
    }

    public interface IGenreRepository
    {
        IQueryable<Genre> GetAllGenres();
        Genre GetGenreById(int id);
        void AddGenre(Genre genre);
        void UpdateGenre(Genre genre);
        void DeleteGenre(int id);
    }

    public interface IProducerRepository
    {
        IQueryable<Producer> GetAllProducers();
        Producer GetProducerById(int id);
        void AddProducer(Producer producer);
        void UpdateProducer(Producer producer);
        void DeleteProducer(int id);
    }
}

namespace MoviesRazorPages.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _context;

        public MovieRepository(MovieDbContext context)
        {
            _context = context;
        }

        public IQueryable<Movie> GetAllMovies()
        {
            return _context.Movies;
        }

        public Movie GetMovieById(int id)
        {
            return _context.Movies.Find(id);
        }

        public void AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public void UpdateMovie(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            Console.WriteLine(_context.Entry(movie).State);
            _context.SaveChanges();
        }

        public void DeleteMovie(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }
        }
    }

    public class GenreRepository : IGenreRepository
    {
        private readonly MovieDbContext _context;

        public GenreRepository(MovieDbContext context)
        {
            _context = context;
        }

        public IQueryable<Genre> GetAllGenres()
        {
            return _context.Genres;
        }

        public Genre GetGenreById(int id)
        {
            return _context.Genres.Find(id);
        }

        public void AddGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        public void UpdateGenre(Genre genre)
        {
            _context.Entry(genre).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteGenre(int id)
        {
            var genre = _context.Genres.Find(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                _context.SaveChanges();
            }
        }
    }

    public class ProducerRepository : IProducerRepository
    {
        private readonly MovieDbContext _context;

        public ProducerRepository(MovieDbContext context)
        {
            _context = context;
        }

        public IQueryable<Producer> GetAllProducers()
        {
            return _context.Producers;
        }

        public Producer GetProducerById(int id)
        {
            return _context.Producers.Find(id);
        }

        public void AddProducer(Producer producer)
        {
            _context.Producers.Add(producer);
            _context.SaveChanges();
        }

        public void UpdateProducer(Producer producer)
        {
            _context.Entry(producer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteProducer(int id)
        {
            var producer = _context.Producers.Find(id);
            if (producer != null)
            {
                _context.Producers.Remove(producer);
                _context.SaveChanges();
            }
        }
    }
}
