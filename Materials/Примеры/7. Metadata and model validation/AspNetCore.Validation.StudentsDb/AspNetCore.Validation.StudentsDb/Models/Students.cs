using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Validation.StudentsDb.Models
{
    // Для взаимодействия с MS SQL Server через Entity Framework необходим пакет 
    // Microsoft.EntityFrameworkCore.SqlServer

    public class StudentContext : DbContext
    {
        // Каждое свойство DbSet будет соотноситься с отдельной таблицей в базе данных.
        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }

    public class Student
    {
        // Идентификатор студента
        public int Id { get; set; }

        // Имя студента
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Имя студента")]
        public string? Name { get; set; }

        // Фамилия студента
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Фамилия студента")]
        public string? Surname { get; set; }

        // Возраст студента
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Возраст")]
        [Range(15, 60, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }

        // Средний балл
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Range(0.0, 12.0, ErrorMessage = "Недопустимый средний балл")]
        [Display(Name = "Средний балл")]
        public double GPA { get; set; }

        // Электронный адрес
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Remote(action: "CheckEmail", controller: "Students", ErrorMessage = "Email уже используется")]
        [Display(Name = "Адрес электронной почты")]
        public string? Email { get; set; }

        /*
        [DataType(DataType.Password)]
        public string Password { get; set; }
 
        [Compare("Password",ErrorMessage="Пароли не совпадают")]
        [DataType(DataType.Password)]
        public  string PasswordConfirm { get; set; }
   
        Currency Отображает текст в виде валюты
        DateTime Отображает дату и время
        Date Отображает только дату, без времени
        Time Отображает только время
        Text Отображает однострочный текст
        MultilineText Отображает многострочный текст (элемент textarea)
        Password Отображает символы с использованием маски
        Url  Отображает строку URL
        EmailAddress Отображает электронный адрес
         * */
    }
}
