---
id: 29
number: 29
title: Parameters
description: Use parameters to customize components.
route: /tutorial/state
category: StateManagement
topics:
  - Required parameters
  - Optional parameters
  - Passing complex types
  - Parameter binding pitfalls
---

## Step 1: Add Parameter
**Type: Action**

Add `[Parameter] public string Text { get; set; }`.

---

## Step 2: Make Required
**Type: Action**

Add `[EditorRequired]` attribute.

---

## Step 3: Pass Object
**Type: Action**

Pass a `Course` object as a parameter.

---

## Quiz

### Question 1
What attribute enforces a parameter is set?
- "[Required]"
- "[EditorRequired]"
- "[Mandatory]"
- "[NotNull]"

**Correct: 1**
**Explanation:** [EditorRequired] warns if a parameter is missing at design time.
