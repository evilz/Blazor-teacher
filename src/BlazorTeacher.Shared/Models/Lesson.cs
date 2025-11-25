namespace BlazorTeacher.Shared.Models;

/// <summary>
/// Represents a lesson within a course.
/// Demonstrates a one-to-many relationship with Course.
/// </summary>
public class Lesson
{
    public int Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;
    
    public int Order { get; set; }
    
    public int DurationInMinutes { get; set; }
    
    public string? VideoUrl { get; set; }
    
    public int CourseId { get; set; }
    
    public Course Course { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
