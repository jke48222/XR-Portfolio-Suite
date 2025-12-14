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
