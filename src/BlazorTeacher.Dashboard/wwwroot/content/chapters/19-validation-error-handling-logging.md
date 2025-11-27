---
id: 19
number: 19
title: Validation, Error Handling & Logging
description: Handle user input, errors, and observability.
route: /tutorial/api
category: ApiDevelopment
topics:
  - Validation using DataAnnotations
  - Exception handling middleware
  - Logging with ILogger
  - API responses conventions
  - Configuration using Options Pattern
---

## Step 1: Add Validation
**Type: Action**

Use `[Required]` on DTO properties.

---

## Step 2: Global Error Handling
**Type: Action**

Add middleware to catch exceptions.

---

## Step 3: Logging
**Type: Action**

Inject `ILogger` and log actions.

---

## Quiz

### Question 1
What attribute marks a field as mandatory?
- "[Key]"
- "[Required]"
- "[MaxLength]"
- "[Table]"

**Correct: 1**
**Explanation:** [Required] ensures the field is not null/empty.
