using System.ComponentModel.DataAnnotations;
using AspNetCore.Validation.StudentsDb.Annotations;

namespace AspNetCore.Validation.StudentsDb.Models
{

    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Название")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Автор")]
        [MyAuthors(["Шилдт", "Троельсен", "Нейгел", "Рихтер", "Страуструп"], ErrorMessage = "Недопустимый автор")]
        public string? Author { get; set; }

        [Required]
        [Display(Name = "Год")]
        public int Year { get; set; }
    }

}