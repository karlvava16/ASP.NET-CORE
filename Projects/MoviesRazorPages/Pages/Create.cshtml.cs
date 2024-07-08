using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using MoviesRazorPages.Models;
using MoviesRazorPages.Repositories;

namespace MoviesRazorPages.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(IMovieRepository movieRepository, IWebHostEnvironment environment)
        {
            _movieRepository = movieRepository;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; }

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

            _movieRepository.AddMovie(Movie);

            return RedirectToPage("./Index");
        }
    }
}
