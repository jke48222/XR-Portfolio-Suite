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

