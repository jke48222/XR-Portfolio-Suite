# Elemental Bending Sandbox Architecture

## Core Loop
XR input -> Gesture window -> Classifier -> Move resolver -> Simulation -> Feedback mixer

## Stability
Impulse clamping and hard caps on rigidbodies and VFX.
Worst-case stress mode used in every performance pass.

## Key Systems
- IGestureClassifier
- FeedbackMixer
- PerformanceGovernor
- DebugOverlay

## Detailed Phase 1 Plan

For a comprehensive Phase 1 implementation plan focusing on the Air element and early infrastructure, see [air-element-phase1-plan.md](air-element-phase1-plan.md).
