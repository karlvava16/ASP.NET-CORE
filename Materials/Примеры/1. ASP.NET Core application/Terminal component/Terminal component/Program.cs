// CreateBuilder создает новый экземпляр WebApplicationBuilder с предустановленными параметрами.
// Для инициализации объекта WebApplicationBuilder в метод CreateBuilder могут передаваться
// аргументы командной строки, указанные при запуске приложения.
var builder = WebApplication.CreateBuilder(args);

// Метод Build создает экземпляр Web-приложения.
var app = builder.Build();
/* Кроме создания объекта WebApplication класс WebApplicationBuilder выполняет 
   еще ряд задач, среди которых можно выделить следующие:
   - Установка конфигурации приложения
   - Добавление сервисов
   - Настройка логирования в приложении
   - Установка окружения приложения
   - Конфигурация объектов IHostBuilder и IWebHostBuilder, 
     которые применяются для создания хоста приложения.
*/

// Метод Run добавляет терминальный middleware-компонент в конвейер обработки запроса.
// Терминальный middleware-компонент, как известно, завершает обработку запроса.
// Поэтому такой компонент, определенный через метод Run, не вызывает никакие другие
// компоненты и дальше обработку запроса не передает.
app.Run(HandleRequest);

// Run без параметров запускает приложение,
// и веб-сервер начинает прослушивать все входящие HTTP-запросы
app.Run();

async Task HandleRequest(HttpContext context)
{
    /*
     При получении запроса сервер формирует на его основе объект HttpContext, 
    которые содержит всю необходимую информацию о запросе. 
    Эта информация посредством объекта HttpContext передается всем 
    компонентам middleware в приложении.
    */

    // Request: возвращает объект HttpRequest,
    // который хранит информацию о текущем запросе.
    // Query: возвращает коллекцию параметров из строки запроса.

    var name = context.Request.Query["Name"];
    var surname = context.Request.Query["Surname"];
    string responseStr = "<html><head><meta charset='utf8'></head><body><h1>"
        + name + "  " + surname + "</h1>"
        + "<a href='/?Name=Lesya&Surname=Ukrainka'>Poet 1</a><br />"
        + "<a href='/?Name=Taras&Surname=Shevchenko'>Poet 2</a><br />"
        + "<a href='/?Name=Ivan&Surname=Franko'>Poet 3</a><br />"
        + "</body></html>";

    // Response: возвращает объект HttpResponse,
    // который позволяет управлять ответом клиент

    // ContentType: получает или устанавливает заголовок Content-Type
    context.Response.ContentType = "text/html; charset=utf-8";

    // WriteAsync(): отправляет некоторое содержимое клиенту.
    await context.Response.WriteAsync(responseStr);
}
