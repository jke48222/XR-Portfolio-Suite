# XR Portfolio Suite – Performance Budgets (Quest 3 Focus)

These budgets define the target envelopes for stable performance on Quest-class hardware.
Budgets are enforced through:
- design constraints
- runtime counters
- stress tests
- profiling evidence

## 1. Global Targets

### 1.1 Frame Rate Target
- Primary target: stable headset frame rate with no sustained drops.
- Secondary target: consistent frame pacing.
- Avoid spikes: any spike is treated as a bug until explained.

### 1.2 GC and Allocations
- Steady-state per-frame allocations: 0 bytes.
- Acceptable exceptions:
  - controlled, infrequent allocations during scene load only.
- All recurring objects must be pooled.

## 2. Shared Budget Categories

### 2.1 Transparency and Particles
- Transparent overdraw is the main GPU risk.
- All apps must implement:
  - particle count tracking
  - max active particle system cap
  - max screen coverage rule for hero effects

### 2.2 Dynamic Lights
- Prefer emissive materials and baked-like cues.
- Dynamic lights are capped per scene and per effect burst.

### 2.3 Physics
- Active rigidbodies and colliders are budgeted.
- Avoid deep collider stacks and high-contact piles.

### 2.4 Audio
- Cap concurrent audio sources.
- Avoid per-frame audio allocations and excessive DSP on main thread.

## 3. App-Specific Budgets (Initial Targets)

These are starting budgets. The first profiling pass will adjust them.

### 3.1 MR Visual Symphony
- Active particle systems: 8 to 16 (hard cap; pool everything)
- Concurrent particles (approx): start low, scale via quality tier
- Dynamic lights: 0 to 2
- Transparent full-screen effects: none in steady state
- Occlusion: enabled only when stable; must have fallback mode

Worst-case mode:
- Max anchors (7)
- All instruments active
- Peak effect burst allowed but must settle quickly

### 3.2 Elemental Bending Sandbox
- Active rigidbodies: 40 to 80 in training range
- Active particle systems: 6 to 12
- Dynamic lights: 0 to 2
- Physics impulses clamped to avoid instability
- Per-element stress toggle:
  - Earth: max debris + stacking stability
  - Air: max force volumes
  - Water: max ribbons/particles
  - Fire: max volumetric sprites

Worst-case mode:
- Two elements active concurrently
- Full combo spam simulation for 30 seconds

### 3.3 MR Pocket Portals
- Concurrent portals: 2 (hard cap for v1)
- Active particle systems: 6 to 10
- Dynamic lights: 0 to 1 (prefer emissive)
- Portal interior render complexity tightly controlled
- Occlusion: enabled only if stable; must have fallback mode

Worst-case mode:
- Two portals open
- Maximum spill effects
- Universe with the heaviest interior content

## 4. Quality Tiers

Each app must support quality tiers:
- Low: safe baseline
- Default: target look
- High: capture mode only

Tier changes must:
- be instantaneous or fade smoothly
- never allocate per-frame
- never break saves

## 5. Profiling Evidence Requirements (Portfolio)

Each app must produce:
- a screenshot of profiler timeline under worst-case mode
- a short paragraph interpreting the bottleneck
- the final tuned budget values and why

## 6. Common Performance Failure Patterns (Non-exhaustive)
- particle overdraw and transparency layers
- dynamic shadows on multiple lights
- frequent instantiation and destruction
- high-contact rigidbody piles
- animated skinned meshes with expensive shaders
- heavy post processing in MR

Treat these as design constraints, not optimization tasks.
