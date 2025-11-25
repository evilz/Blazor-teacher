using BlazorTeacher.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorTeacher.Api.Data;

/// <summary>
/// Entity Framework Core DbContext for the application.
/// Configures SQLite database and entity relationships.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Lesson> Lessons => Set<Lesson>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Course entity
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(c => c.Id);
            
            entity.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(c => c.Description)
                .HasMaxLength(2000);
            
            entity.Property(c => c.Instructor)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(c => c.Price)
                .HasPrecision(18, 2);
            
            // One-to-many relationship with Lesson
            entity.HasMany(c => c.Lessons)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Lesson entity
        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(l => l.Id);
            
            entity.Property(l => l.Title)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(l => l.Content)
                .HasMaxLength(10000);
            
            entity.Property(l => l.VideoUrl)
                .HasMaxLength(500);
        });

        // Seed initial data for the tutorial
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = 1,
                Title = "Introduction to C#",
                Description = "Learn the fundamentals of C# programming language, including syntax, data types, and object-oriented programming concepts.",
                Instructor = "John Smith",
                DurationInHours = 10,
                Price = 49.99m,
                Level = CourseLevel.Beginner,
                IsPublished = true,
                CreatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
            },
            new Course
            {
                Id = 2,
                Title = "ASP.NET Core Web API",
                Description = "Build modern RESTful APIs with ASP.NET Core, Entity Framework Core, and best practices for API design.",
                Instructor = "Jane Doe",
                DurationInHours = 15,
                Price = 79.99m,
                Level = CourseLevel.Intermediate,
                IsPublished = true,
                CreatedAt = new DateTime(2024, 2, 20, 14, 30, 0, DateTimeKind.Utc)
            },
            new Course
            {
                Id = 3,
                Title = "Blazor Web Applications",
                Description = "Create interactive web applications using Blazor, including components, state management, and integration with APIs.",
                Instructor = "Bob Johnson",
                DurationInHours = 12,
                Price = 69.99m,
                Level = CourseLevel.Intermediate,
                IsPublished = true,
                CreatedAt = new DateTime(2024, 3, 10, 9, 15, 0, DateTimeKind.Utc)
            },
            new Course
            {
                Id = 4,
                Title = "Advanced .NET Patterns",
                Description = "Master advanced design patterns and architectural concepts in .NET applications.",
                Instructor = "Alice Williams",
                DurationInHours = 20,
                Price = 99.99m,
                Level = CourseLevel.Advanced,
                IsPublished = false,
                CreatedAt = new DateTime(2024, 4, 5, 11, 45, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Lesson>().HasData(
            new Lesson
            {
                Id = 1,
                Title = "Getting Started with C#",
                Content = "Introduction to the C# programming language and .NET platform.",
                Order = 1,
                DurationInMinutes = 30,
                CourseId = 1,
                CreatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
            },
            new Lesson
            {
                Id = 2,
                Title = "Variables and Data Types",
                Content = "Learn about different data types and how to declare variables in C#.",
                Order = 2,
                DurationInMinutes = 45,
                CourseId = 1,
                CreatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
            },
            new Lesson
            {
                Id = 3,
                Title = "Control Flow Statements",
                Content = "Understanding if-else, switch, loops, and other control flow mechanisms.",
                Order = 3,
                DurationInMinutes = 40,
                CourseId = 1,
                CreatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
            },
            new Lesson
            {
                Id = 4,
                Title = "Building Your First API",
                Content = "Create a minimal API with ASP.NET Core and understand the request pipeline.",
                Order = 1,
                DurationInMinutes = 60,
                CourseId = 2,
                CreatedAt = new DateTime(2024, 2, 20, 14, 30, 0, DateTimeKind.Utc)
            },
            new Lesson
            {
                Id = 5,
                Title = "Entity Framework Core Basics",
                Content = "Learn how to use EF Core for data access and database operations.",
                Order = 2,
                DurationInMinutes = 75,
                CourseId = 2,
                CreatedAt = new DateTime(2024, 2, 20, 14, 30, 0, DateTimeKind.Utc)
            },
            new Lesson
            {
                Id = 6,
                Title = "Introduction to Blazor",
                Content = "Understanding Blazor's component model and rendering modes.",
                Order = 1,
                DurationInMinutes = 45,
                CourseId = 3,
                CreatedAt = new DateTime(2024, 3, 10, 9, 15, 0, DateTimeKind.Utc)
            },
            new Lesson
            {
                Id = 7,
                Title = "Building Blazor Components",
                Content = "Create reusable components with parameters and event callbacks.",
                Order = 2,
                DurationInMinutes = 55,
                CourseId = 3,
                CreatedAt = new DateTime(2024, 3, 10, 9, 15, 0, DateTimeKind.Utc)
            }
        );
    }
}
