var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    context.Response.Headers.ContentDisposition = "attachment; filename=Capri Island.jpg";
    await context.Response.SendFileAsync("Capri.jpg");
});

app.Run();
