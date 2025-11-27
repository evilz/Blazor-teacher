---
id: 6
number: 6
title: "Building Your Own Types with Object-Oriented Programming"
description: Master OOP concepts in C# 14 including classes, records, properties, methods, tuples, and pattern matching.
category: CSharpTypes
topics:
  - Object-oriented programming concepts
  - Building class libraries
  - Fields and properties
  - Methods and tuples
  - Access modifiers
  - Pattern matching
  - Record types
---

## Step 1: Object-Oriented Programming Basics
**Type: Read**

C# is a fully object-oriented language. The four pillars of OOP are:

1. **Encapsulation** - Bundling data and methods, hiding internal details
2. **Abstraction** - Exposing only essential features
3. **Inheritance** - Creating new classes from existing ones
4. **Polymorphism** - Objects behaving differently based on their type

```csharp
// A simple class demonstrating encapsulation
public class BankAccount
{
    // Private field - hidden from outside
    private decimal _balance;
    
    // Public property - controlled access
    public decimal Balance => _balance;
    
    // Public method - the interface
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");
        _balance += amount;
    }
}
```

---

## Step 2: Building Class Libraries
**Type: Action**

Create reusable components in a class library:

```bash
# Create a class library
dotnet new classlib -n MyApp.Core

# Add to your solution
dotnet sln add MyApp.Core
```

**Organizing your types:**

```csharp
// Models/Product.cs
namespace MyApp.Core.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; } = true;
}

// Services/ProductService.cs
namespace MyApp.Core.Services;

public class ProductService
{
    private readonly List<Product> _products = new();
    
    public void Add(Product product) => _products.Add(product);
    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);
    public IEnumerable<Product> GetAll() => _products;
}
```

---

## Step 3: Fields and Properties
**Type: Read**

Control access to your data with fields and properties:

```csharp
public class Person
{
    // Private field (backing store)
    private string _name = string.Empty;
    
    // Full property with validation
    public string Name
    {
        get => _name;
        set => _name = value?.Trim() ?? throw new ArgumentNullException(nameof(value));
    }
    
    // Auto-implemented property
    public int Age { get; set; }
    
    // Read-only property
    public string Id { get; } = Guid.NewGuid().ToString();
    
    // Init-only property (C# 9+)
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    // Computed property
    public bool IsAdult => Age >= 18;
    
    // Required property (C# 11+)
    public required string Email { get; set; }
}

// Usage
var person = new Person
{
    Name = "Alice",
    Age = 25,
    Email = "alice@example.com"  // Required!
};
```

---

## Step 4: Methods and Tuples
**Type: Read**

Methods define behavior, and tuples allow returning multiple values:

```csharp
public class Calculator
{
    // Instance method
    public double Add(double a, double b) => a + b;
    
    // Static method
    public static double Multiply(double a, double b) => a * b;
    
    // Method returning tuple
    public (double sum, double product) Calculate(double a, double b)
    {
        return (a + b, a * b);
    }
    
    // Tuple with named elements
    public (int quotient, int remainder) Divide(int dividend, int divisor)
    {
        return (dividend / divisor, dividend % divisor);
    }
}

// Using tuples
var calc = new Calculator();

// Deconstruction
var (sum, product) = calc.Calculate(3, 4);
Console.WriteLine($"Sum: {sum}, Product: {product}");

// Named access
var divResult = calc.Divide(17, 5);
Console.WriteLine($"17 / 5 = {divResult.quotient} remainder {divResult.remainder}");

// Inline tuple
(string name, int age) person = ("Bob", 30);
Console.WriteLine($"{person.name} is {person.age}");
```

---

## Step 5: Access Modifiers
**Type: Read**

Control visibility with access modifiers:

```csharp
public class Example
{
    public string Public;           // Accessible from anywhere
    private string Private;         // Only within this class
    protected string Protected;     // This class and derived classes
    internal string Internal;       // Same assembly only
    protected internal string ProtectedInternal;  // Assembly OR derived
    private protected string PrivateProtected;    // Assembly AND derived
}

// Best practices
public class Order
{
    // Private fields
    private readonly List<OrderItem> _items = new();
    
    // Public properties for controlled access
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    
    // Public methods for operations
    public void AddItem(OrderItem item)
    {
        ValidateItem(item);  // Private helper
        _items.Add(item);
    }
    
    // Private helper method
    private void ValidateItem(OrderItem item)
    {
        if (item.Quantity <= 0)
            throw new ArgumentException("Quantity must be positive");
    }
}
```

---

## Step 6: Pattern Matching
**Type: Read**

Pattern matching makes type checking and value extraction elegant:

```csharp
// Type patterns
object obj = "Hello";

if (obj is string text)
{
    Console.WriteLine(text.ToUpper());
}

// Switch expression with patterns
string Describe(object obj) => obj switch
{
    null => "Nothing",
    int n when n < 0 => "Negative number",
    int n => $"Number: {n}",
    string s => $"Text: {s}",
    IEnumerable<int> list => $"List with {list.Count()} items",
    _ => "Unknown"
};

// Property patterns
record Point(int X, int Y);

string DescribePoint(Point p) => p switch
{
    { X: 0, Y: 0 } => "Origin",
    { X: 0 } => "On Y-axis",
    { Y: 0 } => "On X-axis",
    { X: var x, Y: var y } when x == y => "On diagonal",
    _ => $"Point at ({p.X}, {p.Y})"
};

// List patterns (C# 11+)
int[] numbers = [1, 2, 3, 4, 5];

var result = numbers switch
{
    [] => "Empty",
    [var single] => $"Single: {single}",
    [var first, .., var last] => $"First: {first}, Last: {last}",
};
```

---

## Step 7: Record Types
**Type: Read**

Records are immutable reference types perfect for data:

```csharp
// Record declaration (positional)
public record Person(string Name, int Age);

// Record with additional members
public record Product(int Id, string Name, decimal Price)
{
    public string FormattedPrice => $"${Price:F2}";
}

// Usage
var person1 = new Person("Alice", 30);
var person2 = person1 with { Age = 31 };  // Non-destructive mutation

Console.WriteLine(person1);  // Person { Name = Alice, Age = 30 }
Console.WriteLine(person1 == person2);  // False - value-based equality

// Deconstruction
var (name, age) = person1;

// Record struct (value type)
public record struct Coordinate(double X, double Y);

// Record class (explicit, same as record)
public record class Customer(int Id, string Email);
```

---

## Quiz

### Question 1
What is the purpose of the 'init' accessor?
- Makes property read-only after construction
- Initializes the property to a default value
- Makes the property required
- Prevents the property from being null

**Correct: 0**
**Explanation:** The 'init' accessor allows a property to be set only during object initialization (in constructor or object initializer), making it effectively read-only afterward.

### Question 2
Which access modifier is most restrictive?
- public
- internal
- protected
- private

**Correct: 3**
**Explanation:** 'private' is the most restrictive - members are only accessible within the same class.

### Question 3
What does the 'with' keyword do with records?
- Creates a copy with some properties changed
- Adds new properties to a record
- Combines two records
- Deletes a record

**Correct: 0**
**Explanation:** The 'with' expression creates a new record instance with specified properties modified (non-destructive mutation).
