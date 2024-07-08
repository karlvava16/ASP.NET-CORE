using System.ComponentModel.DataAnnotations;

namespace MoviesRazorPages.Models
{
    public class RegistrModel
    {
        [Required(ErrorMessage = "Field is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string? Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Field is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string? PasswordConfirm { get; set; }
    }
}
