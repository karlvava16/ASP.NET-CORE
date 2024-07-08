using System.ComponentModel.DataAnnotations;

namespace MoviesRazorPages.Models
{
    public class RegistrModel
    {
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string? PasswordConfirm { get; set; }
    }
}
