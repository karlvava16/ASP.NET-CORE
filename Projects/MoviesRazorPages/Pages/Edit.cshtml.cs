using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesRazorPages.Models;

namespace MoviesRazorPages.Pages
{
    public class EditModel : PageModel
    {
        private readonly MovieDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(MovieDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
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

            Movie = await _context.Movies.FindAsync(id);

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

            _context.Attach(Movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.Id))
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

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }

}
