---
id: 31
number: 31
title: Single Source of Truth
description: Implement proper state management in Blazor.
route: /tutorial/state
category: StateManagement
topics:
  - Avoiding duplicated state
  - Centralized data services
  - Observables and notification patterns
  - UI state vs domain state
---

## Step 1: Create State Service
**Type: Action**

Create `AppState.cs` with an event.

---

## Step 2: Inject Service
**Type: Action**

Register as Scoped in `Program.cs`.

---

## Step 3: Subscribe
**Type: Action**

Components subscribe to `OnChange` event.

---

## Quiz

### Question 1
What lifetime should a state service usually have?
- Transient
- Scoped
- Singleton
- Static

**Correct: 1**
**Explanation:** Scoped is per-user-session in Blazor Server.
