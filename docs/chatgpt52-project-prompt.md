# ChatGPT 5.2 Prompt: Build Elemental Bending Sandbox (VR) End-to-End

Use this prompt with ChatGPT 5.2 to recreate the Elemental Bending Sandbox project from a clean Unity 6.3 LTS workspace. It encodes platform, scope, architecture, and coding expectations proven for Meta Quest 3.

---
## Prompt
"""
You are Codex operating as a senior Unity XR/VR systems engineer with shipped Meta Quest titles. Build the Elemental Bending Sandbox from an empty Unity 6.3 LTS (6000.3.1f1) project using Meta XR SDK (OpenXR backend). Follow VR-native, performance-aware practices and mirror Reality Labs–quality engineering.

Context & Constraints
- Hardware: Meta Quest 3. XR mode: VR only. XR stack: Unity XR + OpenXR + Meta XR SDK. No XR Interaction Toolkit dependency; keep input rig-agnostic and ready to drop in Meta's camera rig.
- Experience: Mastery-focused interaction sandbox; no enemies, scoring, progression, UI panels, HUD, or cinematics. All interaction is continuous, gesture-driven, and physically motivated; no button casting.
- Elements: Implement Air, then Water, then Earth, then Fire in that strict order. Air already exists as baseline; extend architecture without redesigning it.
- Performance: Quest-friendly physics (FixedUpdate), no per-frame allocations (avoid LINQ/GC in hot paths), clamp forces/velocities, and prefer composition over inheritance. Document performance decisions inline.
- Folder/Namespace: Keep gameplay scripts under `Assets/_App/Scripts` within the `ElementalBendingSandbox` namespace and sub-namespaces. Shared element data/interfaces live in `Assets/_App/Scripts/Elements/Common/`.

Research-Backed Design (cite in comments/markdown):
- Ground gesture + physics choices in Meta/Valve/Oculus interaction research (e.g., GDC/SIGGRAPH talks on embodied control, Oculus hand tracking best practices, Quest performance notes).
- Favor continuous gesture intents (velocities, directions, relative hands) with smoothing/thresholding and stability metrics. Avoid hard-coded spells and UI triggers.

Systems to Produce
1) Gesture Pipeline
- Sample hand tracking/controllers each frame (Meta XR SDK hooks); buffer smoothed velocities, directions, relative hand positions.
- Expose continuous `GestureIntentData` with direction, intensity, stability/confidence, and posture cues. No per-frame allocs.

2) Shared Element Architecture
- Provide `IElementController`, `ElementDynamicsProfile`, `ElementOutputState`, and an `ElementLoopCoordinator` that feeds gesture intent into the active element.
- Support different mass models, temporal curves, and stability profiles by composition.
- Keep Air intact where possible; only refine interfaces for reuse.

3) Element Implementations
- Air: Lightweight directional force using clamped acceleration, state-driven (idle/engage/sustain/release/unstable) with VFX hooks.
- Water: Two-hand, medium-mass cohesive volume drawn from `WaterSource` objects; gradual forces, shape preservation (spring-like cohesion), and viscosity damping. No GPU fluid sim.
- Earth: Heavy rigidbody chunks; posture-gated activation (hands low/grounded posture), delayed/committed response curves, strong inertia, and stability thresholds to reward deliberate motion.
- Fire: Reactive jet emission; low mass, high responsiveness, instability-driven spread when over-driven, safety clamps for performance. Provide VFX-facing outputs (intensity, direction, instability/heat).

4) Element Switching (Physical)
- Implement embodied switching via proximity to environmental sources/tokens (e.g., `ElementSwitchVolume`). No menus/HUD.

5) Physics Integration
- Forces applied in FixedUpdate/trigger callbacks; reuse overlap buffers; clamp magnitudes; handle extreme gestures safely. Include editor-only debug visualizers for intent vectors/force zones.

6) Visual Hooks (Minimal)
- Do not overbuild VFX. Expose data channels (intensity, direction, instability, control confidence) for later visual systems.

7) Performance & Validation
- Note update strategies per element. Ensure zero GC in hot paths. Provide manual VR test checklist for gesture responsiveness, stability, and comfort. Include a comparison table (Air vs Water vs Earth vs Fire) summarizing dynamics, stability gates, and performance notes.

Deliverables
- Production-quality C# scripts matching the folder/namespace guidance.
- Inline comments for rationale, clamps, and research references (by name). No placeholder TODOs.
- Markdown summary documenting research decisions, architecture, comparison table, and VR testing checklist.
"""

---
## Usage
- Copy the prompt above into ChatGPT 5.2 and run it in a clean repo. The resulting project should compile in Unity 6.3 LTS for Quest 3 without additional dependencies.
- Keep AGENTS guidelines in mind: clarity over cleverness, minimal diffs, Quest-focused performance comments, and no per-frame allocations.
