# XR Portfolio Suite (Unity 6000.3.1f1)

A professional XR project suite targeting Meta Quest-class hardware with a shared engineering foundation and three distinct applications:

1) MR Visual Symphony – primary MR spatial audio-reactive experience  
2) Elemental Bending Sandbox – primary VR mastery-focused physics sandbox  
3) MR Pocket Portals – polished MR side project with curated miniature universes

## Repository Layout
- Apps/ contains three independent Unity projects
- Packages/com.jalen.xrshared contains shared runtime systems, editor tooling, tests, and samples
- docs/ contains architecture notes, performance budgets, capture guide, and QA checklists

## Build Philosophy
- performance budgets and stress tests are first-class features
- no steady-state per-frame allocations
- interaction quality and comfort defaults are non-negotiable
- each app is shippable and portfolio-ready with a repeatable demo script

## Getting Started
1. Install Unity 6.3 LTS (6000.3.1f1).
2. Open any app under Apps/.
3. Ensure Quest/Meta XR packages are installed per app.
4. Build to Quest and validate performance using the in-app overlay and stress mode.

See docs/ for engineering principles, budgets, and capture workflows.
