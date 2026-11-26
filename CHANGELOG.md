# Changelog

## [2.1.0] - 2026-11-25

### Added
- `SafeArea`, `ResponsiveCanvas`, `ResponsiveSwitch` and `DeviceInfo` (phone / tablet / desktop, orientation).
- ArchNet Hub service **Responsive UI**: makes every canvas of the ArchNet prefabs and scenes responsive (screens nested in another prefab are handled in their own prefab); Responsive UI window (Device Simulator, your own canvases, mobile orientations).
- Edit Mode tests.

### Changed
- References `UnityEngine.UI` explicitly.

## [2.0.0] - 2026-11-25

Unity 6 release.

### Changed
- Requires Unity 6000.0 or newer.
- Code split into `Runtime/` (game code) and `Editor/` (Editor-only assembly, never in builds).
- Namespace `ArchNet.<Category>`; every file starts with a short description and a link to the documentation.
- Files moved with their `.meta`: existing scenes, prefabs and assets keep their references.
- Renamed: `SetCanvasToForeground` → `BringToFront`, `SetCanvasToDefault` → `ResetSorting` (it no longer destroys the GraphicRaycaster), `SetVisible/SetInvisible` → `Show/Hide/SetVisible`, `RefreshCanvas` → `RebuildLayout`.
