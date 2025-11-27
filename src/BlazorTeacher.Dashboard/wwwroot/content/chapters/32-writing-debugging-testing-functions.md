---
id: 32
number: 32
title: "Writing, Debugging, and Testing Functions"
description: Learn to write functions, debug effectively, use hot reload, write unit tests, and handle exceptions properly.
category: CSharpFundamentals
topics:
  - Writing functions
  - Method parameters and return types
  - Debugging techniques
  - Hot reloading
  - Unit testing with xUnit
  - Exception handling in functions
---

## Step 1: Writing Functions (Methods)
**Type: Read**

Functions (methods in C#) are reusable blocks of code:

```csharp
// Basic method
static void SayHello()
{
    Console.WriteLine("Hello!");
}

// Method with parameters
static void Greet(string name)
{
    Console.WriteLine($"Hello, {name}!");
}

// Method with return value
static int Add(int a, int b)
{
    return a + b;
}

// Expression-bodied methods
static int Multiply(int a, int b) => a * b;
static string GetGreeting(string name) => $"Hello, {name}!";

// Usage
SayHello();
Greet("Alice");
int sum = Add(5, 3);
Console.WriteLine($"Sum: {sum}");
```

---

## Step 2: Method Parameters
**Type: Read**

C# offers various ways to pass parameters:

```csharp
// Optional parameters with default values
static void PrintMessage(string message, int times = 1)
{
    for (int i = 0; i < times; i++)
        Console.WriteLine(message);
}
PrintMessage("Hello");        // Prints once
PrintMessage("Hello", 3);     // Prints three times

// Named parameters
static void CreateUser(string name, int age, bool isAdmin = false)
{
    Console.WriteLine($"Name: {name}, Age: {age}, Admin: {isAdmin}");
}
CreateUser(age: 25, name: "Bob");  // Order doesn't matter

// ref parameter (two-way)
static void Double(ref int number)
{
    number *= 2;
}
int value = 5;
Double(ref value);  // value is now 10

// out parameter (one-way, out of method)
static bool TryDivide(int a, int b, out int result)
{
    if (b == 0)
    {
        result = 0;
        return false;
    }
    result = a / b;
    return true;
}

// params for variable arguments
static int Sum(params int[] numbers)
{
    return numbers.Sum();
}
int total = Sum(1, 2, 3, 4, 5);  // 15
```

---

## Step 3: Debugging During Development
**Type: Action**

Effective debugging saves hours of development time:

**Using Visual Studio / VS Code Debugger:**

1. **Set breakpoints** - Click in the left margin of the code
2. **Start debugging** - Press F5 (or `dotnet run` with attach)
3. **Step through code:**
   - F10 - Step Over (execute current line)
   - F11 - Step Into (go inside method calls)
   - Shift+F11 - Step Out (exit current method)

**Debug output:**

```csharp
// Console output (basic)
Console.WriteLine($"Debug: value = {value}");

// Debug class (only in debug builds)
System.Diagnostics.Debug.WriteLine("Debug message");

// Conditional debugging
#if DEBUG
Console.WriteLine("This only runs in debug mode");
#endif

// Debugger break
if (suspiciousValue < 0)
{
    System.Diagnostics.Debugger.Break();  // Pause here
}
```

**Debugging tips:**

```csharp
// Validate assumptions with Debug.Assert
System.Diagnostics.Debug.Assert(value > 0, "Value must be positive");

// Log state at key points
var data = GetData();
Console.WriteLine($"Data count: {data.Count}, First: {data.FirstOrDefault()}");
```

---

## Step 4: Hot Reload During Development
**Type: Action**

.NET 10 supports Hot Reload for rapid development:

```bash
# Run with hot reload enabled
dotnet watch run
```

With `dotnet watch`, you can:
- Modify code while the app is running
- See changes instantly without restarting
- Works with ASP.NET Core, Blazor, and console apps

**Supported changes:**
- Add/modify method bodies
- Add new methods and classes
- Modify CSS and Razor markup
- Add properties to existing types

**Limitations:**
- Cannot change method signatures
- Cannot rename types
- Cannot change inheritance

Example workflow:

```csharp
// Original code
static void ProcessOrder(Order order)
{
    Console.WriteLine($"Processing order {order.Id}");
}

// Modify while app runs (Hot Reload applies automatically)
static void ProcessOrder(Order order)
{
    Console.WriteLine($"Processing order {order.Id} for {order.Customer}");
    SendConfirmationEmail(order);  // Added line
}
```

---

## Step 5: Unit Testing
**Type: Action**

Write tests to ensure your code works correctly:

**Create a test project:**

```bash
dotnet new xunit -n MyApp.Tests
dotnet add MyApp.Tests reference MyApp.Core
```

**Writing tests with xUnit:**

```csharp
// Calculator.cs (in main project)
public class Calculator
{
    public int Add(int a, int b) => a + b;
    public int Divide(int a, int b)
    {
        if (b == 0) throw new DivideByZeroException();
        return a / b;
    }
}

// CalculatorTests.cs (in test project)
using Xunit;

public class CalculatorTests
{
    private readonly Calculator _calculator = new();

    [Fact]
    public void Add_TwoPositiveNumbers_ReturnsSum()
    {
        // Arrange & Act
        int result = _calculator.Add(2, 3);

        // Assert
        Assert.Equal(5, result);
    }

    [Theory]
    [InlineData(10, 2, 5)]
    [InlineData(9, 3, 3)]
    [InlineData(100, 10, 10)]
    public void Divide_ValidNumbers_ReturnsQuotient(int a, int b, int expected)
    {
        int result = _calculator.Divide(a, b);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Divide_ByZero_ThrowsException()
    {
        Assert.Throws<DivideByZeroException>(() => _calculator.Divide(10, 0));
    }
}
```

**Run tests:**

```bash
dotnet test
# Or with detailed output
dotnet test --logger "console;verbosity=detailed"
```

---

## Step 6: Exception Handling in Functions
**Type: Read**

Design your functions to handle errors appropriately:

```csharp
// Throw meaningful exceptions
public class UserService
{
    public User GetUser(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID must be positive", nameof(id));

        var user = _repository.FindById(id);
        
        if (user == null)
            throw new KeyNotFoundException($"User with ID {id} not found");
            
        return user;
    }
}

// Custom exception
public class ValidationException : Exception
{
    public List<string> Errors { get; }
    
    public ValidationException(List<string> errors) 
        : base("Validation failed")
    {
        Errors = errors;
    }
}

// Guard clauses pattern
public void ProcessOrder(Order order)
{
    ArgumentNullException.ThrowIfNull(order);
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(order.Quantity);
    
    // Main logic here - no deep nesting
}

// Result pattern (alternative to exceptions)
public record Result<T>(bool IsSuccess, T? Value, string? Error)
{
    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(string error) => new(false, default, error);
}

public Result<User> TryGetUser(int id)
{
    if (id <= 0)
        return Result<User>.Failure("Invalid ID");
        
    var user = _repository.FindById(id);
    return user != null 
        ? Result<User>.Success(user) 
        : Result<User>.Failure("User not found");
}
```

---

## Quiz

### Question 1
What does the 'params' keyword allow?
- Passing parameters by reference
- Passing a variable number of arguments
- Making parameters optional
- Passing output parameters

**Correct: 1**
**Explanation:** The 'params' keyword allows a method to accept a variable number of arguments as an array.

### Question 2
Which command enables hot reload in .NET?
- dotnet run --hot
- dotnet watch run
- dotnet reload
- dotnet dev run

**Correct: 1**
**Explanation:** The 'dotnet watch run' command enables hot reload, watching for file changes and automatically applying them.

### Question 3
What attribute marks a test method with test data?
- [Fact]
- [Theory]
- [Test]
- [Data]

**Correct: 1**
**Explanation:** [Theory] with [InlineData] allows parameterized tests with different input values, while [Fact] is for single test cases.
