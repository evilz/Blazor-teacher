---
id: 35
number: 35
title: "Packaging and Distributing .NET Types"
description: Learn about .NET components, publishing applications, Native AOT compilation, and NuGet package distribution.
category: CSharpTypes
topics:
  - Understanding .NET components
  - Publishing applications
  - Native AOT compilation
  - NuGet package creation
  - Package distribution
---

## Step 1: Understanding .NET Components
**Type: Read**

.NET applications are composed of various components:

**Assemblies:**

An assembly is the building block of .NET applications:

```csharp
// Get current assembly info
var assembly = System.Reflection.Assembly.GetExecutingAssembly();
Console.WriteLine($"Name: {assembly.GetName().Name}");
Console.WriteLine($"Version: {assembly.GetName().Version}");

// AssemblyInfo (typically in .csproj)
```

```xml
<!-- In .csproj -->
<PropertyGroup>
    <AssemblyName>MyApp</AssemblyName>
    <Version>1.0.0</Version>
    <Authors>Your Name</Authors>
    <Company>Your Company</Company>
    <Description>My awesome application</Description>
</PropertyGroup>
```

**NuGet Packages:**

NuGet is .NET's package manager:

```bash
# Add a package
dotnet add package Newtonsoft.Json

# List packages
dotnet list package

# Remove a package
dotnet remove package Newtonsoft.Json

# Restore packages
dotnet restore
```

---

## Step 2: Publishing Your Application
**Type: Action**

Publish your application for deployment:

**Framework-Dependent Deployment:**

```bash
# Basic publish (requires .NET runtime on target)
dotnet publish -c Release

# For specific runtime
dotnet publish -c Release -r win-x64
dotnet publish -c Release -r linux-x64
dotnet publish -c Release -r osx-x64
```

**Self-Contained Deployment:**

```bash
# Include .NET runtime (no runtime needed on target)
dotnet publish -c Release -r win-x64 --self-contained true

# Trimmed (smaller size)
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishTrimmed=true
```

**Single File Deployment:**

```bash
# All in one executable
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

Project configuration:

```xml
<PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
    <PublishSingleFile>true</PublishSingleFile>
    <SelfContained>true</SelfContained>
    <PublishTrimmed>true</PublishTrimmed>
</PropertyGroup>
```

---

## Step 3: Native AOT Compilation
**Type: Read**

Native AOT (Ahead-of-Time) compilation produces native executables:

```xml
<!-- Enable Native AOT in .csproj -->
<PropertyGroup>
    <PublishAot>true</PublishAot>
</PropertyGroup>
```

```bash
# Publish with Native AOT
dotnet publish -c Release -r win-x64
```

**Benefits:**
- Faster startup time
- Smaller memory footprint
- No JIT compilation at runtime
- No .NET runtime required

**Limitations:**
- No runtime code generation
- Some reflection limitations
- Platform-specific builds required

**AOT-Compatible Code:**

```csharp
// Use source generators instead of reflection
[JsonSerializable(typeof(Person))]
public partial class AppJsonContext : JsonSerializerContext { }

// Usage
var json = JsonSerializer.Serialize(person, AppJsonContext.Default.Person);
var person = JsonSerializer.Deserialize<Person>(json, AppJsonContext.Default.Person);
```

---

## Step 4: Creating NuGet Packages
**Type: Action**

Share your libraries via NuGet:

**Configure Package Metadata:**

```xml
<!-- In your .csproj -->
<PropertyGroup>
    <PackageId>MyCompany.MyLibrary</PackageId>
    <Version>1.0.0</Version>
    <Authors>Your Name</Authors>
    <Company>Your Company</Company>
    <Description>A useful library for doing things</Description>
    <PackageTags>utility;helper;tools</PackageTags>
    <PackageLicenseExpression>MIT</PackageLicenseExpression>
    <PackageProjectUrl>https://github.com/you/mylib</PackageProjectUrl>
    <RepositoryUrl>https://github.com/you/mylib</RepositoryUrl>
    <PackageReadmeFile>README.md</PackageReadmeFile>
</PropertyGroup>

<ItemGroup>
    <None Include="README.md" Pack="true" PackagePath="" />
</ItemGroup>
```

**Build the Package:**

```bash
# Create the package
dotnet pack -c Release

# Package will be in bin/Release/MyCompany.MyLibrary.1.0.0.nupkg
```

**Package Structure:**

```treeview
MyCompany.MyLibrary.1.0.0.nupkg
├── MyCompany.MyLibrary.nuspec
├── lib/
│   └── net10.0/
│       └── MyCompany.MyLibrary.dll
├── README.md
└── [icon.png]
```

---

## Step 5: Publishing to NuGet.org
**Type: Action**

Distribute your package to the world:

**1. Create a NuGet.org Account:**
- Go to [nuget.org](https://www.nuget.org)
- Sign up and get an API key

**2. Push Your Package:**

```bash
# Push to NuGet.org
dotnet nuget push MyCompany.MyLibrary.1.0.0.nupkg \
    --api-key YOUR_API_KEY \
    --source https://api.nuget.org/v3/index.json

# Push to private feed
dotnet nuget push MyPackage.nupkg \
    --source https://mycompany.pkgs.visualstudio.com/_packaging/MyFeed/nuget/v3/index.json \
    --api-key az
```

**Local Package Source:**

```bash
# Add local folder as package source
dotnet nuget add source /path/to/packages --name LocalPackages

# Or in NuGet.config
```

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <packageSources>
        <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
        <add key="LocalPackages" value="/path/to/packages" />
    </packageSources>
</configuration>
```

---

## Step 6: Versioning Best Practices
**Type: Read**

Follow Semantic Versioning (SemVer):

**Version Format: MAJOR.MINOR.PATCH**

- **MAJOR**: Breaking changes
- **MINOR**: New features (backward compatible)
- **PATCH**: Bug fixes (backward compatible)

```xml
<PropertyGroup>
    <!-- Stable release -->
    <Version>1.0.0</Version>
    
    <!-- Pre-release -->
    <Version>1.0.0-beta.1</Version>
    <Version>1.0.0-rc.1</Version>
    
    <!-- With build number (CI/CD) -->
    <Version>1.0.0+build.123</Version>
</PropertyGroup>
```

**CI/CD Integration:**

```yaml
# GitHub Actions example
- name: Build and Pack
  run: |
    dotnet build -c Release
    dotnet pack -c Release -p:Version=${{ github.ref_name }}
    
- name: Publish to NuGet
  run: dotnet nuget push **/*.nupkg --source nuget.org --api-key ${{ secrets.NUGET_API_KEY }}
```

---

## Quiz

### Question 1
What does Native AOT compilation produce?
- Bytecode for the JIT compiler
- Native machine code executable
- JavaScript for web browsers
- Docker containers

**Correct: 1**
**Explanation:** Native AOT (Ahead-of-Time) compilation produces native machine code executables that don't require the .NET runtime.

### Question 2
What command creates a NuGet package?
- dotnet build --pack
- dotnet nuget create
- dotnet pack
- dotnet publish --nuget

**Correct: 2**
**Explanation:** The 'dotnet pack' command creates a NuGet package (.nupkg file) from your project.

### Question 3
In SemVer, what does incrementing the MAJOR version indicate?
- Bug fixes only
- New features, backward compatible
- Breaking changes
- Security patches

**Correct: 2**
**Explanation:** In Semantic Versioning, incrementing the MAJOR version (1.0.0 → 2.0.0) indicates breaking changes that are not backward compatible.
