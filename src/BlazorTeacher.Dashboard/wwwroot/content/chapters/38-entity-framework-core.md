---
id: 38
number: 38
title: "Working with Data Using Entity Framework Core"
description: Learn modern database development with EF Core, including setup, modeling, querying, and migrations.
category: DataAccess
topics:
  - Understanding databases
  - EF Core setup
  - Defining models
  - Querying data
  - Migrations
  - Best practices
---

## Step 1: Understanding Modern Databases
**Type: Read**

Entity Framework Core (EF Core) is .NET's modern ORM (Object-Relational Mapper):

**Benefits of EF Core:**
- Write C# instead of SQL
- Type-safe queries with LINQ
- Automatic change tracking
- Database provider abstraction
- Migration support

**Supported Databases:**
- SQL Server
- PostgreSQL
- SQLite
- MySQL
- Cosmos DB
- In-memory (for testing)

```csharp
// The power of EF Core - intuitive queries
var activeUsers = await context.Users
    .Where(u => u.IsActive)
    .OrderBy(u => u.Name)
    .Take(10)
    .ToListAsync();
```

---

## Step 2: Setting Up EF Core
**Type: Action**

Install and configure EF Core:

```bash
# Add EF Core packages
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design

# Add tools for migrations
dotnet tool install --global dotnet-ef
```

**Create the DbContext:**

```csharp
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }
    
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships, indexes, etc.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
```

**Register in Program.cs:**

```csharp
// For SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// For SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// For PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
```

---

## Step 3: Defining EF Core Models
**Type: Read**

Create entities and configure relationships:

```csharp
// Entity definitions
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Foreign key
    public int CategoryId { get; set; }
    
    // Navigation property
    public Category Category { get; set; } = null!;
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    
    // Collection navigation
    public ICollection<Product> Products { get; set; } = [];
}

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string CustomerEmail { get; set; } = "";
    public OrderStatus Status { get; set; }
    
    public ICollection<OrderItem> Items { get; set; } = [];
    
    public decimal Total => Items.Sum(i => i.Quantity * i.UnitPrice);
}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    
    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}
```

**Fluent API Configuration:**

```csharp
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(p => p.Price)
            .HasPrecision(18, 2);
        
        builder.HasIndex(p => p.Name);
        
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
```

---

## Step 4: Querying Data
**Type: Action**

Query your database using LINQ:

```csharp
public class ProductService
{
    private readonly AppDbContext _context;
    
    public ProductService(AppDbContext context) => _context = context;
    
    // Get all active products
    public async Task<List<Product>> GetActiveProductsAsync()
    {
        return await _context.Products
            .Where(p => p.IsActive)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }
    
    // Get product with category (eager loading)
    public async Task<Product?> GetProductWithCategoryAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    // Search products
    public async Task<List<Product>> SearchAsync(string term, decimal? maxPrice)
    {
        var query = _context.Products.AsQueryable();
        
        if (!string.IsNullOrEmpty(term))
        {
            query = query.Where(p => p.Name.Contains(term) || 
                                      p.Description.Contains(term));
        }
        
        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice.Value);
        }
        
        return await query.ToListAsync();
    }
    
    // Projection (select specific columns)
    public async Task<List<ProductDto>> GetProductDtosAsync()
    {
        return await _context.Products
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryName = p.Category.Name
            })
            .ToListAsync();
    }
    
    // Aggregate queries
    public async Task<ProductStats> GetStatsAsync()
    {
        return new ProductStats
        {
            TotalProducts = await _context.Products.CountAsync(),
            ActiveProducts = await _context.Products.CountAsync(p => p.IsActive),
            AveragePrice = await _context.Products.AverageAsync(p => p.Price),
            TotalValue = await _context.Products.SumAsync(p => p.Price * p.Stock)
        };
    }
}

public record ProductDto(int Id, string Name, decimal Price, string CategoryName);
public record ProductStats(int TotalProducts, int ActiveProducts, decimal AveragePrice, decimal TotalValue);
```

---

## Step 5: CRUD Operations
**Type: Action**

Create, Update, and Delete data:

```csharp
public class ProductRepository
{
    private readonly AppDbContext _context;
    
    public ProductRepository(AppDbContext context) => _context = context;
    
    // Create
    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
    
    // Update
    public async Task UpdateAsync(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    
    // Update specific properties
    public async Task UpdatePriceAsync(int id, decimal newPrice)
    {
        await _context.Products
            .Where(p => p.Id == id)
            .ExecuteUpdateAsync(setters => 
                setters.SetProperty(p => p.Price, newPrice));
    }
    
    // Delete
    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
    
    // Bulk delete
    public async Task DeleteInactiveAsync()
    {
        await _context.Products
            .Where(p => !p.IsActive)
            .ExecuteDeleteAsync();
    }
}
```

---

## Step 6: Migrations
**Type: Action**

Manage database schema changes:

```bash
# Create initial migration
dotnet ef migrations add InitialCreate

# Apply migrations
dotnet ef database update

# Create new migration after model changes
dotnet ef migrations add AddProductDescription

# Revert to specific migration
dotnet ef database update PreviousMigrationName

# Remove last migration (if not applied)
dotnet ef migrations remove

# Generate SQL script
dotnet ef migrations script
```

**Migration file example:**

```csharp
public partial class AddProductDescription : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Description",
            table: "Products",
            type: "TEXT",
            nullable: false,
            defaultValue: "");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Description",
            table: "Products");
    }
}
```

---

## Quiz

### Question 1
What is the purpose of Include() in EF Core?
- To filter results
- To eager load related entities
- To add new records
- To create indexes

**Correct: 1**
**Explanation:** Include() performs eager loading, fetching related entities in the same query to avoid N+1 query problems.

### Question 2
What command applies pending migrations to the database?
- dotnet ef migrations apply
- dotnet ef database update
- dotnet ef database migrate
- dotnet ef update database

**Correct: 1**
**Explanation:** The 'dotnet ef database update' command applies all pending migrations to update the database schema.

### Question 3
What method saves all changes tracked by the DbContext?
- DbContext.Commit()
- DbContext.Save()
- DbContext.SaveChangesAsync()
- DbContext.Persist()

**Correct: 2**
**Explanation:** SaveChangesAsync() persists all tracked changes to the database. It returns the number of affected rows.
