---
id: 29
number: 29
title: "Hello, C#! Welcome, .NET!"
description: Set up your development environment, understand .NET 10 fundamentals, and create your first C# 14 application with modern features.
category: CSharpFundamentals
topics:
  - Setting up development environment
  - Understanding .NET 10
  - Building console apps
  - Managing multiple projects in a solution
  - Using GitHub repository resources
  - Running C# code without project files
---

## Step 1: Understanding .NET 10
**Type: Read**

Welcome to the world of **.NET 10** and **C# 14**! .NET 10 is the latest Long-Term Support (LTS) release, bringing performance improvements, new language features, and enhanced developer productivity.

Key features in .NET 10 include:

- **Performance improvements** across the runtime and libraries
- **Native AOT** (Ahead-of-Time) compilation for smaller, faster apps
- **Enhanced Blazor** with unified rendering modes
- **C# 14** with new language features

Here's your first glimpse of C# 14:

```csharp
// Top-level statements - no Main method required!
Console.WriteLine("Hello, .NET 10!");

// Using new C# 14 features
var greeting = "Welcome to the future of .NET!";
Console.WriteLine(greeting);
```

---

## Step 2: Setting Up Your Development Environment
**Type: Action**

Let's set up everything you need to start developing with .NET 10.

**1. Install the .NET 10 SDK:**

Download from [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/10.0)

**2. Verify installation:**

```bash
dotnet --version
# Should output: 10.0.xxx
```

**3. Choose your IDE:**

- **Visual Studio 2022** (Windows) - Full-featured IDE
- **Visual Studio Code** + C# Dev Kit (Cross-platform)
- **JetBrains Rider** (Cross-platform)

For VS Code, install these extensions:

```bash
# Install C# Dev Kit
code --install-extension ms-dotnettools.csdevkit
```

---

## Step 3: Creating Your First Console App
**Type: Action**

Let's create a simple console application!

```bash
# Create a new console app
dotnet new console -n HelloWorld

# Navigate to the project
cd HelloWorld

# Run the application
dotnet run
```

This creates a `Program.cs` file with:

```csharp
// Program.cs
Console.WriteLine("Hello, World!");
```

Modify it to be more interactive:

```csharp
Console.Write("What is your name? ");
var name = Console.ReadLine();
Console.WriteLine($"Hello, {name}! Welcome to .NET 10!");
```

---

## Step 4: Managing Solutions with Multiple Projects
**Type: Action**

Real applications often have multiple projects. Let's create a solution structure:

```bash
# Create solution
dotnet new sln -n MyFirstSolution

# Create projects
dotnet new console -n MyApp
dotnet new classlib -n MyApp.Core
dotnet new xunit -n MyApp.Tests

# Add projects to solution
dotnet sln add MyApp
dotnet sln add MyApp.Core
dotnet sln add MyApp.Tests

# Add project references
dotnet add MyApp reference MyApp.Core
dotnet add MyApp.Tests reference MyApp.Core
```

Your solution structure now looks like:

```treeview
MyFirstSolution/
├── MyFirstSolution.sln
├── MyApp/
│   ├── MyApp.csproj
│   └── Program.cs
├── MyApp.Core/
│   ├── MyApp.Core.csproj
│   └── Class1.cs
└── MyApp.Tests/
    ├── MyApp.Tests.csproj
    └── UnitTest1.cs
```

---

## Step 5: Running C# Without a Project File
**Type: Action**

.NET 10 supports running single C# files directly! Create a file called `script.cs`:

```csharp
// script.cs
Console.WriteLine("Running without a project!");

var numbers = new[] { 1, 2, 3, 4, 5 };
var sum = numbers.Sum();
Console.WriteLine($"Sum: {sum}");
```

Run it directly:

```bash
dotnet run script.cs
```

This is perfect for quick scripts, learning, and prototyping!

---

## Quiz

### Question 1
What command creates a new console application?
- dotnet create console
- dotnet new console
- dotnet init console
- dotnet make console

**Correct: 1**
**Explanation:** The `dotnet new console` command creates a new console application from the template.

### Question 2
What is the purpose of a .sln file?
- It contains compiled code
- It manages multiple projects together
- It stores application settings
- It defines the main entry point

**Correct: 1**
**Explanation:** A solution file (.sln) is used to organize and manage multiple related projects together.

### Question 3
Can you run a C# file without creating a project in .NET 10?
- No, projects are always required
- Yes, using dotnet run filename.cs
- Only with Visual Studio
- Only on Windows

**Correct: 1**
**Explanation:** .NET 10 supports running single C# files directly using `dotnet run filename.cs`.
