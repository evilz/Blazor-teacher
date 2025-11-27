---
id: 34
number: 34
title: "Implementing Interfaces and Inheriting Classes"
description: Master inheritance, interfaces, generics, events, and null handling in C# 14.
category: CSharpTypes
topics:
  - Static methods and operator overloading
  - Generics
  - Events and delegates
  - Interfaces
  - Null handling
  - Inheritance hierarchies
  - Extending .NET types
---

## Step 1: Static Methods and Operator Overloading
**Type: Read**

Static members belong to the type itself, not instances:

```csharp
public class MathHelper
{
    // Static field
    public static readonly double Pi = 3.14159265359;
    
    // Static method
    public static double CircleArea(double radius) => Pi * radius * radius;
    
    // Static property
    public static int CalculationCount { get; private set; }
}

// Usage - no instance needed
double area = MathHelper.CircleArea(5);
Console.WriteLine(MathHelper.Pi);
```

**Operator Overloading:**

```csharp
public readonly struct Money
{
    public decimal Amount { get; }
    public string Currency { get; }
    
    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }
    
    // Operator overloading
    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new InvalidOperationException("Cannot add different currencies");
        return new Money(a.Amount + b.Amount, a.Currency);
    }
    
    public static Money operator -(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new InvalidOperationException("Cannot subtract different currencies");
        return new Money(a.Amount - b.Amount, a.Currency);
    }
    
    public static bool operator ==(Money a, Money b) 
        => a.Amount == b.Amount && a.Currency == b.Currency;
    
    public static bool operator !=(Money a, Money b) => !(a == b);
    
    public override string ToString() => $"{Currency} {Amount:F2}";
}

// Usage
var price1 = new Money(100, "USD");
var price2 = new Money(50, "USD");
var total = price1 + price2;  // USD 150.00
```

---

## Step 2: Generics
**Type: Read**

Generics allow type-safe, reusable code:

```csharp
// Generic class
public class Repository<T> where T : class
{
    private readonly List<T> _items = new();
    
    public void Add(T item) => _items.Add(item);
    public T? GetById(Func<T, bool> predicate) => _items.FirstOrDefault(predicate);
    public IEnumerable<T> GetAll() => _items;
}

// Generic method
public static T Max<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) > 0 ? a : b;
}

// Multiple type parameters
public class KeyValueStore<TKey, TValue> where TKey : notnull
{
    private readonly Dictionary<TKey, TValue> _store = new();
    
    public void Set(TKey key, TValue value) => _store[key] = value;
    public TValue? Get(TKey key) => _store.GetValueOrDefault(key);
}

// Generic constraints
public class Service<T> where T : class, IEntity, new()
{
    public T CreateNew() => new T();
}

public interface IEntity
{
    int Id { get; set; }
}
```

---

## Step 3: Events and Delegates
**Type: Read**

Events enable loose coupling through the observer pattern:

```csharp
// Delegate definition
public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);

// Using built-in EventHandler
public class StockPrice
{
    private decimal _price;
    
    // Event declaration
    public event EventHandler<PriceChangedEventArgs>? PriceChanged;
    
    public decimal Price
    {
        get => _price;
        set
        {
            if (_price != value)
            {
                var oldPrice = _price;
                _price = value;
                OnPriceChanged(oldPrice, value);
            }
        }
    }
    
    protected virtual void OnPriceChanged(decimal oldPrice, decimal newPrice)
    {
        PriceChanged?.Invoke(this, new PriceChangedEventArgs(oldPrice, newPrice));
    }
}

public class PriceChangedEventArgs : EventArgs
{
    public decimal OldPrice { get; }
    public decimal NewPrice { get; }
    public decimal Change => NewPrice - OldPrice;
    
    public PriceChangedEventArgs(decimal oldPrice, decimal newPrice)
    {
        OldPrice = oldPrice;
        NewPrice = newPrice;
    }
}

// Subscribing to events
var stock = new StockPrice { Price = 100 };
stock.PriceChanged += (sender, e) => 
    Console.WriteLine($"Price changed from {e.OldPrice} to {e.NewPrice}");

stock.Price = 105;  // Triggers event
```

---

## Step 4: Interfaces
**Type: Read**

Interfaces define contracts that types must implement:

```csharp
// Interface definition
public interface IRepository<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}

// Default interface methods (C# 8+)
public interface ILogger
{
    void Log(string message);
    
    // Default implementation
    void LogError(string message) => Log($"ERROR: {message}");
    void LogWarning(string message) => Log($"WARNING: {message}");
}

// Implementing interfaces
public class ProductRepository : IRepository<Product>
{
    private readonly AppDbContext _context;
    
    public ProductRepository(AppDbContext context) => _context = context;
    
    public async Task<Product?> GetByIdAsync(int id) 
        => await _context.Products.FindAsync(id);
        
    public async Task<IEnumerable<Product>> GetAllAsync() 
        => await _context.Products.ToListAsync();
        
    public async Task AddAsync(Product entity) 
        => await _context.Products.AddAsync(entity);
        
    public async Task UpdateAsync(Product entity) 
        => _context.Products.Update(entity);
        
    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null) _context.Products.Remove(entity);
    }
}

// Multiple interfaces
public class ConsoleLogger : ILogger, IDisposable
{
    public void Log(string message) => Console.WriteLine(message);
    public void Dispose() => Console.WriteLine("Logger disposed");
}
```

