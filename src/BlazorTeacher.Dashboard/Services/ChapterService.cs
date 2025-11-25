using System.Collections.Concurrent;
using BlazorTeacher.Shared.Models;

namespace BlazorTeacher.Dashboard.Services;

/// <summary>
/// Service for managing tutorial chapters and learning progress.
/// </summary>
public class ChapterService
{
    private readonly ConcurrentDictionary<int, ChapterProgress> _progressCache = new();
    
    public event Action? OnProgressChanged;

    /// <summary>
    /// Gets all tutorial chapters.
    /// </summary>
    public List<Chapter> GetAllChapters()
    {
        return new List<Chapter>
        {
            // Introduction
            new Chapter
            {
                Id = 1,
                Number = 1,
                Title = "Introduction",
                Description = "This chapter sets the stage: what we will build, the tech stack, the workflow, and the final outcome. You'll understand how the API, EF Core, and Blazor interact to form a complete full-stack .NET 10 application.",
                Category = ChapterCategory.Introduction,
                KeyPoints = new List<string>
                {
                    "What .NET 10 brings",
                    "Architecture overview (API → Data → Blazor)",
                    "Learning goals and structure",
                    "Naming and domain selection"
                }
            },
            
            // Setup
            new Chapter
            {
                Id = 2,
                Number = 2,
                Title = "Developer Environment Setup",
                Description = "Install everything needed for .NET 10 development.",
                Category = ChapterCategory.Setup,
                Topics = new List<string>
                {
                    ".NET 10 SDK",
                    "IDE (VS Code / Visual Studio / Rider)",
                    "SQLite tools",
                    "Browser & Blazor debugging tools",
                    "Optional: Postman / Thunder Client"
                }
            },
            new Chapter
            {
                Id = 3,
                Number = 3,
                Title = "Creating the Solution (Project Template)",
                Description = "Build the initial folder structure and create the solution using clean architecture principles.",
                Category = ChapterCategory.Setup,
                Topics = new List<string>
                {
                    "dotnet new sln",
                    "Projects: Api (REST), Domain (Entities, contracts), Infrastructure (EF Core, DB), Web (Blazor dashboard)",
                    "Reference linking across projects"
                }
            },
            
            // API Development
            new Chapter
            {
                Id = 4,
                Number = 4,
                Title = "First API: Hello World",
                Description = "Create your first Minimal API endpoint using .NET 10.",
                Route = "/tutorial/api",
                Category = ChapterCategory.ApiDevelopment,
                Topics = new List<string>
                {
                    "Minimal API basics",
                    "Routing",
                    "Dependency injection basics",
                    "Running and testing the endpoint"
                }
            },
            new Chapter
            {
                Id = 5,
                Number = 5,
                Title = "Domain Modeling",
                Description = "Define the business domain, entities, and DTOs.",
                Route = "/tutorial/api",
                Category = ChapterCategory.ApiDevelopment,
                Topics = new List<string>
                {
                    "Entities with C# modern syntax",
                    "DTOs",
                    "Mapping principles",
                    "Validation basics",
                    "Separation of concerns (Domain vs API)"
                }
            },
            new Chapter
            {
                Id = 6,
                Number = 6,
                Title = "EF Core Setup (Infrastructure Layer)",
                Description = "Set up EF Core Code First with SQLite.",
                Route = "/tutorial/api",
                Category = ChapterCategory.ApiDevelopment,
                Topics = new List<string>
                {
                    "DbContext",
                    "Entity configuration",
                    "Migrations",
                    "Database creation",
                    "Connection strings",
                    "Dependency injection"
                }
            },
            new Chapter
            {
                Id = 7,
                Number = 7,
                Title = "CRUD Implementation",
                Description = "Build the core of the REST API.",
                Route = "/tutorial/api",
                Category = ChapterCategory.ApiDevelopment,
                Topics = new List<string>
                {
                    "GET list",
                    "GET by ID",
                    "POST create",
                    "PUT update",
                    "DELETE remove",
                    "Returning proper HTTP status codes",
                    "Async design"
                }
            },
            new Chapter
            {
                Id = 8,
                Number = 8,
                Title = "Validation, Error Handling & Logging",
                Description = "Handle user input, errors, and observability.",
                Route = "/tutorial/api",
                Category = ChapterCategory.ApiDevelopment,
                Topics = new List<string>
                {
                    "Validation using DataAnnotations",
                    "Exception handling middleware",
                    "Logging with ILogger",
                    "API responses conventions",
                    "Configuration using Options Pattern"
                }
            },
            
            // Blazor Basics
            new Chapter
            {
                Id = 9,
                Number = 9,
                Title = "Blazor Dashboard: Introduction",
                Description = "Start exploring Blazor as the UI framework.",
                Route = "/tutorial/components",
                Category = ChapterCategory.BlazorBasics,
                Topics = new List<string>
                {
                    "What is Blazor?",
                    "Rendering model",
                    "Server vs WASM",
                    "Project structure overview",
                    "Routing basics"
                }
            },
            new Chapter
            {
                Id = 10,
                Number = 10,
                Title = "Blazor Developer Environment Setup",
                Description = "Prepare the Blazor app for development.",
                Category = ChapterCategory.BlazorBasics,
                Topics = new List<string>
                {
                    "Running Blazor Server",
                    "Hot reload",
                    "Browser dev tools",
                    "Debugging C# in the browser"
                }
            },
            
            // Components
            new Chapter
            {
                Id = 11,
                Number = 11,
                Title = "Components at a Glance",
                Description = "Understand the concept of components.",
                Route = "/tutorial/components",
                Category = ChapterCategory.Components,
                Topics = new List<string>
                {
                    "Razor component structure",
                    ".razor anatomy",
                    "Code-behind patterns",
                    "Component lifecycle basics"
                }
            },
            new Chapter
            {
                Id = 12,
                Number = 12,
                Title = "Component Directives",
                Description = "Learn about the directives that power component behavior.",
                Route = "/tutorial/components",
                Category = ChapterCategory.Components,
                Topics = new List<string>
                {
                    "@page",
                    "@using",
                    "@inject",
                    "@code",
                    "@attribute"
                }
            },
            new Chapter
            {
                Id = 13,
                Number = 13,
                Title = "Event Handling",
                Description = "Handle UI interactions.",
                Route = "/tutorial/components",
                Category = ChapterCategory.Components,
                Topics = new List<string>
                {
                    "@onclick",
                    "EventCallback",
                    "Form submissions",
                    "Two-way binding (bind-Value)"
                }
            },
            new Chapter
            {
                Id = 14,
                Number = 14,
                Title = "Lifecycle Methods",
                Description = "Master the lifecycle flow of Blazor components.",
                Route = "/lifecycle",
                Category = ChapterCategory.Components,
                Topics = new List<string>
                {
                    "OnInitialized / OnInitializedAsync",
                    "OnParametersSet",
                    "OnAfterRender / OnAfterRenderAsync",
                    "When they run & common pitfalls"
                }
            },
            new Chapter
            {
                Id = 15,
                Number = 15,
                Title = "Component Rendering & State",
                Description = "Control rendering, refreshes, and state.",
                Route = "/tutorial/components",
                Category = ChapterCategory.Components,
                Topics = new List<string>
                {
                    "Render rules",
                    "StateHasChanged()",
                    "Preventing unnecessary rerenders",
                    "Local vs global state"
                }
            },
            new Chapter
            {
                Id = 16,
                Number = 16,
                Title = "Building Reusable Components",
                Description = "Design components to be reused across your dashboard.",
                Route = "/tutorial/components",
                Category = ChapterCategory.Components,
                Topics = new List<string>
                {
                    "Component patterns",
                    "Partial UI reuse",
                    "Slots via RenderFragment",
                    "Parameterizing components"
                }
            },
            
            // State Management
            new Chapter
            {
                Id = 17,
                Number = 17,
                Title = "Component Communication",
                Description = "Make components talk to each other.",
                Route = "/tutorial/state",
                Category = ChapterCategory.StateManagement,
                Topics = new List<string>
                {
                    "Parent → Child with [Parameter]",
                    "Child → Parent via EventCallback",
                    "Sibling communication patterns"
                }
            },
            new Chapter
            {
                Id = 18,
                Number = 18,
                Title = "Parameters",
                Description = "Use parameters to customize components.",
                Route = "/tutorial/state",
                Category = ChapterCategory.StateManagement,
                Topics = new List<string>
                {
                    "Required parameters",
                    "Optional parameters",
                    "Passing complex types",
                    "Parameter binding pitfalls"
                }
            },
            new Chapter
            {
                Id = 19,
                Number = 19,
                Title = "Cascading Parameters",
                Description = "Share state across the UI tree.",
                Route = "/tutorial/state",
                Category = ChapterCategory.StateManagement,
                Topics = new List<string>
                {
                    "CascadingValue",
                    "CascadingParameter",
                    "Global shared context",
                    "Pros/cons vs DI"
                }
            },
            new Chapter
            {
                Id = 20,
                Number = 20,
                Title = "Single Source of Truth",
                Description = "Implement proper state management in Blazor.",
                Route = "/tutorial/state",
                Category = ChapterCategory.StateManagement,
                Topics = new List<string>
                {
                    "Avoiding duplicated state",
                    "Centralized data services",
                    "Observables and notification patterns",
                    "UI state vs domain state"
                }
            },
            new Chapter
            {
                Id = 21,
                Number = 21,
                Title = "Styling & UI Foundations",
                Description = "Make the app look clean and consistent.",
                Category = ChapterCategory.StateManagement,
                Topics = new List<string>
                {
                    "CSS isolation",
                    "Conditional classes",
                    "Layout components",
                    "Basic integration with Bootstrap or Tailwind",
                    "UI composition best practices"
                }
            },
            
            // Dashboard
            new Chapter
            {
                Id = 22,
                Number = 22,
                Title = "Dashboard Pages Implementation",
                Description = "Build the real pages of your dashboard.",
                Route = "/courses",
                Category = ChapterCategory.Dashboard,
                Topics = new List<string>
                {
                    "List page",
                    "Details page",
                    "Create/Edit form page",
                    "Delete confirmation",
                    "Loading states",
                    "Error display"
                }
            },
            new Chapter
            {
                Id = 23,
                Number = 23,
                Title = "Integrating the API",
                Description = "Connect Blazor to the .NET 10 REST API.",
                Route = "/courses",
                Category = ChapterCategory.Dashboard,
                Topics = new List<string>
                {
                    "HttpClient",
                    "Fetching async",
                    "Mapping API data to UI models",
                    "Error handling in UI",
                    "Retry and fallback patterns"
                }
            },
            new Chapter
            {
                Id = 24,
                Number = 24,
                Title = "Enriching the Dashboard",
                Description = "Add useful UX features.",
                Route = "/courses",
                Category = ChapterCategory.Dashboard,
                Topics = new List<string>
                {
                    "Filtering",
                    "Searching",
                    "Sorting",
                    "Pagination",
                    "Optional: simple charts (bar or line)"
                }
            },
            
            // Advanced
            new Chapter
            {
                Id = 25,
                Number = 25,
                Title = "Testing (Optional)",
                Description = "Add minimal but useful tests.",
                Category = ChapterCategory.Advanced,
                Topics = new List<string>
                {
                    "API integration tests",
                    "Component tests",
                    "Basic test organization"
                }
            },
            new Chapter
            {
                Id = 26,
                Number = 26,
                Title = "Refactoring & Cleanup",
                Description = "Improve maintainability.",
                Category = ChapterCategory.Advanced,
                Topics = new List<string>
                {
                    "Organizing files",
                    "Extracting services",
                    "Improving naming",
                    "Architectural touch-ups"
                }
            },
            new Chapter
            {
                Id = 27,
                Number = 27,
                Title = "Extending the Application (Optional)",
                Description = "Show what features could be added next.",
                Category = ChapterCategory.Advanced,
                Topics = new List<string>
                {
                    "Authentication & Authorization",
                    "Role-based dashboard",
                    "Multi-tenant patterns",
                    "Background jobs",
                    "Real-time updates with SignalR"
                }
            },
            
            // Wrap-Up
            new Chapter
            {
                Id = 28,
                Number = 28,
                Title = "Wrap-Up",
                Description = "Recap the journey and what was learned.",
                Category = ChapterCategory.WrapUp,
                KeyPoints = new List<string>
                {
                    "Review of all concepts covered",
                    "Architecture decisions recap",
                    "Best practices summary",
                    "Next steps for continued learning"
                }
            }
        };
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
