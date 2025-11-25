namespace BlazorTeacher.Shared.Models;

/// <summary>
/// Represents a chapter in the tutorial.
/// </summary>
public class Chapter
{
    public int Id { get; set; }
    
    public int Number { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string? Route { get; set; }
    
    public ChapterCategory Category { get; set; }
    
    public List<string> Topics { get; set; } = new();
    
    public List<string> KeyPoints { get; set; } = new();

    public List<ChapterStep> Steps { get; set; } = new();

    public Quiz? Quiz { get; set; }
}

/// <summary>
/// Represents the category/section of a chapter.
/// </summary>
public enum ChapterCategory
{
    Introduction = 0,
    Setup = 1,
    ApiDevelopment = 2,
    BlazorBasics = 3,
    Components = 4,
    StateManagement = 5,
    Dashboard = 6,
    Advanced = 7,
    WrapUp = 8
}

/// <summary>
/// Represents the learning progress state for a chapter.
/// </summary>
public enum LearningState
{
    NotStarted = 0,
    InProgress = 1,
    Completed = 2
}

/// <summary>
/// Tracks learning progress for a specific chapter.
/// </summary>
public class ChapterProgress
{
    public int ChapterId { get; set; }
    
    public LearningState State { get; set; } = LearningState.NotStarted;
    
    public DateTime? StartedAt { get; set; }
    
    public DateTime? CompletedAt { get; set; }
    
    public int ProgressPercentage { get; set; }

    public int CurrentStepIndex { get; set; }
}