---

## Step 5: Working with Null
**Type: Read**

C# has excellent null handling features:

```csharp
// Nullable reference types (enabled in project)
#nullable enable

string name = "Alice";        // Cannot be null
string? nickname = null;      // Can be null

// Null-conditional operator
int? length = nickname?.Length;  // null if nickname is null

// Null-coalescing operator
string displayName = nickname ?? "No nickname";

// Null-coalescing assignment
nickname ??= "Default";  // Assign if null

// Null-forgiving operator (when you know better)
string definitelyNotNull = nickname!;  // Suppress warning

// Pattern matching with null
string GetDisplayName(Person? person) => person switch
{
    null => "Unknown",
    { Name: "" } => "Unnamed",
    { Name: var n } => n
};

// Guard clauses
public void Process(Order? order)
{
    ArgumentNullException.ThrowIfNull(order);
    // order is definitely not null here
}
```

---

## Step 6: Inheritance
**Type: Read**

Inheritance enables code reuse and polymorphism:

```csharp
// Base class
public abstract class Shape
{
    public string Name { get; init; }
    public ConsoleColor Color { get; set; }
    
    protected Shape(string name) => Name = name;
    
    // Abstract method - must be implemented
    public abstract double Area { get; }
    
    // Virtual method - can be overridden
    public virtual void Draw()
    {
        Console.ForegroundColor = Color;
        Console.WriteLine($"Drawing {Name}");
        Console.ResetColor();
    }
}

// Derived classes
public class Circle : Shape
{
    public double Radius { get; init; }
    
    public Circle(double radius) : base("Circle") => Radius = radius;
    
    public override double Area => Math.PI * Radius * Radius;
    
    public override void Draw()
    {
        base.Draw();
        Console.WriteLine($"  Radius: {Radius}, Area: {Area:F2}");
    }
}

public class Rectangle : Shape
{
    public double Width { get; init; }
    public double Height { get; init; }
    
    public Rectangle(double width, double height) : base("Rectangle")
    {
        Width = width;
        Height = height;
    }
    
    public override double Area => Width * Height;
}

// Sealed class - cannot be inherited
public sealed class Square : Rectangle
{
    public Square(double side) : base(side, side) { }
}

// Polymorphism in action
Shape[] shapes = [new Circle(5), new Rectangle(4, 3), new Square(2)];
foreach (var shape in shapes)
{
    Console.WriteLine($"{shape.Name}: Area = {shape.Area:F2}");
}
```

---

## Step 7: Extension Methods
**Type: Read**

Extend existing types without modifying them:

```csharp
public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? value) 
        => string.IsNullOrEmpty(value);
    
    public static string Truncate(this string value, int maxLength)
    {
        if (value.Length <= maxLength) return value;
        return value[..maxLength] + "...";
    }
    
    public static string ToTitleCase(this string value)
        => System.Globalization.CultureInfo.CurrentCulture
            .TextInfo.ToTitleCase(value.ToLower());
}

public static class EnumerableExtensions
{
    public static bool None<T>(this IEnumerable<T> source) 
        => !source.Any();
    
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) 
        where T : class
        => source.Where(x => x != null)!;
}

// Usage
string text = "hello world";
Console.WriteLine(text.ToTitleCase());  // "Hello World"

var names = new[] { "Alice", null, "Bob", null };
var validNames = names.WhereNotNull();  // ["Alice", "Bob"]
```

---

## Quiz

### Question 1
What is the purpose of the 'where' clause in generics?
- To filter collections
- To constrain type parameters
- To add conditions to queries
- To define method location

**Correct: 1**
**Explanation:** The 'where' clause in generics constrains type parameters, specifying requirements like class, struct, interface implementation, or constructor availability.

### Question 2
What does the '?.' operator do?
- Throws if null
- Returns null if left side is null
- Makes a type nullable
- Assigns if not null

**Correct: 1**
**Explanation:** The null-conditional operator (?.) safely navigates object references, returning null instead of throwing NullReferenceException if the left side is null.

### Question 3
Can you inherit from multiple classes in C#?
- Yes
- No, but you can implement multiple interfaces
- Only with special keywords
- Only in .NET 10

**Correct: 1**
**Explanation:** C# doesn't support multiple class inheritance to avoid complexity, but you can implement multiple interfaces to achieve similar flexibility.
