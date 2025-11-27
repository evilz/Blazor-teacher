---
id: 5
number: 5
title: Domain Modeling
description: Define the business domain, entities, and DTOs.
route: /tutorial/api
category: ApiDevelopment
topics:
  - Entities with C# modern syntax
  - DTOs
  - Mapping principles
  - Validation basics
  - Separation of concerns (Domain vs API)
---

## Step 1: Create Entity
**Type: Action**

Create `Course.cs` in Domain project.

---

## Step 2: Define Properties
**Type: Action**

Add `Id`, `Title`, `Price` properties.

---

## Step 3: Create DTO
**Type: Action**

Create `CourseDto.cs` for API responses.

---

## Quiz

### Question 1
Where should entities live?
- Api
- Infrastructure
- Domain
- Web

**Correct: 2**
**Explanation:** Entities belong in the Domain.
