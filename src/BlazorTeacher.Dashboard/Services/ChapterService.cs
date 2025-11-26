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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep 
                    { 
                        Title = "Welcome to the Course", 
                        Content = @"Welcome! In this course, we will build a complete **.NET 10** application from scratch. 

We'll start with a robust **Minimal API**, connect it to a **SQLite** database using **EF Core**, and then build a beautiful **Blazor Server** dashboard to manage the data. 

By the end, you'll have a production-ready full-stack architecture.

Here's a preview of what a minimal API looks like:

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet(""/api/hello"", () => ""Hello World"");

app.Run();
```

Pretty simple, right? Let's get started!",
                        Type = StepType.Read
                    },
                    new ChapterStep 
                    { 
                        Title = "The Architecture", 
                        Content = @"We will use a **Clean Architecture** approach:

1. **Domain**: The core entities and business logic (no dependencies).
2. **Infrastructure**: Database access and external services.
3. **Api**: The REST API layer.
4. **Web**: The Blazor dashboard UI.

This separation ensures our code is maintainable and testable.

Here's how the project structure looks:

```
BlazorTeacher/
├── BlazorTeacher.Domain/
│   └── Models/
├── BlazorTeacher.Infrastructure/
│   └── Data/
├── BlazorTeacher.Api/
│   └── Program.cs
└── BlazorTeacher.Web/
    └── Components/
```",
                        Type = StepType.Read
                    },
                    new ChapterStep 
                    { 
                        Title = "Prerequisites Check", 
                        Content = @"Before we begin, ensure you have the **.NET 10 SDK** installed. 

Open your terminal and run the following command:

```bash
dotnet --version
```

If you see `10.0.xxx`, you are ready to go! 

If not, download and install it from [Microsoft's official site](https://dotnet.microsoft.com/).",
                        Type = StepType.Action
                    }
                },
                Quiz = new Quiz
                {
                    Questions = new List<QuizQuestion>
                    {
                        new QuizQuestion
                        {
                            Text = "Which component will handle the database access?",
                            Options = new List<string> { "Blazor Web", "Infrastructure Layer", "Domain Layer", "API Layer" },
                            CorrectOptionIndex = 1,
                            Explanation = "The Infrastructure layer is responsible for external concerns like database access (EF Core)."
                        },
                        new QuizQuestion
                        {
                            Text = "What type of API will we build?",
                            Options = new List<string> { "gRPC", "SOAP", "Minimal API (REST)", "GraphQL" },
                            CorrectOptionIndex = 2,
                            Explanation = "We will use .NET's Minimal API features to build a lightweight and fast REST API."
                        }
                    }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep
                    {
                        Title = "Install .NET 10 SDK",
                        Content = "Download and install the **.NET 10 SDK** from the official Microsoft website. Verify the installation by running `dotnet --version` in your terminal.",
                        Type = StepType.Action
                    },
                    new ChapterStep
                    {
                        Title = "Choose Your IDE",
                        Content = "We recommend **Visual Studio 2022** (latest preview) or **VS Code** with the C# Dev Kit extension. **JetBrains Rider** is also a great choice.",
                        Type = StepType.Read
                    },
                    new ChapterStep
                    {
                        Title = "Install SQLite Tools",
                        Content = "We will use SQLite. You can install a GUI tool like **DB Browser for SQLite** or use the VS Code extension 'SQLite'.",
                        Type = StepType.Action
                    }
                },
                Quiz = new Quiz
                {
                    Questions = new List<QuizQuestion>
                    {
                        new QuizQuestion
                        {
                            Text = "Which command checks the installed .NET version?",
                            Options = new List<string> { "dotnet check", "dotnet --version", "dotnet info", "dotnet sdk" },
                            CorrectOptionIndex = 1,
                            Explanation = "`dotnet --version` displays the installed SDK version."
                        }
                    }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep
                    {
                        Title = "Create Solution",
                        Content = "Run `dotnet new sln -n BlazorTeacher` to create an empty solution file.",
                        Type = StepType.Action
                    },
                    new ChapterStep
                    {
                        Title = "Create Projects",
                        Content = "Create the projects:\n\n1. `dotnet new classlib -n BlazorTeacher.Domain`\n2. `dotnet new classlib -n BlazorTeacher.Infrastructure`\n3. `dotnet new webapi -n BlazorTeacher.Api`\n4. `dotnet new blazor -n BlazorTeacher.Web`",
                        Type = StepType.Action
                    },
                    new ChapterStep
                    {
                        Title = "Link Projects",
                        Content = "Add references:\n- Api references Infrastructure & Domain\n- Infrastructure references Domain\n- Web references Domain (and Api client later)",
                        Type = StepType.Read
                    }
                },
                Quiz = new Quiz
                {
                    Questions = new List<QuizQuestion>
                    {
                        new QuizQuestion
                        {
                            Text = "Which project should have NO dependencies on others?",
                            Options = new List<string> { "Api", "Infrastructure", "Domain", "Web" },
                            CorrectOptionIndex = 2,
                            Explanation = "The Domain layer is the core and should not depend on outer layers."
                        }
                    }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep 
                    { 
                        Title = "Create Endpoint", 
                        Content = @"Let's create your first API endpoint! 

In `Program.cs`, add the following code:

```csharp
app.MapGet(""/hello"", () => ""Hello World"");
```

This creates a simple GET endpoint at `/hello` that returns ""Hello World"".

You can also return JSON objects:

```csharp
app.MapGet(""/api/info"", () => new { 
    Name = ""BlazorTeacher API"", 
    Version = ""1.0"",
    Status = ""Running""
});
```",
                        Type = StepType.Action 
                    },
                    new ChapterStep 
                    { 
                        Title = "Run API", 
                        Content = @"Now let's run the API!

Open your terminal in the Api project folder and run:

```bash
dotnet run
```

You should see output similar to this:

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```",
                        Type = StepType.Action 
                    },
                    new ChapterStep 
                    { 
                        Title = "Test", 
                        Content = @"Open your browser and navigate to:

```
http://localhost:5000/hello
```

You should see ""Hello World"" displayed in your browser!

You can also test with curl:

```bash
curl http://localhost:5000/hello
```

Or use Postman/Thunder Client for more advanced testing.",
                        Type = StepType.Action 
                    }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "What method creates a GET endpoint?", Options = new List<string> { "MapPost", "MapGet", "MapPut", "Get" }, CorrectOptionIndex = 1, Explanation = "MapGet creates a GET endpoint." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Create Entity", Content = "Create `Course.cs` in Domain project.", Type = StepType.Action },
                    new ChapterStep { Title = "Define Properties", Content = "Add `Id`, `Title`, `Price` properties.", Type = StepType.Action },
                    new ChapterStep { Title = "Create DTO", Content = "Create `CourseDto.cs` for API responses.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "Where should entities live?", Options = new List<string> { "Api", "Infrastructure", "Domain", "Web" }, CorrectOptionIndex = 2, Explanation = "Entities belong in the Domain." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Install EF Core", Content = "Add `Microsoft.EntityFrameworkCore.Sqlite` package.", Type = StepType.Action },
                    new ChapterStep { Title = "Create DbContext", Content = "Inherit from `DbContext` and add `DbSet<Course>`.", Type = StepType.Action },
                    new ChapterStep { Title = "Add Migration", Content = "Run `dotnet ef migrations add InitialCreate`.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "What tool manages DB schema changes?", Options = new List<string> { "Git", "Migrations", "Docker", "NPM" }, CorrectOptionIndex = 1, Explanation = "EF Core Migrations manage schema changes." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Implement GET", Content = "Use `dbContext.Courses.ToListAsync()`.", Type = StepType.Action },
                    new ChapterStep { Title = "Implement POST", Content = "Add endpoint to add new course.", Type = StepType.Action },
                    new ChapterStep { Title = "Implement DELETE", Content = "Find by ID and Remove.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "Which HTTP method is for creating?", Options = new List<string> { "GET", "PUT", "POST", "DELETE" }, CorrectOptionIndex = 2, Explanation = "POST is standard for creation." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Add Validation", Content = "Use `[Required]` on DTO properties.", Type = StepType.Action },
                    new ChapterStep { Title = "Global Error Handling", Content = "Add middleware to catch exceptions.", Type = StepType.Action },
                    new ChapterStep { Title = "Logging", Content = "Inject `ILogger` and log actions.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "What attribute marks a field as mandatory?", Options = new List<string> { "[Key]", "[Required]", "[MaxLength]", "[Table]" }, CorrectOptionIndex = 1, Explanation = "[Required] ensures the field is not null/empty." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Explore Project", Content = "Look at `App.razor` and `MainLayout.razor`.", Type = StepType.Read },
                    new ChapterStep { Title = "Run App", Content = "Run `dotnet run` in Web project.", Type = StepType.Action },
                    new ChapterStep { Title = "See Counter", Content = "Navigate to Counter page and click button.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "Where does Blazor Server code run?", Options = new List<string> { "Client Browser", "Web Server", "Database", "CDN" }, CorrectOptionIndex = 1, Explanation = "Blazor Server runs on the server and communicates via SignalR." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Enable Hot Reload", Content = "Run with `dotnet watch`.", Type = StepType.Action },
                    new ChapterStep { Title = "Make Change", Content = "Change text in `Index.razor` and save.", Type = StepType.Action },
                    new ChapterStep { Title = "Verify", Content = "See browser update instantly.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "What command enables Hot Reload?", Options = new List<string> { "dotnet run", "dotnet build", "dotnet watch", "dotnet clean" }, CorrectOptionIndex = 2, Explanation = "dotnet watch monitors files and reloads." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Create Component", Content = "Create `MyComponent.razor`.", Type = StepType.Action },
                    new ChapterStep { Title = "Add Markup", Content = "Add `<h3>My Component</h3>`.", Type = StepType.Action },
                    new ChapterStep { Title = "Use Component", Content = "Add `<MyComponent />` to `Index.razor`.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "What is the file extension for a Razor component?", Options = new List<string> { ".cs", ".html", ".razor", ".cshtml" }, CorrectOptionIndex = 2, Explanation = ".razor is used for Blazor components." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Add Route", Content = "Add `@page \"/my-page\"` to top of file.", Type = StepType.Action },
                    new ChapterStep { Title = "Inject Service", Content = "Add `@inject NavigationManager Nav`.", Type = StepType.Action },
                    new ChapterStep { Title = "Add Code", Content = "Add `@code { ... }` block.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "Which directive defines a route?", Options = new List<string> { "@route", "@page", "@path", "@url" }, CorrectOptionIndex = 1, Explanation = "@page defines the URL route." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Handle Click", Content = "Add `@onclick=\"HandleClick\"` to a button.", Type = StepType.Action },
                    new ChapterStep { Title = "Bind Input", Content = "Use `@bind-Value=\"myText\"` on an input.", Type = StepType.Action },
                    new ChapterStep { Title = "Test", Content = "Type in input and see value update.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "How do you bind an input value?", Options = new List<string> { "@bind", "@bind-Value", "value=", "ng-model" }, CorrectOptionIndex = 1, Explanation = "@bind-Value is the standard two-way binding syntax." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Override OnInitialized", Content = "Load data in `OnInitializedAsync`.", Type = StepType.Action },
                    new ChapterStep { Title = "Log Lifecycle", Content = "Add `Console.WriteLine` to lifecycle methods.", Type = StepType.Action },
                    new ChapterStep { Title = "Observe", Content = "Watch console when navigating.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "Which method runs first?", Options = new List<string> { "OnAfterRender", "OnParametersSet", "OnInitialized", "Dispose" }, CorrectOptionIndex = 2, Explanation = "OnInitialized runs first after the component is created." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Manual Render", Content = "Call `StateHasChanged()` after async work.", Type = StepType.Action },
                    new ChapterStep { Title = "ShouldRender", Content = "Override `ShouldRender` to optimize.", Type = StepType.Read },
                    new ChapterStep { Title = "Test", Content = "Verify UI updates correctly.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "What method forces a re-render?", Options = new List<string> { "Render()", "Update()", "StateHasChanged()", "Refresh()" }, CorrectOptionIndex = 2, Explanation = "StateHasChanged() notifies the component that state has changed." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Create Card", Content = "Create a reusable `Card.razor`.", Type = StepType.Action },
                    new ChapterStep { Title = "Add ChildContent", Content = "Add `[Parameter] public RenderFragment ChildContent { get; set; }`.", Type = StepType.Action },
                    new ChapterStep { Title = "Use Card", Content = "Wrap content in `<Card>...</Card>`.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "What type is used for child content?", Options = new List<string> { "string", "html", "RenderFragment", "ComponentBase" }, CorrectOptionIndex = 2, Explanation = "RenderFragment represents a segment of UI content." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Parent to Child", Content = "Pass data via `[Parameter]`.", Type = StepType.Action },
                    new ChapterStep { Title = "Child to Parent", Content = "Invoke `EventCallback` from child.", Type = StepType.Action },
                    new ChapterStep { Title = "Test", Content = "Verify data flows both ways.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "How does a child notify a parent?", Options = new List<string> { "Function Call", "EventCallback", "SignalR", "Global State" }, CorrectOptionIndex = 1, Explanation = "EventCallback allows child components to trigger events in the parent." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Add Parameter", Content = "Add `[Parameter] public string Text { get; set; }`.", Type = StepType.Action },
                    new ChapterStep { Title = "Make Required", Content = "Add `[EditorRequired]` attribute.", Type = StepType.Action },
                    new ChapterStep { Title = "Pass Object", Content = "Pass a `Course` object as a parameter.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "What attribute enforces a parameter is set?", Options = new List<string> { "[Required]", "[EditorRequired]", "[Mandatory]", "[NotNull]" }, CorrectOptionIndex = 1, Explanation = "[EditorRequired] warns if a parameter is missing at design time." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Wrap App", Content = "Wrap `App.razor` content in `<CascadingValue Value=\"...\">`.", Type = StepType.Action },
                    new ChapterStep { Title = "Consume Value", Content = "Use `[CascadingParameter]` in a child component.", Type = StepType.Action },
                    new ChapterStep { Title = "Verify", Content = "Check if deep child receives the value.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "What tag provides a cascading value?", Options = new List<string> { "<Provider>", "<CascadingValue>", "<Context>", "<Global>" }, CorrectOptionIndex = 1, Explanation = "<CascadingValue> provides a value to all descendants." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Create State Service", Content = "Create `AppState.cs` with an event.", Type = StepType.Action },
                    new ChapterStep { Title = "Inject Service", Content = "Register as Scoped in `Program.cs`.", Type = StepType.Action },
                    new ChapterStep { Title = "Subscribe", Content = "Components subscribe to `OnChange` event.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "What lifetime should a state service usually have?", Options = new List<string> { "Transient", "Scoped", "Singleton", "Static" }, CorrectOptionIndex = 1, Explanation = "Scoped is per-user-session in Blazor Server." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "CSS Isolation", Content = "Create `MyComponent.razor.css`.", Type = StepType.Action },
                    new ChapterStep { Title = "Add Styles", Content = "Add scoped styles.", Type = StepType.Action },
                    new ChapterStep { Title = "Conditional Class", Content = "Use `@(isActive ? \"active\" : \"\")`.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "How do you scope CSS to a component?", Options = new List<string> { "Inline styles", "Global CSS", "Component.razor.css", "!important" }, CorrectOptionIndex = 2, Explanation = "Creating a file named Component.razor.css enables CSS isolation." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Create List Page", Content = "Fetch and display list of courses.", Type = StepType.Action },
                    new ChapterStep { Title = "Create Details Page", Content = "Show details for a specific ID.", Type = StepType.Action },
                    new ChapterStep { Title = "Create Form", Content = "Use `EditForm` for creating courses.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "What component handles form validation?", Options = new List<string> { "Form", "EditForm", "ValidateForm", "InputForm" }, CorrectOptionIndex = 1, Explanation = "EditForm is the standard Blazor component for forms with validation." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Inject HttpClient", Content = "Inject `HttpClient` configured for API.", Type = StepType.Action },
                    new ChapterStep { Title = "Fetch Data", Content = "Call `GetFromJsonAsync<List<CourseDto>>`.", Type = StepType.Action },
                    new ChapterStep { Title = "Handle Errors", Content = "Wrap in try-catch and show error message.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "What method fetches JSON data?", Options = new List<string> { "GetAsync", "GetJsonAsync", "GetFromJsonAsync", "FetchJson" }, CorrectOptionIndex = 2, Explanation = "GetFromJsonAsync is the helper method to fetch and deserialize JSON." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Add Search", Content = "Filter list based on text input.", Type = StepType.Action },
                    new ChapterStep { Title = "Add Sorting", Content = "Sort list by clicking headers.", Type = StepType.Action },
                    new ChapterStep { Title = "Add Pagination", Content = "Show only 10 items per page.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "Where should complex filtering logic ideally happen?", Options = new List<string> { "Client Browser", "API/Database", "Middleware", "CSS" }, CorrectOptionIndex = 1, Explanation = "Filtering at the database level is most efficient for large datasets." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Setup Test Project", Content = "Create xUnit project.", Type = StepType.Action },
                    new ChapterStep { Title = "Test API", Content = "Use `WebApplicationFactory` to test endpoints.", Type = StepType.Action },
                    new ChapterStep { Title = "Test Components", Content = "Use `bunit` library for component tests.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "What library is popular for Blazor testing?", Options = new List<string> { "Jest", "bunit", "Mocha", "Selenium" }, CorrectOptionIndex = 1, Explanation = "bunit is the standard for unit testing Blazor components." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Review Code", Content = "Look for duplicated logic.", Type = StepType.Action },
                    new ChapterStep { Title = "Extract Service", Content = "Move logic to a dedicated service.", Type = StepType.Action },
                    new ChapterStep { Title = "Clean Up", Content = "Remove unused usings and comments.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "Why refactor?", Options = new List<string> { "To make code faster", "To make code maintainable", "To add features", "To break things" }, CorrectOptionIndex = 1, Explanation = "Refactoring primarily improves maintainability and readability." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Explore Auth", Content = "Read about ASP.NET Core Identity.", Type = StepType.Read },
                    new ChapterStep { Title = "Explore SignalR", Content = "Understand real-time communication.", Type = StepType.Read },
                    new ChapterStep { Title = "Plan Next", Content = "Decide what to add next.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "What handles real-time web functionality in .NET?", Options = new List<string> { "WCF", "SignalR", "REST", "SOAP" }, CorrectOptionIndex = 1, Explanation = "SignalR is the library for real-time web functionality." } } }
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
                },
                Steps = new List<ChapterStep>
                {
                    new ChapterStep { Title = "Review", Content = "Look back at what you built.", Type = StepType.Read },
                    new ChapterStep { Title = "Celebrate", Content = "You built a full-stack .NET 10 app!", Type = StepType.Read },
                    new ChapterStep { Title = "Share", Content = "Share your project on GitHub.", Type = StepType.Action }
                },
                Quiz = new Quiz { Questions = new List<QuizQuestion> { new QuizQuestion { Text = "What was the main UI framework used?", Options = new List<string> { "Angular", "React", "Blazor", "Vue" }, CorrectOptionIndex = 2, Explanation = "We used Blazor for the UI." } } }
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
