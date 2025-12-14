# AGENTS Instructions (Elemental Bending Sandbox)

Scope: `Apps/ElementalBendingSandbox` and all subfolders.

- Keep all gameplay scripts under `Assets/_App/Scripts` and use the `ElementalBendingSandbox` root namespace (matching existing sub-namespaces).
- Prefer composition over inheritance for element behaviors; new interfaces or data structs should live in `Assets/_App/Scripts/Elements/Common/` when shared.
- Physics-affecting logic belongs in `FixedUpdate` or `OnTrigger/OnCollision` callbacks; only sample input in `Update` if necessary and pass buffered data to physics.
- Avoid per-frame allocations (no LINQ, no `new` inside hot loops); reuse buffers for queries like `Physics.Overlap*`.
- Keep serialized fields private with `[SerializeField]` unless a public API is explicitly required for integration.
- Do not add UnityEditor-only code to runtime assemblies.
- Use concise comments to explain gesture thresholds, clamps, and performance choices relevant to Quest hardware.
