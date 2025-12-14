# MR Pocket Portals (Unity 6000.3.1f1)

## Overview
A curated mixed reality portal experience where the user opens miniature universes in their real room. The focus is premium compositing, strong art direction, and polished interaction.

## Key Features
- portal placement and manipulation via hand tracking
- modular universe presets with distinct visual identity
- subtle spill effects that blend into the real environment
- persistent anchors to restore portal placement

## Tech Stack
- Unity 6.3 LTS (6000.3.1f1)
- Meta XR SDK: Core, Interaction SDK
- URP
- Depth-aware occlusion with fallback mode

## Scenes
- Bootstrap: service initialization and configuration validation
- Experience: MR portal placement space

## How to Run
1. Open the project under Apps/MRPocketPortals in Unity 6000.3.1f1.
2. Open Bootstrap scene and press Play (Editor) or build to Quest.

## Demo Script (60 seconds)
1. Place a portal on a desk surface.
2. Open and resize it, then switch universes.
3. Lean in to show interior diorama detail.
4. Show spill effects interacting with real space.
5. End with a two-portal composition shot.

## Architecture Summary
- input -> portal placement -> anchor bind -> universe loader -> portal render + spill effects
- universe modules are data-driven presets for repeatable polish
