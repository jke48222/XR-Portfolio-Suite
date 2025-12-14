# Shared Core Architecture (com.jalen.xrshared)

## Overview
The shared core provides:
- deterministic app startup
- service installation and validation
- centralized ticking
- performance governance and debug overlay
- persistence and schema versioning
- capture mode hooks

## Startup Sequence
1) AppBootstrap loads AppConfig
2) Validators run (editor and runtime)
3) ServiceInstaller installs core services
4) AppStateMachine enters BootState
5) AppStateMachine transitions to MainState

## Data Flow
Input -> Intent -> App Systems -> Feedback -> Persistence

- IInputRouter routes raw XR input into app systems.
- App systems publish DebugStats and optionally events to EventBus.
- PerformanceGovernor enforces quality tiers and caps.
- PersistenceStore saves schema-versioned state.

## Service Locator Rules
- Only AppBootstrap installs services.
- Any system may resolve services, but only through interfaces.
- In tests, services can be replaced with fakes or null implementations.

## Debug Overlay
- Reports FPS, allocations, and app counters.
- Toggle exists in DEV builds and editor.

## Capture Mode
- Standard capture hooks for:
  - hiding UI
  - switching quality tier
  - enabling cinematic transitions
