using Microsoft.EntityFrameworkCore;
using AspNetCore.Validation.StudentsDb.Models;

var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<StudentContext>(options => options.UseSqlServer(connection));

// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles(); // обрабатывает запросы к файлам в папке wwwroot

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=Create}/{id?}");

app.Run();