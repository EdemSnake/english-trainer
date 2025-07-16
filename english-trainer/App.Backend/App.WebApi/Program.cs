using App.Application;
using App.Application.Interfaces;
using App.Persistence;
using App.WebApi.Middleware;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using App.WebApi.Consumers;
using App.WebApi.Hubs;

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

// Add SignalR
builder.Services.AddSignalR();

// Add MassTransit with RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TtsResultConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("tts_results", e =>
        {
            e.ConfigureConsumer<TtsResultConsumer>(context);
        });
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.WithOrigins("http://localhost:5173") // Allow the frontend origin
            .AllowCredentials(); // Allow credentials for SignalR
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

// Enable static files
app.UseStaticFiles();

// 4. Define a simple HTTP GET endpoint
app.MapControllers();
app.MapHub<TtsHub>("/ttsHub"); // Map the SignalR hub

// 5. Start the web application
app.Run();
