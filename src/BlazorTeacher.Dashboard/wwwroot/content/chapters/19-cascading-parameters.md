---
id: 19
number: 19
title: Cascading Parameters
description: Share state across the UI tree.
route: /tutorial/state
category: StateManagement
topics:
  - CascadingValue
  - CascadingParameter
  - Global shared context
  - Pros/cons vs DI
---

## Step 1: Wrap App
**Type: Action**

Wrap `App.razor` content in `<CascadingValue Value="...">`.

---

## Step 2: Consume Value
**Type: Action**

Use `[CascadingParameter]` in a child component.

---

## Step 3: Verify
**Type: Action**

Check if deep child receives the value.

---

## Quiz

### Question 1
What tag provides a cascading value?
- "<Provider>"
- "<CascadingValue>"
- "<Context>"
- "<Global>"

**Correct: 1**
**Explanation:** <CascadingValue> provides a value to all descendants.
