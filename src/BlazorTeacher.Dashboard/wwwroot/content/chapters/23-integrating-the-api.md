---
id: 23
number: 23
title: Integrating the API
description: Connect Blazor to the .NET 10 REST API.
route: /courses
category: Dashboard
topics:
  - HttpClient
  - Fetching async
  - Mapping API data to UI models
  - Error handling in UI
  - Retry and fallback patterns
---

## Step 1: Inject HttpClient
**Type: Action**

Inject `HttpClient` configured for API.

---

## Step 2: Fetch Data
**Type: Action**

Call `GetFromJsonAsync<List<CourseDto>>`.

---

## Step 3: Handle Errors
**Type: Action**

Wrap in try-catch and show error message.

---

## Quiz

### Question 1
What method fetches JSON data?
- GetAsync
- GetJsonAsync
- GetFromJsonAsync
- FetchJson

**Correct: 2**
**Explanation:** GetFromJsonAsync is the helper method to fetch and deserialize JSON.
