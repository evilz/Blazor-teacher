---
id: 39
number: 39
title: "Querying and Manipulating Data Using LINQ"
description: Master LINQ for querying collections and databases with powerful, expressive syntax.
category: DataAccess
topics:
  - LINQ expressions
  - Query vs method syntax
  - Sorting and filtering
  - Grouping and joining
  - LINQ with EF Core
  - Advanced LINQ patterns
---

## Step 1: Writing LINQ Expressions
**Type: Read**

LINQ (Language Integrated Query) provides a consistent way to query any data source:

**Two Syntaxes:**

```csharp
var products = new List<Product>
{
    new("Apple", 1.50m, "Fruit"),
    new("Banana", 0.75m, "Fruit"),
    new("Milk", 2.50m, "Dairy"),
    new("Cheese", 4.99m, "Dairy"),
    new("Bread", 2.00m, "Bakery")
};

// Method syntax (fluent)
var cheapFruit = products
    .Where(p => p.Category == "Fruit")
    .Where(p => p.Price < 1.00m)
    .OrderBy(p => p.Price)
    .Select(p => p.Name);

// Query syntax (SQL-like)
var cheapFruitQuery = from p in products
                      where p.Category == "Fruit"
                      where p.Price < 1.00m
                      orderby p.Price
                      select p.Name;

// Both produce the same result!
```

**Basic Operations:**

```csharp
// Filtering
var expensive = products.Where(p => p.Price > 2.00m);

// Projection
var names = products.Select(p => p.Name);
var projected = products.Select(p => new { p.Name, p.Price });

// Ordering
var byPrice = products.OrderBy(p => p.Price);
var byPriceDesc = products.OrderByDescending(p => p.Price);
var multiSort = products.OrderBy(p => p.Category).ThenBy(p => p.Name);

// Taking and skipping (pagination)
var firstThree = products.Take(3);
var skipFirst = products.Skip(2).Take(3);

// Distinct
var categories = products.Select(p => p.Category).Distinct();
```

---

## Step 2: Aggregation and Element Access
**Type: Read**

Extract information and access specific elements:

```csharp
var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Aggregations
int count = numbers.Count();                    // 10
int sum = numbers.Sum();                        // 55
double average = numbers.Average();             // 5.5
int max = numbers.Max();                        // 10
int min = numbers.Min();                        // 1

// With products
decimal totalValue = products.Sum(p => p.Price);
decimal maxPrice = products.Max(p => p.Price);
Product? cheapest = products.MinBy(p => p.Price);
Product? priciest = products.MaxBy(p => p.Price);

// Element access
var first = products.First();                   // Error if empty
var firstOrNull = products.FirstOrDefault();    // null if empty
var firstFruit = products.First(p => p.Category == "Fruit");

var single = products.Single(p => p.Name == "Apple");     // Error if not exactly 1
var singleOrNull = products.SingleOrDefault(p => p.Name == "Unknown");

var last = products.Last();
var elementAt = products.ElementAt(2);

// Existence checks
bool anyFruit = products.Any(p => p.Category == "Fruit");   // true
bool allCheap = products.All(p => p.Price < 10);            // true
bool containsApple = products.Any(p => p.Name == "Apple");  // true
```

---

## Step 3: Grouping Data
**Type: Read**

Organize data into groups:

```csharp
// Basic grouping
var byCategory = products.GroupBy(p => p.Category);

foreach (var group in byCategory)
{
    Console.WriteLine($"Category: {group.Key}");
    foreach (var product in group)
    {
        Console.WriteLine($"  - {product.Name}: ${product.Price}");
    }
}

// Grouping with projection
var categorySummary = products
    .GroupBy(p => p.Category)
    .Select(g => new
    {
        Category = g.Key,
        Count = g.Count(),
        TotalValue = g.Sum(p => p.Price),
        AveragePrice = g.Average(p => p.Price)
    });

// Group by multiple keys
var orders = GetOrders();
var byCustomerAndMonth = orders
    .GroupBy(o => new { o.CustomerId, Month = o.OrderDate.Month })
    .Select(g => new
    {
        g.Key.CustomerId,
        g.Key.Month,
        OrderCount = g.Count(),
        Total = g.Sum(o => o.Total)
    });

// Query syntax grouping
var grouped = from p in products
              group p by p.Category into g
              select new
              {
                  Category = g.Key,
                  Products = g.ToList()
              };
```

---

## Step 4: Joining Data
**Type: Read**

Combine data from multiple sources:

```csharp
var categories = new List<Category>
{
    new(1, "Fruit"),
    new(2, "Dairy"),
    new(3, "Bakery")
};

var products2 = new List<ProductItem>
{
    new("Apple", 1, 1.50m),
    new("Banana", 1, 0.75m),
    new("Milk", 2, 2.50m),
    new("Cheese", 2, 4.99m)
};

// Inner join (only matching items)
var joined = products2.Join(
    categories,
    p => p.CategoryId,
    c => c.Id,
    (p, c) => new { p.Name, p.Price, Category = c.Name }
);

// Query syntax join
var joinedQuery = from p in products2
                  join c in categories on p.CategoryId equals c.Id
                  select new { p.Name, p.Price, Category = c.Name };

// Left outer join
var leftJoin = from c in categories
               join p in products2 on c.Id equals p.CategoryId into productsGroup
               from p in productsGroup.DefaultIfEmpty()
               select new
               {
                   Category = c.Name,
                   Product = p?.Name ?? "No products"
               };

// Group join
var categoryWithProducts = categories
    .GroupJoin(
        products2,
        c => c.Id,
        p => p.CategoryId,
        (category, prods) => new
        {
            Category = category.Name,
            Products = prods.ToList(),
            ProductCount = prods.Count()
        }
    );

record Category(int Id, string Name);
record ProductItem(string Name, int CategoryId, decimal Price);
```

