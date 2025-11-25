namespace BlazorTeacher.Shared.Models;

/// <summary>
/// Represents a quiz at the end of a chapter.
/// </summary>
public class Quiz
{
    public List<QuizQuestion> Questions { get; set; } = new();
}

/// <summary>
/// Represents a single question in a quiz.
/// </summary>
public class QuizQuestion
{
    public string Text { get; set; } = string.Empty;
    
    public List<string> Options { get; set; } = new();
    
    /// <summary>
    /// The index of the correct option (0-based).
    /// </summary>
    public int CorrectOptionIndex { get; set; }
    
    public string Explanation { get; set; } = string.Empty;
}
