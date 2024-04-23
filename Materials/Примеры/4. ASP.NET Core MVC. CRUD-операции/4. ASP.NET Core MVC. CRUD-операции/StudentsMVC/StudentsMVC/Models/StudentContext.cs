using Microsoft.EntityFrameworkCore;

namespace StudentsMVC.Models
{
    // Чтобы подключиться к базе данных через Entity Framework, необходим контекст данных. 
    // Контекст данных представляет собой класс, производный от класса DbContext.
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public StudentContext(DbContextOptions<StudentContext> options)
           : base(options)
        {
            if (Database.EnsureCreated())
            {
                Students?.Add(new Student { Name = "Иван", Surname = "Иванов", Age = 20, GPA = 10.5 });
                Students?.Add(new Student { Name = "Сергей", Surname = "Сергеев", Age = 23, GPA = 11.5 });
                Students?.Add(new Student { Name = "Петр", Surname = "Петров", Age = 25, GPA = 12 });
                SaveChanges();
            }
        }
    }
}