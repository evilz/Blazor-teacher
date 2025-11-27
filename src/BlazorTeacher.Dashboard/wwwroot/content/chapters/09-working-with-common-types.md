---
id: 9
number: 9
title: "Working with Common .NET Types"
description: Master working with numbers, text, regular expressions, and collections in .NET 10.
category: DataAccess
topics:
  - Working with numbers
  - String manipulation
  - Regular expressions
  - Collections
  - Dictionary and HashSet
  - LINQ basics
---

## Step 1: Working with Numbers
**Type: Read**

.NET provides powerful numeric types and operations:

**Numeric Types:**

```csharp
// Integer types
byte b = 255;           // 0-255
short s = 32_767;       // ±32K
int i = 2_147_483_647;  // ±2B
long l = 9_223_372_036_854_775_807L;  // Really big!

// Floating-point
float f = 3.14f;        // ~7 digits precision
double d = 3.14159265;  // ~15-16 digits
decimal m = 19.99m;     // 28-29 digits (for money!)

// Using underscores for readability
int million = 1_000_000;
long binary = 0b1010_1100_0011;
int hex = 0xFF_AA_BB;
```

**Math Operations:**

```csharp
// Math class
double sqrt = Math.Sqrt(16);        // 4
double power = Math.Pow(2, 10);     // 1024
double round = Math.Round(3.7);     // 4
double ceiling = Math.Ceiling(3.1); // 4
double floor = Math.Floor(3.9);     // 3
int abs = Math.Abs(-42);            // 42
int max = Math.Max(10, 20);         // 20
int min = Math.Min(10, 20);         // 10

// Random numbers
var random = new Random();
int dice = random.Next(1, 7);       // 1-6
double fraction = random.NextDouble();  // 0.0-1.0

// BigInteger for arbitrary precision
using System.Numerics;
BigInteger huge = BigInteger.Parse("123456789012345678901234567890");
```

---

## Step 2: Working with Text
**Type: Read**

Strings are fundamental in any application:

```csharp
// String creation
string greeting = "Hello, World!";
string multiline = """
    This is a raw string literal.
    It can span multiple lines.
    No need to escape "quotes"!
    """;

// String interpolation
string name = "Alice";
int age = 30;
string message = $"Hello, {name}! You are {age} years old.";

// String manipulation
string text = "  Hello, World!  ";
Console.WriteLine(text.Trim());           // "Hello, World!"
Console.WriteLine(text.ToUpper());        // "  HELLO, WORLD!  "
Console.WriteLine(text.ToLower());        // "  hello, world!  "
Console.WriteLine(text.Replace("World", "C#"));  // "  Hello, C#!  "

// Searching
bool contains = text.Contains("World");    // true
bool starts = text.StartsWith("  Hello");  // true
int index = text.IndexOf("World");         // 8

// Splitting and joining
string csv = "apple,banana,cherry";
string[] fruits = csv.Split(',');
string joined = string.Join(" - ", fruits);  // "apple - banana - cherry"

// StringBuilder for performance
var builder = new StringBuilder();
for (int i = 0; i < 1000; i++)
{
    builder.Append(i);
    builder.Append(", ");
}
string result = builder.ToString();
```

---

## Step 3: Regular Expressions
**Type: Read**

Pattern matching with regex:

```csharp
using System.Text.RegularExpressions;

// Simple matching
string email = "user@example.com";
bool isEmail = Regex.IsMatch(email, @"^\w+@\w+\.\w+$");

// Extract matches
string text = "Call 555-1234 or 555-5678";
var matches = Regex.Matches(text, @"\d{3}-\d{4}");
foreach (Match match in matches)
{
    Console.WriteLine(match.Value);  // 555-1234, 555-5678
}

// Replace with pattern
string cleaned = Regex.Replace("Hello123World456", @"\d+", "");  // "HelloWorld"

// Named groups
string pattern = @"(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2})";
var match = Regex.Match("2024-01-15", pattern);
if (match.Success)
{
    Console.WriteLine($"Year: {match.Groups["year"].Value}");
    Console.WriteLine($"Month: {match.Groups["month"].Value}");
}

// Compiled regex for performance (C# 7+)
[GeneratedRegex(@"\b\w+@\w+\.\w+\b")]
private static partial Regex EmailRegex();

// Usage
bool valid = EmailRegex().IsMatch("test@example.com");
```

---

## Step 4: Collections - Lists and Arrays
**Type: Read**

