---
id: 17
number: 17
title: Component Communication
description: Make components talk to each other.
route: /tutorial/state
category: StateManagement
topics:
  - Parent → Child with [Parameter]
  - Child → Parent via EventCallback
  - Sibling communication patterns
---

## Step 1: Parent to Child
**Type: Action**

Pass data via `[Parameter]`.

---

## Step 2: Child to Parent
**Type: Action**

Invoke `EventCallback` from child.

---

## Step 3: Test
**Type: Action**

Verify data flows both ways.

---

## Quiz

### Question 1
How does a child notify a parent?
- Function Call
- EventCallback
- SignalR
- Global State

**Correct: 1**
**Explanation:** EventCallback allows child components to trigger events in the parent.
