using BlazorTeacher.Api.Data;
using BlazorTeacher.Api.Middleware;
using BlazorTeacher.Api.Validators;
using BlazorTeacher.Shared.DTOs;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

// Configure Serilog for structured logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/api-.log", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    Log.Information("Starting BlazorTeacher API");

    var builder = WebApplication.CreateBuilder(args);

    // Use Serilog for logging
    builder.Host.UseSerilog();

    // Add services to the container
    builder.Services.AddControllers();
    builder.Services.AddOpenApi();

    // Configure SQLite with EF Core
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
            ?? "Data Source=blazorteacher.db"));

    // Register FluentValidation validators
    builder.Services.AddScoped<IValidator<CreateCourseDto>, CreateCourseValidator>();
    builder.Services.AddScoped<IValidator<UpdateCourseDto>, UpdateCourseValidator>();
    builder.Services.AddScoped<IValidator<CreateLessonDto>, CreateLessonValidator>();
    builder.Services.AddScoped<IValidator<UpdateLessonDto>, UpdateLessonValidator>();

    // Configure CORS for Blazor Dashboard
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowBlazorDashboard", policy =>
        {
            policy.WithOrigins("https://localhost:5002", "http://localhost:5003")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

    var app = builder.Build();

    // Apply migrations and seed data
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();
    }

    // Configure the HTTP request pipeline
    app.UseMiddleware<GlobalExceptionMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseCors("AllowBlazorDashboard");
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
