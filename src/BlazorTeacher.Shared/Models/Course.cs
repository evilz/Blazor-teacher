namespace BlazorTeacher.Shared.Models;

/// <summary>
/// Represents a course in the learning management system.
/// This is our main domain entity for the tutorial.
/// </summary>
public class Course
{
    public int Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string Instructor { get; set; } = string.Empty;
    
    public int DurationInHours { get; set; }
    
    public decimal Price { get; set; }
    
    public CourseLevel Level { get; set; }
    
    public bool IsPublished { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}

/// <summary>
/// Represents the difficulty level of a course.
/// </summary>
public enum CourseLevel
{
    Beginner = 0,
    Intermediate = 1,
    Advanced = 2
}
