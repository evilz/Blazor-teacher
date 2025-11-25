namespace BlazorTeacher.Shared.Models;

/// <summary>
/// Represents a single step within a chapter.
/// </summary>
public class ChapterStep
{
    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// The type of step (e.g., Read, Action/Exercise).
    /// </summary>
    public StepType Type { get; set; } = StepType.Read;
}

public enum StepType
{
    Read,
    Action
}
