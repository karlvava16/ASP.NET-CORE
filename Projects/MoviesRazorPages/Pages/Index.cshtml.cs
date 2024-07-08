using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoviesRazorPages.Models;
using MoviesRazorPages.Repositories;

namespace MoviesRazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMovieRepository _context;

        public IndexModel(IMovieRepository context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _context.GetAllMovies().ToListAsync();
        }
    }
}
