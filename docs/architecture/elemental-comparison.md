# Elemental Control Comparison (Air vs Water vs Earth vs Fire)

| Element | Mass Model | Control Feel | Update Strategy | Stability Handling | Physics Coupling | Visual Hooks |
| --- | --- | --- | --- | --- | --- | --- |
| Air | Mass-agnostic acceleration | Light, responsive push | Gesture in FixedUpdate via `AirElementController` | Confidence gates to Unstable, intensity damped | Force zones applying `ForceMode.Acceleration` | Instability and intensity outputs per frame |
| Water | Medium mass cohesive volume | Smooth, shapeable flow drawn from sources | Cohesive rigidbody driven in `FixedUpdate` by `WaterElementController` | Instability halves intensity; requires source volume | Direct forces on water body with cohesion + viscosity | Volume %, instability, direction exposed for VFX ribbons |
| Earth | Heavy inertial body | Slow, committed motion gated by posture | Rigidbody updated in `FixedUpdate`; commitment timer | Requires hands below head; instability damps velocity | Planar acceleration clamps + release damping | Output state includes commitment readiness |
| Fire | Low mass reactive jet | Fast, sharp emission with dispersion | Emission volume executed every `FixedUpdate` | Instability increases spread, reduces focus | Overlap non-alloc cone applying acceleration | Spread + intensity for heat/glow drivers |

## Manual VR Test Checklist
- Confirm element switching via proximity volumes without UI; dwell hand in each volume and observe active controller change.
- Air: push with coherent hand movement; verify idle → engage → sustain transitions and stable acceleration on lightweight props.
- Water: stand near a `WaterSource`, draw flow with two-hand arc; ensure volume fills, cohesive body follows direction, and drains when leaving source.
- Earth: lower hands below head, hold intent for commit time; confirm heavy body accelerates slowly and stops when posture rises.
- Fire: rapid, sharp gestures produce directional emission; intentionally jitter hands to see spread widen and forces dampen.
- Stress test multiple rigidbodies in each element’s influence to watch for frame hitches or runaway velocities.

## Deferred Expansion Ideas
- Add posture-aware modifiers using future body tracking APIs once Meta XR rig is integrated.
- Introduce networked ghosting of intent data for collaborative practice (out of scope now).
- Layer lightweight GPU VFX driven purely by `ElementOutputState` without altering physics.
