using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using MoviesRazorPages.Models;
using MoviesRazorPages.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MoviesRazorPages.Pages
{
    public class EditModel : PageModel
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IWebHostEnvironment _environment;

        public EditModel(IMovieRepository movieRepository, IWebHostEnvironment environment)
        {
            _movieRepository = movieRepository;
            _environment = environment;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = _movieRepository.GetMovieById(id.Value);

            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Request.Form.Files.Count > 0)
            {
                var uploadedFile = Request.Form.Files[0];
                if (uploadedFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "Posters");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(uploadedFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }

                    Movie.Poster = "/Posters/" + uniqueFileName;
                }
            }

            // Check if the poster is still empty
            if (string.IsNullOrEmpty(Movie.Poster))
            {
                ModelState.AddModelError("Movie.Poster", "Poster is required.");
                return Page();
            }

            try
            {
                _movieRepository.UpdateMovie(Movie);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_movieRepository.GetMovieById(Movie.Id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
