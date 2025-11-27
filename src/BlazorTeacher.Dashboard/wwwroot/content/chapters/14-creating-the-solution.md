---
id: 14
number: 14
title: Creating the Solution (Project Template)
description: Build the initial folder structure and create the solution using clean architecture principles.
category: Setup
topics:
  - dotnet new sln
  - "Projects: Api (REST), Domain (Entities, contracts), Infrastructure (EF Core, DB), Web (Blazor dashboard)"
  - Reference linking across projects
---

## Step 1: Create Solution
**Type: Action**

Run `dotnet new sln -n BlazorTeacher` to create an empty solution file.

---

## Step 2: Create Projects
**Type: Action**

Create the projects:

1. `dotnet new classlib -n BlazorTeacher.Domain`
2. `dotnet new classlib -n BlazorTeacher.Infrastructure`
3. `dotnet new webapi -n BlazorTeacher.Api`
4. `dotnet new blazor -n BlazorTeacher.Web`

---

## Step 3: Link Projects
**Type: Read**

Add references:
- Api references Infrastructure & Domain
- Infrastructure references Domain
- Web references Domain (and Api client later)

---

## Quiz

### Question 1
Which project should have NO dependencies on others?
- Api
- Infrastructure
- Domain
- Web

**Correct: 2**
**Explanation:** The Domain layer is the core and should not depend on outer layers.
