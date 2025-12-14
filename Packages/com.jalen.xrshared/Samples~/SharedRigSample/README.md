# Shared Rig Sample

This sample exists to validate that the `com.jalen.xrshared` package
boots correctly without opening a full app project.

## Purpose
- Demonstrate AppBootstrap + ServiceInstaller
- Validate TickRunner and DebugOverlay
- Provide a minimal sanity check for the shared core

## How to Use
1. Create a new empty Unity scene.
2. Add an empty GameObject named `AppRoot`.
3. Add `AppBootstrap` to it.
4. Assign an `AppConfig` asset.
5. Press Play.

If the debug overlay appears and no errors are logged,
the shared package is functioning correctly.

This sample intentionally contains no XR or MR dependencies.
