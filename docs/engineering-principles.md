# XR Portfolio Suite – Engineering Principles (Unity 6.3 LTS)

This document defines the non-negotiable engineering standards for the XR portfolio suite.
These standards are applied consistently across all three applications:
MR Visual Symphony, Elemental Bending Sandbox, and MR Pocket Portals.

## 1. Design Goals

### 1.1 Portfolio Hiring Signals
- Systems thinking: modular architecture, clear boundaries, documented data flow.
- Product quality: onboarding, settings, error recovery, comfort, persistence.
- XR craft: high-quality interaction, stable visuals, MR compositing credibility.
- Performance engineering: budgets, profiling, and optimization with evidence.

### 1.2 Unity 6.3 LTS Constraints
- Maintain deterministic, testable service startup.
- Prefer package-based shared code (com.jalen.xrshared) over copy-pasted scripts.
- Use asmdefs for clean compilation boundaries and faster iteration.

## 2. Runtime Architecture Standards

### 2.1 Composition
- Each app must have:
  - Bootstrap scene (fast load, service init, config validation).
  - Experience scene (content and gameplay, loaded additively if needed).
- All services must be initialized in a deterministic order and must fail fast with actionable errors.

### 2.2 Services and Boundaries
Core interfaces (minimum):
- IInputRouter
- IAppStateMachine
- IPersistenceStore
- IPerformanceGovernor
- ICaptureController

MR-specific:
- IAnchorStore
- IOcclusionController
- IPassthroughController

Audio-specific:
- IAudioFeatures
- IAudioMappingGraph

Physics/gesture-specific:
- IGestureClassifier
- IFeedbackMixer

### 2.3 Update Discipline
- Avoid scattered Update loops.
- Prefer a centralized tick system:
  - Tick (per-frame)
  - FixedTick (physics)
  - LateTick (camera/visual settle)
- No allocations in steady-state per-frame code.

## 3. Performance and Memory Standards

### 3.1 Allocation Rules
- No GC allocations per-frame in steady state.
- Pool everything that spawns repeatedly:
  - particles
  - audio events
  - temporary UI elements
  - physics props

### 3.2 Rendering Rules
- Transparency and particle overdraw are budgeted and validated.
- Keep shader complexity intentionally constrained.
- Limit dynamic lights. Prefer emissive and baked-like lighting cues.

### 3.3 Worst-case Stress Tests
Each app must implement:
- A worst-case mode toggle that spawns the maximum expected load.
- A profiling overlay that reports:
  - FPS
  - CPU/GPU frame time (best-effort)
  - draw calls
  - active particles
  - active rigidbodies
  - allocations per frame
- A recorded capture of the app surviving worst-case mode at stable frame rate.

## 4. Interaction Quality Standards

### 4.1 Hand Interaction
- Every hand interaction must define:
  - activation affordance
  - cancellation behavior
  - fallback behavior
  - comfort range

### 4.2 UI Interaction
- Spatial UI must be readable and usable at typical MR/VR distances.
- Avoid micro text and dense panels.
- Inputs must work in imperfect tracking conditions.

### 4.3 Comfort and Safety
- No forced camera motion.
- Recenter and reset are always available.
- Use comfort defaults; allow advanced toggles.

## 5. Persistence Standards

### 5.1 Persistence Principles
- Persistence must be:
  - deterministic
  - versioned
  - forward-compatible when possible
- Missing anchors or broken content must fail gracefully:
  - offer rebind workflow
  - do not crash
  - do not corrupt saves

## 6. Observability Standards

### 6.1 Logging
- Logging categories:
  INPUT, XR, MR, AUDIO, PHYS, VFX, SAVE, UX, PERF
- Logs must be actionable. Avoid noise.

### 6.2 Debug Overlay
- Toggle with a dev gesture or keyboard in editor.
- Overlay is disabled in Release builds.

## 7. Documentation Standards
Each app must ship:
- README with quick start, feature list, and demo steps.
- Architecture doc with:
  - system diagram (textual)
  - data flow
  - key tradeoffs
  - performance notes
- QA checklist and known limitations.

## 8. Release Gate Rules
An app is "portfolio ready" only if:
- onboarding is complete and clean
- comfort and reset flows are robust
- performance budgets are met in worst-case mode
- visuals are stable with minimal artifacts
- video capture is repeatable and looks premium
