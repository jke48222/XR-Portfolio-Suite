# Elemental Bending Sandbox (Unity 6000.3.1f1)

## Overview
A mastery-focused VR sandbox built around physics-driven, gesture-based elemental control. The design goal is interaction quality and embodied skill, not content quantity.

## Key Features
- gesture system with confidence scoring and false-positive prevention
- two elements implemented at premium quality in v1
- dojo drills that provide measurable feedback and progression
- performance-stable simulation with strict budgets and pooling

## Tech Stack
- Unity 6.3 LTS (6000.3.1f1)
- OpenXR + Meta XR support where needed
- URP
- Quest performance and stability constraints

## Scenes
- Bootstrap: service initialization and configuration validation
- Dojo: primary training environment
- TestRange: stress and profiling environment

## How to Run
1. Open the project under Apps/ElementalBendingSandbox in Unity 6000.3.1f1.
2. Open Bootstrap scene and press Play (Editor) or build to Quest.

## Demo Script (90 seconds)
1. Show one basic move with a clean gesture.
2. Run a drill and hit targets with clear feedback.
3. Perform a short combo chain.
4. Trigger a controlled hero burst and return to calm.

## Architecture Summary
- xr input -> gesture window -> classifier -> move resolver -> simulation + feedback mixer
- stability constraints clamp impulses and enforce hard caps on active physics and VFX

## Portfolio Talking Points
- gesture recognition architecture and mitigation of misclassification
- physics stability techniques and worst-case stress testing
- VFX language and haptics design tied to simulation state
