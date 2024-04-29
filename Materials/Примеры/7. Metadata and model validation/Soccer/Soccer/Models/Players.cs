using System.ComponentModel.DataAnnotations;
namespace Soccer.Models
{   
    public class Players
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Position { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int TeamId { get; set; }
        public Teams? Team { get; set; }
    }
}
