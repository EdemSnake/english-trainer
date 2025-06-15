using App.Application;
using App.Application.Interfaces;
using App.Persistence;
using App.WebApi.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Read the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Register AppDbContext to use PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IAppDbContext, AppDbContext>();

builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

var app = builder.Build();

// 3. At startup, create a scope and call the initializer
using (var scope = app.Services.CreateScope())
{
    // Get the AppDbContext instance
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Apply pending migrations or create database
    DbInitializer.Initialize(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.UseCors("AllowAll");

// 4. Define a simple HTTP GET endpoint
app.MapControllers();

// 5. Start the web application
app.Run();
