---
id: 15
number: 15
title: "First API: Hello World"
description: Create your first Minimal API endpoint using .NET 10.
route: /tutorial/api
category: ApiDevelopment
topics:
  - Minimal API basics
  - Routing
  - Dependency injection basics
  - Running and testing the endpoint
---

## Step 1: Create Endpoint
**Type: Action**

Let's create your first API endpoint! 

In `Program.cs`, add the following code:

```csharp
app.MapGet("/hello", () => "Hello World");
```

This creates a simple GET endpoint at `/hello` that returns "Hello World".

You can also return JSON objects:

```csharp
app.MapGet("/api/info", () => new { 
    Name = "BlazorTeacher API", 
    Version = "1.0",
    Status = "Running"
});
```

---

## Step 2: Run API
**Type: Action**

Now let's run the API!

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
```

---

## Step 3: Test
**Type: Action**

Open your browser and navigate to:

```
http://localhost:5000/hello
```

You should see "Hello World" displayed in your browser!

You can also test with curl:

```bash
curl http://localhost:5000/hello
```

Or use Postman/Thunder Client for more advanced testing.

---

## Quiz

### Question 1
What method creates a GET endpoint?
- MapPost
- MapGet
- MapPut
- Get

**Correct: 1**
**Explanation:** MapGet creates a GET endpoint.
