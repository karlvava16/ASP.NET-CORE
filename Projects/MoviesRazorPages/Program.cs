using MoviesRazorPages.Models;
using MoviesRazorPages.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Get the connection string from configuration
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Register repositories
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IProducerRepository, ProducerRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();

// Add DbContext services
builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddRazorPages();

// Add distributed memory cache and session management
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.Use(async (context, next) =>
{
    var path = context.Request.Path.ToString().ToLower();
    if (!context.Session.TryGetValue("IsLoggedIn", out _) && !path.Contains("/account/login") && !path.Contains("/account/registration"))
    {
        context.Response.Redirect("/Account/Login");
        return;
    }
    await next();
});

app.UseAuthorization();

app.MapRazorPages();

app.Run();
