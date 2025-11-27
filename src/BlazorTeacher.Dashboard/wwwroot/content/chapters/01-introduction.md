---
id: 1
number: 1
title: Introduction
description: This chapter sets the stage: what we will build, the tech stack, the workflow, and the final outcome. You'll understand how the API, EF Core, and Blazor interact to form a complete full-stack .NET 10 application.
category: Introduction
keyPoints:
  - What .NET 10 brings
  - Architecture overview (API → Data → Blazor)
  - Learning goals and structure
  - Naming and domain selection
---

## Step 1: Welcome to the Course
**Type: Read**

Welcome! In this course, we will build a complete **.NET 10** application from scratch. 

We'll start with a robust **Minimal API**, connect it to a **SQLite** database using **EF Core**, and then build a beautiful **Blazor Server** dashboard to manage the data. 

By the end, you'll have a production-ready full-stack architecture.

Here's a preview of what a minimal API looks like:

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/api/hello", () => "Hello World");

app.Run();
```

Pretty simple, right? Let's get started!

---

## Step 2: The Architecture
**Type: Read**

We will use a **Clean Architecture** approach:

1. **Domain**: The core entities and business logic (no dependencies).
2. **Infrastructure**: Database access and external services.
3. **Api**: The REST API layer.
4. **Web**: The Blazor dashboard UI.

This separation ensures our code is maintainable and testable.

Here's how the project structure looks:

```treeview
BlazorTeacher/
|-- BlazorTeacher.Domain/
|   `-- Models/
|-- BlazorTeacher.Infrastructure/
|   `-- Data/
|-- BlazorTeacher.Api/
|   `-- Program.cs
`-- BlazorTeacher.Web/
    `-- Components/
```

---

## Step 3: Prerequisites Check
**Type: Action**

Before we begin, ensure you have the **.NET 10 SDK** installed. 

Open your terminal and run the following command:

```bash
dotnet --version
```

If you see `10.0.xxx`, you are ready to go! 

If not, download and install it from [Microsoft's official site](https://dotnet.microsoft.com/).

---

## Quiz

### Question 1
Which component will handle the database access?
- Blazor Web
- Infrastructure Layer
- Domain Layer
- API Layer

**Correct: 1**
**Explanation:** The Infrastructure layer is responsible for external concerns like database access (EF Core).

### Question 2
What type of API will we build?
- gRPC
- SOAP
- Minimal API (REST)
- GraphQL

**Correct: 2**
**Explanation:** We will use .NET's Minimal API features to build a lightweight and fast REST API.
