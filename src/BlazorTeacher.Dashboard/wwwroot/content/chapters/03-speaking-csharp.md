---
id: 3
number: 3
title: "Speaking C#"
description: Learn the C# 14 language fundamentals including grammar, vocabulary, types, and working with variables using modern syntax.
category: CSharpFundamentals
topics:
  - C# language introduction
  - C# compiler versions
  - Grammar and vocabulary
  - Variables and data types
  - Console application features
---

## Step 1: The C# Language
**Type: Read**

C# (pronounced "C-sharp") is a modern, object-oriented programming language developed by Microsoft. With C# 14 and .NET 10, you get access to the latest language features and improvements.

**C# Language Evolution:**

| Version | .NET Version | Key Features |
|---------|--------------|--------------|
| C# 12 | .NET 8 | Primary constructors, collection expressions |
| C# 13 | .NET 9 | Params collections, implicit indexer access |
| C# 14 | .NET 10 | Field keyword, extension everything |

Check your compiler version:

```bash
dotnet --version  # SDK version
csc --version     # Compiler version (if available)
```

Or in code:

```csharp
#error version  // Compiler will show version in error message
```

---

## Step 2: Understanding C# Grammar
**Type: Read**

C# has a clear and consistent grammar. Let's explore the key elements:

**Statements and Expressions:**

```csharp
// Statements end with semicolons
int x = 10;                    // Variable declaration statement
Console.WriteLine(x);          // Method call statement

// Expressions produce values
var result = x + 5;            // x + 5 is an expression
var isPositive = x > 0;        // x > 0 is a boolean expression
```

**Code Blocks and Scope:**

```csharp
{
    // Code block - defines a scope
    int localVariable = 42;
    Console.WriteLine(localVariable);
}
// localVariable is not accessible here
```

**Comments:**

```csharp
// Single-line comment

/* 
   Multi-line comment
   Can span multiple lines
*/

/// <summary>
/// XML documentation comment
/// </summary>
```

---

## Step 3: Variables and Type System
**Type: Read**

C# is a strongly-typed language. Every variable has a specific type.

**Value Types:**

```csharp
// Integers
int age = 25;           // 32-bit signed integer
long population = 8_000_000_000L;  // 64-bit (use _ for readability!)
short small = 100;      // 16-bit
byte tiny = 255;        // 8-bit unsigned

// Floating-point
double price = 19.99;   // 64-bit (default for decimals)
float temperature = 98.6f;  // 32-bit
decimal money = 1234.56m;   // 128-bit (best for financial)

// Boolean
bool isActive = true;

// Character
char grade = 'A';
```

**Reference Types:**

```csharp
// Strings
string name = "Alice";
string greeting = $"Hello, {name}!";  // String interpolation

// Objects
object anything = "I can hold any value";

// Arrays
int[] numbers = [1, 2, 3, 4, 5];  // C# 12+ collection expression
string[] fruits = new[] { "apple", "banana", "cherry" };
```

---

## Step 4: Type Inference with var
**Type: Read**

Use `var` for local variables when the type is obvious:

```csharp
// Compiler infers the type
var message = "Hello";        // string
var count = 42;               // int
var prices = new List<decimal>();  // List<decimal>

// var is great for complex types
var customerOrders = customers
    .Where(c => c.IsActive)
    .SelectMany(c => c.Orders)
    .ToList();

// Don't use var when type isn't clear
int quantity;  // Better than 'var quantity;' - shows intent
```

**Target-typed new (C# 9+):**

```csharp
// Type is inferred from the left side
List<string> names = new();  // Same as new List<string>()
Dictionary<int, string> map = new()
{
    [1] = "One",
    [2] = "Two"
};
```

---

## Step 5: Working with Console Applications
**Type: Action**

Let's build a more interactive console application:

```csharp
// Reading input
Console.Write("Enter your name: ");
string? name = Console.ReadLine();

// Parsing numbers
Console.Write("Enter your age: ");
if (int.TryParse(Console.ReadLine(), out int age))
{
    Console.WriteLine($"Hello {name}, you are {age} years old!");
}
else
{
    Console.WriteLine("Invalid age entered.");
}

// Console formatting
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!");
Console.ResetColor();

// Reading a key
Console.WriteLine("Press any key to continue...");
ConsoleKeyInfo key = Console.ReadKey(intercept: true);
Console.WriteLine($"\nYou pressed: {key.Key}");
```

**Command-line arguments:**

```csharp
// Program.cs with args
if (args.Length > 0)
{
    Console.WriteLine($"Arguments received: {string.Join(", ", args)}");
}
else
{
    Console.WriteLine("No arguments provided.");
}
```

Run with arguments:

```bash
dotnet run -- arg1 arg2 arg3
```

---

## Quiz

### Question 1
Which type should you use for monetary calculations?
- float
- double
- decimal
- int

**Correct: 2**
**Explanation:** The decimal type has 128-bit precision and avoids floating-point rounding errors, making it ideal for financial calculations.

### Question 2
What does the 'var' keyword do?
- Declares a dynamic variable
- Lets the compiler infer the type
- Creates a reference type
- Makes the variable nullable

**Correct: 1**
**Explanation:** The 'var' keyword enables type inference - the compiler determines the type based on the assigned value.

### Question 3
What is the output of: int[] nums = [1, 2, 3]; Console.WriteLine(nums.Length);
- [1, 2, 3]
- 3
- 1, 2, 3
- Error

**Correct: 1**
**Explanation:** The Length property returns the number of elements in the array, which is 3.
