using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MoviesRazorPages.Models;

namespace MoviesRazorPages.Pages
{
    public class CreateModel : PageModel
    {
        private readonly MovieDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(MovieDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public IActionResult OnGet()
        {
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
                var uploadedFile = Request.Form.Files[0]; // Get the uploaded file
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

            _context.Movies.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }

}
