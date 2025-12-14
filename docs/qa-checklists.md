# XR Portfolio Suite – QA Checklists and Release Gates

These checklists define the release gate for "portfolio-ready."

## 1) Global Checklist (All Apps)

### 1.1 First-Run and UX
- [ ] App loads to usable state in under 10 seconds on device.
- [ ] Onboarding completes in under 60 seconds with no confusion.
- [ ] Recenter action exists and works.
- [ ] Reset Experience exists and clears runtime state safely.
- [ ] Settings panel exists and is readable in-headset.

### 1.2 Interaction Quality
- [ ] Primary interactions are reliable under imperfect tracking.
- [ ] Interaction range is comfort tuned (no excessive reaching).
- [ ] Cancel behavior exists for all grab or placement actions.
- [ ] No frequent accidental activations.

### 1.3 Performance and Stability
- [ ] Worst-case mode exists and is accessible in DEV builds.
- [ ] Stable performance in worst-case mode for 60 seconds.
- [ ] No steady-state per-frame allocations.
- [ ] No memory growth over 10 minutes of typical use.
- [ ] No physics explosions or runaway effects.

### 1.4 Visual Stability
- [ ] No flicker, shimmer, or unstable transparency edges in key effects.
- [ ] Overdraw controlled in particle-heavy scenes.
- [ ] Lighting consistent and readable.

### 1.5 Portfolio Evidence
- [ ] 60 to 120 second trailer recorded using Capture Mode.
- [ ] 5 to 10 screenshots that demonstrate polish and scale.
- [ ] Architecture doc completed with data flow and tradeoffs.
- [ ] One profiling screenshot with interpretation.

## 2) App-Specific Gates

### 2.1 MR Visual Symphony
- [ ] Anchors: place 3 to 7 instruments reliably.
- [ ] Persistence: instruments restore across sessions.
- [ ] Audio analysis: stable, smooth, and responsive with no jitter.
- [ ] Occlusion: enabled when stable, fallback mode works.

### 2.2 Elemental Bending Sandbox
- [ ] Two elements implemented at premium quality in v1.
- [ ] Gesture classifier produces confidence score and rejects low confidence.
- [ ] Dojo drills provide measurable feedback.
- [ ] Physics stability validated under stress.

### 2.3 MR Pocket Portals
- [ ] Portal place, open, close, scale, move.
- [ ] Five curated universes load instantly and look distinct.
- [ ] Two portals supported concurrently with stable performance.
- [ ] Occlusion stable, fallback mode works.
