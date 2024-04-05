var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) => await context.Response.SendFileAsync("Capri.jpg"));

app.Run();
