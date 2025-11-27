---
id: 17
number: 17
title: EF Core Setup (Infrastructure Layer)
description: Set up EF Core Code First with SQLite.
route: /tutorial/api
category: ApiDevelopment
topics:
  - DbContext
  - Entity configuration
  - Migrations
  - Database creation
  - Connection strings
  - Dependency injection
---

## Step 1: Install EF Core
**Type: Action**

Add `Microsoft.EntityFrameworkCore.Sqlite` package.

---

## Step 2: Create DbContext
**Type: Action**

Inherit from `DbContext` and add `DbSet<Course>`.

---

## Step 3: Add Migration
**Type: Action**

Run `dotnet ef migrations add InitialCreate`.

---

## Quiz

### Question 1
What tool manages DB schema changes?
- Git
- Migrations
- Docker
- NPM

**Correct: 1**
**Explanation:** EF Core Migrations manage schema changes.
