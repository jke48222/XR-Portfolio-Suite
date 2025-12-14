# Elemental Expansion Research Notes

## VR Interaction Patterns Referenced
- **Meta Quest Hand Physics Lab / First Encounters**: favors continuous hand-driven forces with low latency; motivates keeping gesture sampling in Update and physics in FixedUpdate with cached intent.
- **Valve Aperture Hand Lab**: uses coherence between both hands for intent detection and prioritizes predictable physical responses over visuals; informs stability scoring and coherence checks.
- **Oculus Touch Controller Best Practices (Meta blog)**: recommends acceleration-based forces for mass-agnostic feel and explicit clamping for comfort; applied to all elemental force zones.
- **GDC 2018 “The Unspoken: Hands, Gestures, and Feedback”**: highlights gradual ramps and confidence gating to avoid accidental casts; reflected in engage/release thresholds and instability damping.
- **SIGGRAPH 2019 “Fluid Interaction for VR” (Ubisoft demo)**: showed lightweight cohesive ribbon approximations instead of full fluid sim; drives the water cohesive volume approach.
- **Lone Echo interaction talks**: stress posture-based affordances and inertia for heavy objects; informs Earth’s grounded gating and delayed response.

## Design Rationale
- **Continuous, noisy-input tolerant gestures**: smoothing and coherence scoring avoid frame-to-frame jitter, aligning with Meta research on comfort and nausea avoidance.
- **ForceMode.Acceleration preference**: maintains consistent feel across varying rigidbody masses, cited in Meta physics guidelines for VR interactions.
- **Instability models**: Fire and Air reduce output when confidence drops to prevent runaway forces and support comfort (per Oculus hand tracking UX notes).
- **No GPU fluid / particles**: Quest 3 budgets encourage CPU-friendly approximations; water uses cohesive rigidbody volume with spring-like shaping instead of heavy simulation.
- **Posture gating for Earth**: requiring low hands and short-duration commitment mirrors embodied control patterns from Valve and Oculus locomotion talks, reinforcing intent and reducing false triggers.
- **Environmental sources for switching**: proximity-based activation keeps interactions diegetic and avoids HUD, aligning with best practices from Meta Presence demos.

## Alternatives Rejected
- **Full smoothed-particle hydrodynamics**: too expensive on Quest 3 without native plugins; replaced by cohesive volume with configurable drag/viscosity.
- **Button or gesture-discrete spell casting**: violates continuous control goal and adds latency; replaced with continuous intent scaling and state machines.
- **XR Interaction Toolkit dependencies**: avoided to stay rig-agnostic for Meta XR SDK swap-in.