Store and manipulate multiple values:

```csharp
// List<T> - dynamic size
List<string> names = ["Alice", "Bob", "Charlie"];  // Collection expression
names.Add("David");
names.Remove("Bob");
names.Insert(0, "Anna");

// Common operations
Console.WriteLine(names.Count);           // 4
Console.WriteLine(names.Contains("Alice"));  // true
names.Sort();                              // Alphabetical order
names.Reverse();

// Searching
string? found = names.Find(n => n.StartsWith("A"));
List<string> filtered = names.FindAll(n => n.Length > 4);
int index = names.FindIndex(n => n == "Charlie");

// Arrays
int[] numbers = [1, 2, 3, 4, 5];
Array.Sort(numbers);
Array.Reverse(numbers);
int[] copy = new int[5];
Array.Copy(numbers, copy, 5);

// Span<T> for performance
Span<int> slice = numbers.AsSpan(1, 3);  // [2, 3, 4]
```

---

## Step 5: Dictionary and HashSet
**Type: Read**

Key-value pairs and unique collections:

```csharp
// Dictionary<TKey, TValue>
Dictionary<string, int> ages = new()
{
    ["Alice"] = 30,
    ["Bob"] = 25,
    ["Charlie"] = 35
};

// Access and modify
int aliceAge = ages["Alice"];
ages["David"] = 28;
ages.Remove("Bob");

// Safe access
if (ages.TryGetValue("Eve", out int eveAge))
{
    Console.WriteLine(eveAge);
}
else
{
    Console.WriteLine("Eve not found");
}

// Iterate
foreach (var (name, age) in ages)
{
    Console.WriteLine($"{name} is {age}");
}

// HashSet<T> - unique values only
HashSet<string> uniqueNames = ["Alice", "Bob", "Alice"];  // Only 2 items!
Console.WriteLine(uniqueNames.Count);  // 2

uniqueNames.Add("Charlie");
bool hasAlice = uniqueNames.Contains("Alice");

// Set operations
HashSet<int> set1 = [1, 2, 3, 4];
HashSet<int> set2 = [3, 4, 5, 6];

var union = set1.Union(set2);        // [1, 2, 3, 4, 5, 6]
var intersection = set1.Intersect(set2);  // [3, 4]
var difference = set1.Except(set2);  // [1, 2]
```

---

## Step 6: Introduction to LINQ
**Type: Read**

Query collections with Language Integrated Query:

```csharp
List<int> numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

// Method syntax
var evens = numbers.Where(n => n % 2 == 0);
var doubled = numbers.Select(n => n * 2);
var sum = numbers.Sum();
var average = numbers.Average();

// Query syntax
var evenQuery = from n in numbers
                where n % 2 == 0
                select n;

// Complex queries
var products = new List<Product>
{
    new("Apple", 1.50m, "Fruit"),
    new("Banana", 0.75m, "Fruit"),
    new("Milk", 2.50m, "Dairy"),
    new("Cheese", 4.99m, "Dairy")
};

var expensiveFruit = products
    .Where(p => p.Category == "Fruit")
    .Where(p => p.Price > 1.00m)
    .OrderBy(p => p.Price)
    .Select(p => new { p.Name, p.Price });

// Aggregation
var totalValue = products.Sum(p => p.Price);
var cheapest = products.MinBy(p => p.Price);
var mostExpensive = products.MaxBy(p => p.Price);
var categories = products.GroupBy(p => p.Category);

// First, Single, Any, All
var first = products.First();
var firstFruit = products.FirstOrDefault(p => p.Category == "Fruit");
bool anyExpensive = products.Any(p => p.Price > 10);
bool allAffordable = products.All(p => p.Price < 100);

record Product(string Name, decimal Price, string Category);
```

---

## Quiz

### Question 1
What type should you use for monetary calculations?
- double
- float
- decimal
- int

**Correct: 2**
**Explanation:** The decimal type provides the precision needed for financial calculations without the rounding errors of float/double.

### Question 2
What does HashSet<T> guarantee about its elements?
- They are sorted
- They are unique
- They are immutable
- They are thread-safe

**Correct: 1**
**Explanation:** HashSet<T> ensures all elements are unique - adding duplicates has no effect.

### Question 3
What LINQ method filters a collection based on a condition?
- Select
- Where
- Filter
- Find

**Correct: 1**
**Explanation:** The Where() method filters elements based on a predicate, similar to SQL's WHERE clause.
