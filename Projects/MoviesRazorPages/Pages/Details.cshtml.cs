using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoviesRazorPages.Models;
using MoviesRazorPages.Repositories;

namespace MoviesRazorPages.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IMovieRepository _movieRepository;

        public DetailsModel(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _movieRepository.GetMovieById(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                Movie = movie;
            }
            return Page();
        }
    }
}
