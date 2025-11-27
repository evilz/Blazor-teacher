---
id: 16
number: 16
title: Building Reusable Components
description: Design components to be reused across your dashboard.
route: /tutorial/components
category: Components
topics:
  - Component patterns
  - Partial UI reuse
  - Slots via RenderFragment
  - Parameterizing components
---

## Step 1: Create Card
**Type: Action**

Create a reusable `Card.razor`.

---

## Step 2: Add ChildContent
**Type: Action**

Add `[Parameter] public RenderFragment ChildContent { get; set; }`.

---

## Step 3: Use Card
**Type: Action**

Wrap content in `<Card>...</Card>`.

---

## Quiz

### Question 1
What type is used for child content?
- string
- html
- RenderFragment
- ComponentBase

**Correct: 2**
**Explanation:** RenderFragment represents a segment of UI content.
