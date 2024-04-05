var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// WelcomePageMiddleware - компонент middlware-конвейера,
// который отправляет клиенту некоторую стандартную веб-страницу
app.UseWelcomePage();

app.Run();
