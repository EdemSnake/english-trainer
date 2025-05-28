using App.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Read the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Register AppDbContext to use PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// 3. At startup, create a scope and call the initializer
using (var scope = app.Services.CreateScope())
{
    // Get the AppDbContext instance
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Apply pending migrations or create database
    DbInitializer.Initialize(context);
}

// 4. Define a simple HTTP GET endpoint
app.MapGet("/", () => "Hello World!");

// 5. Start the web application
app.Run();
