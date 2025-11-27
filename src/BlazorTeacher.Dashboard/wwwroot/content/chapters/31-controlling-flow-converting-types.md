---
id: 31
number: 31
title: "Controlling Flow, Converting Types, and Handling Exceptions"
description: Master control flow statements, type conversions, arrays, and exception handling in C# 14.
category: CSharpFundamentals
topics:
  - Operating on variables
  - Selection statements (if, switch)
  - Iteration statements (for, foreach, while)
  - Working with arrays
  - Type casting and conversion
  - Exception handling
  - Overflow checking
---

## Step 1: Operators and Expressions
**Type: Read**

C# provides a rich set of operators for working with values:

**Arithmetic Operators:**

```csharp
int a = 10, b = 3;

Console.WriteLine(a + b);   // 13 - Addition
Console.WriteLine(a - b);   // 7  - Subtraction
Console.WriteLine(a * b);   // 30 - Multiplication
Console.WriteLine(a / b);   // 3  - Integer division
Console.WriteLine(a % b);   // 1  - Modulus (remainder)

// Increment/Decrement
int x = 5;
Console.WriteLine(++x);  // 6 - Pre-increment
Console.WriteLine(x++);  // 6 - Post-increment (x is now 7)
```

**Comparison and Logical Operators:**

```csharp
bool isEqual = (a == b);      // false
bool isGreater = (a > b);     // true
bool notEqual = (a != b);     // true

// Logical operators
bool result = (a > 5) && (b < 5);  // true - AND
bool either = (a > 5) || (b > 5);  // true - OR
bool negation = !(a == b);         // true - NOT

// Null-coalescing operators
string? name = null;
string displayName = name ?? "Unknown";           // "Unknown"
name ??= "Default";                                // Assign if null
```

---

## Step 2: Selection Statements
**Type: Read**

Use selection statements to make decisions in your code:

**If-Else Statements:**

```csharp
int score = 85;

if (score >= 90)
{
    Console.WriteLine("Grade: A");
}
else if (score >= 80)
{
    Console.WriteLine("Grade: B");
}
else if (score >= 70)
{
    Console.WriteLine("Grade: C");
}
else
{
    Console.WriteLine("Grade: F");
}

// Ternary operator for simple conditions
string result = score >= 60 ? "Pass" : "Fail";
```

**Switch Statements and Expressions:**

```csharp
// Traditional switch
int dayOfWeek = 3;
switch (dayOfWeek)
{
    case 1:
        Console.WriteLine("Monday");
        break;
    case 2:
        Console.WriteLine("Tuesday");
        break;
    case 3:
        Console.WriteLine("Wednesday");
        break;
    default:
        Console.WriteLine("Other day");
        break;
}

// Switch expression (modern C#)
string dayName = dayOfWeek switch
{
    1 => "Monday",
    2 => "Tuesday",
    3 => "Wednesday",
    4 => "Thursday",
    5 => "Friday",
    6 or 7 => "Weekend",
    _ => "Invalid"
};
```

---

## Step 3: Iteration Statements
**Type: Read**

Loop through code multiple times:

**For Loop:**

```csharp
// Standard for loop
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Iteration: {i}");
}

// Counting backwards
for (int i = 10; i >= 0; i--)
{
    Console.WriteLine(i);
}
```

**Foreach Loop:**

```csharp
string[] colors = ["Red", "Green", "Blue"];

foreach (string color in colors)
{
    Console.WriteLine(color);
}

// With index using LINQ
foreach (var (color, index) in colors.Select((c, i) => (c, i)))
{
    Console.WriteLine($"{index}: {color}");
}
```

**While and Do-While:**

```csharp
// While loop
int count = 0;
while (count < 5)
{
    Console.WriteLine(count);
    count++;
}

// Do-while (executes at least once)
do
{
    Console.Write("Enter 'quit' to exit: ");
    var input = Console.ReadLine();
    if (input == "quit") break;
} while (true);
```

---

## Step 4: Working with Arrays
**Type: Action**

Arrays store multiple values of the same type:

```csharp
// Array declaration and initialization
int[] numbers = [1, 2, 3, 4, 5];        // Collection expression
string[] names = new string[3];          // Fixed size, default values
double[] prices = new double[] { 9.99, 19.99, 29.99 };

// Accessing elements (0-based indexing)
Console.WriteLine(numbers[0]);  // 1 (first element)
Console.WriteLine(numbers[^1]); // 5 (last element using Index)

// Array slicing with Range
int[] subset = numbers[1..4];   // [2, 3, 4]
int[] lastTwo = numbers[^2..];  // [4, 5]

// Multi-dimensional arrays
int[,] matrix = new int[3, 3]
{
    { 1, 2, 3 },
    { 4, 5, 6 },
    { 7, 8, 9 }
};
Console.WriteLine(matrix[1, 1]); // 5 (center element)

// Jagged arrays (array of arrays)
int[][] jagged = 
[
    [1, 2],
    [3, 4, 5],
    [6, 7, 8, 9]
];
```

---

## Step 5: Type Conversions
**Type: Read**

Converting between types is common in C#:

**Implicit Conversion (safe, automatic):**

```csharp
int smallNumber = 100;
long bigNumber = smallNumber;    // int to long is safe
float fraction = smallNumber;    // int to float is safe
```

**Explicit Conversion (casting):**

```csharp
double pi = 3.14159;
int rounded = (int)pi;           // 3 - truncates decimal

long bigValue = 1_000_000_000_000L;
int smallValue = (int)bigValue;  // Warning: possible data loss!
```

**Parse and TryParse:**

```csharp
// Parse (throws exception on failure)
string numberText = "42";
int number = int.Parse(numberText);

// TryParse (safe, returns bool)
if (int.TryParse("123", out int result))
{
    Console.WriteLine($"Parsed: {result}");
}

// Convert class
double d = Convert.ToDouble("3.14");
int i = Convert.ToInt32(true);  // 1
```

---

## Step 6: Exception Handling
**Type: Read**

Handle runtime errors gracefully:

```csharp
try
{
    Console.Write("Enter a number: ");
    int number = int.Parse(Console.ReadLine()!);
    int result = 100 / number;
    Console.WriteLine($"Result: {result}");
}
catch (FormatException)
{
    Console.WriteLine("That's not a valid number!");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Cannot divide by zero!");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}
finally
{
    Console.WriteLine("Operation completed.");
}
```

**Checking for Overflow:**

```csharp
// Unchecked (default) - wraps around silently
int max = int.MaxValue;
int overflow = max + 1;  // Becomes int.MinValue!

// Checked - throws OverflowException
try
{
    int safeResult = checked(max + 1);
}
catch (OverflowException)
{
    Console.WriteLine("Integer overflow detected!");
}

// Checked block
checked
{
    int a = int.MaxValue;
    // Any overflow here throws an exception
}
```

---

## Quiz

### Question 1
What is the result of: 10 / 3 in C# (with int types)?
- 3.333
- 3
- 4
- Error

**Correct: 1**
**Explanation:** Integer division truncates the decimal portion, so 10 / 3 = 3.

### Question 2
Which keyword handles any exception type?
- catch (Error)
- catch (Exception)
- catch (Throwable)
- catch (All)

**Correct: 1**
**Explanation:** Exception is the base class for all exceptions in C#. catch(Exception) handles any exception type.

### Question 3
What does numbers[^1] access in an array?
- The first element
- The last element
- An error
- The length

**Correct: 1**
**Explanation:** The ^ operator accesses from the end. ^1 means the last element, ^2 is second-to-last, etc.
