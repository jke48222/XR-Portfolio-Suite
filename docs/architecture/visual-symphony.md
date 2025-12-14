# MR Visual Symphony Architecture

## Core Loop
Audio input -> Feature extraction -> Mapping graph -> VFX bus + spatial audio

## Persistence
Anchor bindings + instrument presets serialized by schema version.

## Performance
Primary risk is transparency overdraw and occlusion cost.
Quality tiers clamp particle density and effect complexity.

## Key Systems
- IAudioFeatures
- AudioMappingGraph
- IAnchorStore
- PerformanceGovernor
- DebugOverlay
