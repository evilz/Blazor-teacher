---
id: 14
number: 14
title: Lifecycle Methods
description: Master the lifecycle flow of Blazor components.
route: /lifecycle
category: Components
topics:
  - OnInitialized / OnInitializedAsync
  - OnParametersSet
  - OnAfterRender / OnAfterRenderAsync
  - When they run & common pitfalls
---

## Step 1: Override OnInitialized
**Type: Action**

Load data in `OnInitializedAsync`.

---

## Step 2: Log Lifecycle
**Type: Action**

Add `Console.WriteLine` to lifecycle methods.

---

## Step 3: Observe
**Type: Action**

Watch console when navigating.

---

## Quiz

### Question 1
Which method runs first?
- OnAfterRender
- OnParametersSet
- OnInitialized
- Dispose

**Correct: 2**
**Explanation:** OnInitialized runs first after the component is created.
