# MR Visual Symphony (Unity 6000.3.1f1)

## Overview
MR Visual Symphony turns your real room into a spatial, audio-reactive musical instrument using passthrough, hand tracking, persistent anchors, and real-time audio feature mapping.

## Key Features
- hand-tracked interaction: ray select, poke UI, grab and two-hand manipulation
- anchored instruments placed in the real environment and persisted across sessions
- real-time audio analysis driving premium VFX and spatial audio behaviors
- capture mode for repeatable cinematic shots

## Tech Stack
- Unity 6.3 LTS (6000.3.1f1)
- Meta XR SDK: Core, Interaction SDK
- URP
- Quest-targeted performance budgets and stress tests

## Scenes
- Bootstrap: service initialization and configuration validation
- Experience: MR performance space

## How to Run
1. Open the project under Apps/MRVisualSymphony in Unity 6000.3.1f1.
2. Ensure Meta XR packages are installed and configured for Quest.
3. Open Bootstrap scene and press Play (Editor) or build to Quest.

## Demo Script (60 to 90 seconds)
1. Place 3 anchors around the room.
2. Enable a preset with strong bass waves and wall ribbons.
3. Conduct for 20 seconds and demonstrate responsiveness.
4. Show a close-up occlusion moment.
5. End with a wide-shot composition.

## Architecture Summary
- audio input -> feature extraction -> mapping graph -> vfx bus and spatial audio param routing
- anchors bind instruments to physical space and persist mappings
- performance governor enforces caps and quality tiers

## Portfolio Talking Points
- stable hand-first interaction design and comfort tuning
- real-time DSP feature extraction with jitter-resistant mapping
- MR compositing and occlusion strategy with graceful fallback
- performance engineering focused on transparency and VFX

## Known Constraints (Intentional)
- not a full DAW; focuses on spatial performance and visual musicality
- limited anchor count for stability and performance
