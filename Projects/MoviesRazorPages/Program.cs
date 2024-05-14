using Microsoft.EntityFrameworkCore;
using MoviesRazorPages.Models;

var builder = WebApplication.CreateBuilder(args);
// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// ��������� �������� ApplicationContext � �������� ������� � ����������
builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

app.Run();
