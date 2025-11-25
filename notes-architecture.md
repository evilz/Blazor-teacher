# Architecture Decisions

## Project Name
BlazorTeacher

A full-stack .NET 10 tutorial app demonstrating REST API + Blazor Dashboard integration.

## Domain

### Main Entity: Course
A learning management system course with the following fields:
- Id (int)
- Title (string, required)
- Description (string)
- Instructor (string, required)
- DurationInHours (int)
- Price (decimal)
- Level (enum: Beginner/Intermediate/Advanced)
- IsPublished (bool)
- CreatedAt (DateTime)
- UpdatedAt (DateTime?, optional)

### Related Entity: Lesson
Represents a lesson within a course (1-to-many relationship):
- Id (int)
- Title (string, required)
- Content (string)
- Order (int)
- DurationInMinutes (int)
- VideoUrl (string?, optional)
- CourseId (int) - Foreign key to Course
- CreatedAt (DateTime)

## Stack
- .NET 10
- REST API: Controllers
- Database: SQLite + EF Core Code First
- UI: Blazor Server
- Projects:
  - BlazorTeacher.Shared (Domain models & DTOs)
  - BlazorTeacher.Api (REST API with Controllers)
  - BlazorTeacher.Dashboard (Blazor Web App)

## Architecture Diagram

```
[ Blazor Web (BlazorTeacher.Dashboard) ]
          |
       HttpClient
          |
[ REST API (BlazorTeacher.Api) ]
          |
       EF Core
          |
[ SQLite DB (Data/AppDbContext.cs) ]
          |
[ Domain models (BlazorTeacher.Shared) ]
```

## Non-functional Goals
- Simple, readable code
- Async everywhere
- Clear separation API / Domain / UI
- Easy to extend later (more entities)
- Built-in tutorial progress tracking
- Interactive learning demos

## Alternative Domain (Mini-Exercise)

### Entity: Chapter
Represents a tutorial chapter for learning progress tracking:
- Id (int)
- Number (int)
- Title (string, required)
- Description (string)
- Route (string?, optional)
- Category (enum: Introduction/Setup/ApiDevelopment/BlazorBasics/Components/StateManagement/Dashboard/Advanced/WrapUp)
- Topics (List<string>)
- KeyPoints (List<string>)

Relationship: A Chapter can have multiple learning progress entries (ChapterProgress).

## Quick Quiz Answers

1. **What are the 4 main pieces of the app?**
   - Domain models (BlazorTeacher.Shared)
   - Infrastructure/Data (EF Core + SQLite in BlazorTeacher.Api)
   - REST API (BlazorTeacher.Api)
   - Blazor Dashboard (BlazorTeacher.Dashboard)

2. **Why is it useful to define the domain and fields before writing code?**
   - Ensures clear understanding of business requirements
   - Helps design proper database schema and relationships
   - Allows for better API endpoint planning
   - Reduces refactoring later in development

3. **Why might Blazor Server be easier to start with than Blazor WASM?**
   - No need for a separate API call for initial data (direct DB access)
   - Faster initial load (smaller download)
   - Easier debugging (runs on server)
   - Full access to .NET libraries without restrictions
   - SignalR handles UI updates automatically