---

## Step 5: LINQ with Entity Framework Core
**Type: Action**

LINQ translates to efficient SQL queries:

```csharp
public class OrderService
{
    private readonly AppDbContext _context;
    
    public OrderService(AppDbContext context) => _context = context;
    
    // Complex query - translated to SQL
    public async Task<List<OrderSummary>> GetOrderSummariesAsync(DateTime startDate)
    {
        return await _context.Orders
            .Where(o => o.OrderDate >= startDate)
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .Select(o => new OrderSummary
            {
                OrderId = o.Id,
                CustomerEmail = o.CustomerEmail,
                OrderDate = o.OrderDate,
                ItemCount = o.Items.Count,
                Total = o.Items.Sum(i => i.Quantity * i.UnitPrice),
                Products = o.Items.Select(i => i.Product.Name).ToList()
            })
            .OrderByDescending(o => o.Total)
            .ToListAsync();
    }
    
    // Grouping in EF Core
    public async Task<List<MonthlySales>> GetMonthlySalesAsync(int year)
    {
        return await _context.Orders
            .Where(o => o.OrderDate.Year == year)
            .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
            .Select(g => new MonthlySales
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                OrderCount = g.Count(),
                TotalSales = g.SelectMany(o => o.Items).Sum(i => i.Quantity * i.UnitPrice)
            })
            .OrderBy(m => m.Month)
            .ToListAsync();
    }
    
    // Pagination
    public async Task<PagedResult<Order>> GetOrdersPagedAsync(int page, int pageSize)
    {
        var query = _context.Orders
            .OrderByDescending(o => o.OrderDate);
        
        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new PagedResult<Order>(items, totalCount, page, pageSize);
    }
}

public record OrderSummary(int OrderId, string CustomerEmail, DateTime OrderDate, 
    int ItemCount, decimal Total, List<string> Products);
public record MonthlySales(int Year, int Month, int OrderCount, decimal TotalSales);
public record PagedResult<T>(List<T> Items, int TotalCount, int Page, int PageSize);
```

---

## Step 6: Advanced LINQ Patterns
**Type: Read**

Powerful techniques for complex scenarios:

```csharp
// Lazy vs eager execution
var lazyQuery = products.Where(p => p.Price > 1);  // Not executed yet
var eagerResult = products.Where(p => p.Price > 1).ToList();  // Executed now!

// AsEnumerable vs AsQueryable
var dbProducts = context.Products
    .Where(p => p.IsActive)     // Runs in database
    .AsEnumerable()             // Switch to client-side
    .Where(p => CustomLogic(p)); // Runs in memory

// Conditional queries (building dynamic queries)
public IQueryable<Product> Search(string? name, decimal? minPrice, string? category)
{
    var query = context.Products.AsQueryable();
    
    if (!string.IsNullOrEmpty(name))
        query = query.Where(p => p.Name.Contains(name));
    
    if (minPrice.HasValue)
        query = query.Where(p => p.Price >= minPrice.Value);
    
    if (!string.IsNullOrEmpty(category))
        query = query.Where(p => p.Category.Name == category);
    
    return query;
}

// SelectMany (flattening)
var allItems = orders.SelectMany(o => o.Items);

// Let clause (intermediate calculations)
var withDiscount = from p in products
                   let discountedPrice = p.Price * 0.9m
                   where discountedPrice < 2.00m
                   select new { p.Name, Original = p.Price, Discounted = discountedPrice };

// Zip (combine two sequences)
var names2 = new[] { "Alice", "Bob", "Charlie" };
var scores = new[] { 90, 85, 95 };
var results = names2.Zip(scores, (name, score) => $"{name}: {score}");

// Chunk (C# 12+)
var batches = products.Chunk(2);  // Groups of 2
foreach (var batch in batches)
{
    ProcessBatch(batch);
}
```

---

## Quiz

### Question 1
What is the difference between First() and Single()?
- No difference
- First() returns the first element; Single() throws if there's not exactly one
- Single() is faster
- First() only works with arrays

**Correct: 1**
**Explanation:** First() returns the first element (throws if empty). Single() returns the only element and throws if there are zero or more than one.

### Question 2
When is a LINQ query executed?
- Immediately when defined
- When ToList(), ToArray(), or iteration occurs
- Only when calling Execute()
- At the end of the method

**Correct: 1**
**Explanation:** LINQ uses deferred execution - queries are only executed when results are enumerated (ToList, foreach, etc.).

### Question 3
What does GroupJoin produce?
- A flat list of matches
- Groups where each item has a collection of matching items
- Just the count of matches
- Only non-matching items

**Correct: 1**
**Explanation:** GroupJoin creates a hierarchical result where each element from the first sequence has a collection of all matching elements from the second sequence.
