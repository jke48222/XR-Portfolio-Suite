# Elemental Bending Sandbox – Phase 1 Plan (Air Element)

## Purpose
This document defines the **engineering-executable Phase 1 plan** for the Elemental Bending Sandbox, focused on implementing **Air** as the first element.  
The goal is to validate the full embodied interaction loop with the lowest technical risk while establishing reusable systems for later elements.

This plan assumes:
- Meta Quest 3
- Unity 6.3 LTS (6000.3.1f1)
- VR-only (no passthrough)
- OpenXR + Meta XR SDK
- No XR Interaction Toolkit dependency

---

## Why Air Is First (Technical Rationale)

Air is the lowest-risk element that still exercises the full system:

- **Gesture clarity**: Continuous sweeping and pushing gestures map directly to velocity and direction vectors.
- **Physics simplicity**: Force-field–style interactions using Rigidbody forces avoid complex simulation.
- **Debuggability**: Direction, magnitude, and instability can be visualized directly.
- **Performance safety**: No fluid meshes, fracture, or heavy particle counts required.
- **Architecture reuse**: Gesture pipeline, state machine, and force application patterns carry forward to all elements.

Air is treated as a **system**, not a spell.

---

## Core Loop (Phase 1)

XR Input
→ Gesture Sampling & Filtering
→ GestureIntentData
→ Air Element State Machine
→ Force Field Parameters
→ Rigidbody Force Application
→ Visual Feedback


This loop runs continuously with no discrete “casts”.

---

## Gesture Pipeline

### Responsibilities
- Sample hand or controller poses every frame
- Derive velocities, directions, and coherence
- Output continuous intent data with stability metrics

### Inputs
- Controller or hand positions (world space)
- Linear velocity
- Head forward vector (body orientation proxy)

### Gesture Primitives (Air)
- Sweep direction (normalized velocity vector)
- Intensity (velocity magnitude → 0..1)
- Two-hand coherence (optional, Phase 2)
- Stability and confidence scores

### Output Structure
- `GestureIntentData`
  - PrimaryDirection
  - Intensity01
  - Stability01
  - Confidence01

### Update Strategy
- Sample in `Update`
- No allocations
- Buffer latest intent for physics

---

## Air Element State Machine

### States
- Idle
- Engaging
- Sustaining
- Releasing
- Unstable

### State Rules
- Engage when intensity crosses threshold
- Sustain while above release threshold
- Release smoothly when dropping
- Enter Unstable when confidence or stability drops

### Key Design Rule
Unstable input **reduces output**, never amplifies it.

---

## Air Field Model

Air is implemented as a **bounded force field**.

### Field Parameters
- Center (in front of player)
- Direction (smoothed)
- Strength (clamped)
- Radius
- Turbulence (from instability)

### Mapping
- Gesture intensity → force strength
- Sweep direction → force direction
- Instability → noise / damping

---

## Physics Integration

### Strategy
- Use Unity Rigidbodies
- Apply forces in `FixedUpdate`
- Use `Physics.OverlapSphereNonAlloc`

### Force Application
- ForceMode.Acceleration (mass-agnostic feel)
- Per-body acceleration clamp
- Velocity clamp for light props

### Safeguards
- Max affected bodies per frame
- Continuous collision on fast props
- Hard caps on impulse magnitude

---

## Visual Feedback (Minimal)

### Goals
- Communicate direction, intensity, and loss of control
- Never mask physics problems

### Visuals
- Streamlines or ribbon particles
- Directional cones
- Instability flicker or turbulence

### Rules
- Driven directly from physics parameters
- Fixed particle budgets
- No heavy transparency stacking

---

## Environment (Phase 1)

### Reactive
- Lightweight rigidbody props
- Hanging indicators (wind vanes)
- Dust or debris particles

### Static
- Arena walls and floor
- Structural pillars

No destruction systems in Phase 1.

---

## Performance Constraints

- Zero GC allocations in hot paths
- FixedUpdate tuned for Quest refresh
- NonAlloc physics queries only
- Hard caps on:
  - Active rigidbodies
  - Forces
  - Particles

Profiling is mandatory after every major change.

---

## Phase 1 “Done” Criteria

Air feels:
- Immediate
- Stable
- Predictable
- Learnable
- Expressive under mastery

No jitter, no runaway forces, no frame drops.

---

## Next Steps After Phase 1
- Refine gesture stability scoring
- Add two-hand coherence
- Begin Water using same architecture

Air is not replaced or rewritten.  
All future elements extend this foundation.

