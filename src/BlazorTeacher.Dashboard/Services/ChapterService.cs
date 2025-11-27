using System.Collections.Concurrent;
using BlazorTeacher.Shared.Models;
using Microsoft.Extensions.Logging;

namespace BlazorTeacher.Dashboard.Services;

/// <summary>
/// Service for managing tutorial chapters and learning progress.
/// Loads chapter content from markdown files in wwwroot/content/chapters/.
/// </summary>
public class ChapterService
{
    private readonly ConcurrentDictionary<int, ChapterProgress> _progressCache = new();
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<ChapterService> _logger;
    private List<Chapter>? _chaptersCache;
    private readonly object _cacheLock = new();
    
    public event Action? OnProgressChanged;

    public ChapterService(IWebHostEnvironment environment, ILogger<ChapterService> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    /// <summary>
    /// Gets all tutorial chapters loaded from markdown files.
    /// </summary>
    public List<Chapter> GetAllChapters()
    {
        if (_chaptersCache != null)
        {
            return _chaptersCache;
        }

        lock (_cacheLock)
        {
            if (_chaptersCache != null)
            {
                return _chaptersCache;
            }

            _chaptersCache = LoadChaptersFromMarkdown();
            return _chaptersCache;
        }
    }

    /// <summary>
    /// Reloads chapters from markdown files (useful for development).
    /// </summary>
    public void ReloadChapters()
    {
        lock (_cacheLock)
        {
            _chaptersCache = null;
        }
    }

    private List<Chapter> LoadChaptersFromMarkdown()
    {
        var chapters = new List<Chapter>();
        var chaptersPath = Path.Combine(_environment.WebRootPath, "content", "chapters");

        if (!Directory.Exists(chaptersPath))
        {
            return chapters;
        }

        var markdownFiles = Directory.GetFiles(chaptersPath, "*.md")
            .OrderBy(f => f)
            .ToList();

        foreach (var filePath in markdownFiles)
        {
            try
            {
                var content = File.ReadAllText(filePath);
                var chapter = MarkdownChapterParser.Parse(content);
                chapters.Add(chapter);
            }
            catch (Exception ex)
            {
                // Log error but continue loading other chapters
                _logger.LogError(ex, "Error loading chapter from {FilePath}", filePath);
            }
        }

        return chapters.OrderBy(c => c.Number).ToList();
    }

    /// <summary>
    /// Gets chapters grouped by category.
    /// </summary>
    public Dictionary<ChapterCategory, List<Chapter>> GetChaptersByCategory()
    {
        return GetAllChapters()
            .GroupBy(c => c.Category)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    /// <summary>
    /// Gets the learning progress for a chapter.
    /// </summary>
    public ChapterProgress GetProgress(int chapterId)
    {
        return _progressCache.GetOrAdd(chapterId, id => new ChapterProgress 
        { 
            ChapterId = id, 
            State = LearningState.NotStarted 
        });
    }

    /// <summary>
    /// Gets all chapter progress.
    /// </summary>
    public List<ChapterProgress> GetAllProgress()
    {
        var chapters = GetAllChapters();
        return chapters.Select(c => GetProgress(c.Id)).ToList();
    }

    /// <summary>
    /// Gets overall learning progress percentage.
    /// </summary>
    public int GetOverallProgressPercentage()
    {
        var chapters = GetAllChapters();
        if (chapters.Count == 0) return 0;
        
        var completed = chapters.Count(c => GetProgress(c.Id).State == LearningState.Completed);
        return (int)((completed / (double)chapters.Count) * 100);
    }

    /// <summary>
    /// Gets the count of completed chapters.
    /// </summary>
    public int GetCompletedCount()
    {
        return GetAllChapters().Count(c => GetProgress(c.Id).State == LearningState.Completed);
    }

    /// <summary>
    /// Marks a chapter as started.
    /// </summary>
    public void StartChapter(int chapterId)
    {
        var progress = GetProgress(chapterId);
        if (progress.State == LearningState.NotStarted)
        {
            progress.State = LearningState.InProgress;
            progress.StartedAt = DateTime.UtcNow;
            progress.ProgressPercentage = 0;
            progress.CurrentStepIndex = 0;
            OnProgressChanged?.Invoke();
        }
    }

    /// <summary>
    /// Advances to the next step in a chapter.
    /// </summary>
    public void CompleteStep(int chapterId, int stepIndex)
    {
        var progress = GetProgress(chapterId);
        var chapter = GetAllChapters().FirstOrDefault(c => c.Id == chapterId);
        
        if (chapter == null) return;
        
        // If this is the last step (or beyond), we don't just "complete" it here, 
        // the UI should trigger the Quiz or CompleteChapter. 
        // But we can track the index.
        
        if (stepIndex >= progress.CurrentStepIndex)
        {
            progress.CurrentStepIndex = stepIndex + 1;
            
            // Calculate progress based on steps
            // If there are steps, mapped 0-90%. Quiz/Completion is the rest.
            if (chapter.Steps.Any())
            {
                int totalSteps = chapter.Steps.Count;
                double stepWeight = 90.0 / totalSteps;
                int newPercentage = (int)(progress.CurrentStepIndex * stepWeight);
                progress.ProgressPercentage = Math.Clamp(newPercentage, 0, 90);
            }
            
            OnProgressChanged?.Invoke();
        }
    }

    /// <summary>
    /// Updates the progress percentage for a chapter.
    /// </summary>
    public void UpdateProgress(int chapterId, int percentage)
    {
        var progress = GetProgress(chapterId);
        if (progress.State == LearningState.NotStarted)
        {
            StartChapter(chapterId);
        }
        
        progress.ProgressPercentage = Math.Clamp(percentage, 0, 100);
        
        if (percentage >= 100 && progress.State != LearningState.Completed)
        {
            progress.State = LearningState.Completed;
            progress.CompletedAt = DateTime.UtcNow;
        }
        
        OnProgressChanged?.Invoke();
    }

    /// <summary>
    /// Marks a chapter as completed.
    /// </summary>
    public void CompleteChapter(int chapterId)
    {
        var progress = GetProgress(chapterId);
        progress.State = LearningState.Completed;
        progress.CompletedAt = DateTime.UtcNow;
        progress.ProgressPercentage = 100;
        progress.StartedAt ??= DateTime.UtcNow;
        
        OnProgressChanged?.Invoke();
    }

    /// <summary>
    /// Resets progress for a chapter.
    /// </summary>
    public void ResetChapter(int chapterId)
    {
        if (_progressCache.TryRemove(chapterId, out _))
        {
            OnProgressChanged?.Invoke();
        }
    }

    /// <summary>
    /// Resets all chapter progress.
    /// </summary>
    public void ResetAllProgress()
    {
        _progressCache.Clear();
        OnProgressChanged?.Invoke();
    }

    /// <summary>
    /// Gets the display name for a chapter category.
    /// </summary>
    public static string GetCategoryDisplayName(ChapterCategory category)
    {
        return category switch
        {
            ChapterCategory.Introduction => "📖 Introduction",
            ChapterCategory.Setup => "🛠️ Environment Setup",
            ChapterCategory.ApiDevelopment => "🚀 API Development",
            ChapterCategory.BlazorBasics => "⚡ Blazor Basics",
            ChapterCategory.Components => "🧩 Components",
            ChapterCategory.StateManagement => "🔄 State Management",
            ChapterCategory.Dashboard => "📊 Dashboard",
            ChapterCategory.Advanced => "🎯 Advanced Topics",
            ChapterCategory.WrapUp => "🎉 Wrap-Up",
            _ => category.ToString()
        };
    }

    /// <summary>
    /// Gets the icon for a learning state.
    /// </summary>
    public static string GetStateIcon(LearningState state)
    {
        return state switch
        {
            LearningState.NotStarted => "○",
            LearningState.InProgress => "◐",
            LearningState.Completed => "✓",
            _ => "○"
        };
    }

    /// <summary>
    /// Gets the CSS class for a learning state.
    /// </summary>
    public static string GetStateClass(LearningState state)
    {
        return state switch
        {
            LearningState.NotStarted => "not-started",
            LearningState.InProgress => "in-progress",
            LearningState.Completed => "completed",
            _ => "not-started"
        };
    }
}
