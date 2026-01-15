using DoctorAppointment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string dbPath = Path.Combine(
    Directory.GetCurrentDirectory(),
    "..",
    "medical.db");

builder.Services.AddDbContext<MedicalDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Use Controllers + Views for MVC
builder.Services.AddControllersWithViews();   // <-- replace AddControllers()

// Swagger (for API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

// Default route for MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map API controllers too
app.MapControllers();

app.Run();
