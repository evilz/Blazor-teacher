---
id: 15
number: 15
title: Component Rendering & State
description: Control rendering, refreshes, and state.
route: /tutorial/components
category: Components
topics:
  - Render rules
  - StateHasChanged()
  - Preventing unnecessary rerenders
  - Local vs global state
---

## Step 1: Manual Render
**Type: Action**

Call `StateHasChanged()` after async work.

---

## Step 2: ShouldRender
**Type: Read**

Override `ShouldRender` to optimize.

---

## Step 3: Test
**Type: Action**

Verify UI updates correctly.

---

## Quiz

### Question 1
What method forces a re-render?
- Render()
- Update()
- StateHasChanged()
- Refresh()

**Correct: 2**
**Explanation:** StateHasChanged() notifies the component that state has changed.
