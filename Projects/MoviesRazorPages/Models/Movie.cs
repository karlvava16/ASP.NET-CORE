using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesRazorPages.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title length can't be more than 100.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Producer is required")]
        [StringLength(50, ErrorMessage = "Producer length can't be more than 50.")]
        public string Producer { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Year of issue is required")]
        public DateOnly YearOfIssue { get; set; }

       // [Required(ErrorMessage = "Poster of issue is required")]
        public string? Poster { get; set; }

        [StringLength(500, ErrorMessage = "Short description length can't be more than 500.")]
        public string ShortDescription { get; set; }
    }
}
