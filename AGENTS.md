# Repository Guidelines

## Project Structure & Module Organization

This repository is a Unity 6 project (Editor `6000.5.9f1`) for a mobile game. Runtime code is in `Assets/Scripts`, with gameplay-specific code in `Assets/Gameplay Scripts`. Scenes are in `Assets/Scenes` (`Gameplay.unity` and `POPUPUI.unity`); reusable objects are in `Assets/Prefabs`, and images/audio are in `Assets/Sprites` and `Assets/Audios`. Supporting assets and dependencies live under `Assets/Resources`, `Assets/Settings`, and `Packages`; project configuration is in `ProjectSettings`.

Keep new game code and assets under `Assets`. Unity `.meta` files are required: keep them alongside assets and include them when adding, moving, or renaming files. Avoid editing generated `Library`, `Temp`, or build output.

## Build, Test, and Development Commands

Open the repository in Unity Hub with the specified Editor. Run scenes in Play Mode and create platform builds from the Editor. For a batch build, Unity supports commands such as:

```powershell
Unity.exe -batchmode -projectPath . -buildTarget Android -quit
```

A build requires a configured build profile and platform modules; use the project’s Build Profiles where applicable. The Unity Test Framework package is installed. Run EditMode or PlayMode tests from **Window > General > Test Runner**; no repository-specific test command or coverage threshold is configured.

## Coding Style & Naming Conventions

Use C# conventions consistent with the existing scripts: four spaces for indentation, PascalCase for types and public members, and descriptive camelCase for private fields and locals. Give MonoBehaviour scripts filenames matching their class names. Keep serialized scene-facing fields stable when possible, since scenes and prefabs reference them. Preserve the project’s existing naming and language choices for user-facing content.

## Testing Guidelines

For gameplay or UI changes, exercise the affected scene in Play Mode, including mobile layouts. Add tests under an appropriate `Assets` test folder when introducing testable behavior; name test files and methods to describe the behavior being checked. Confirm Unity reports no compilation errors before submitting.

## Commit & Pull Request Guidelines

Git history was unavailable in the current environment, so no established commit format could be confirmed. Use short, imperative commit subjects (for example, `Fix pause menu layout`). Pull requests should summarize the change, list the scenes or assets affected, link related issues when available, and include screenshots for visible UI or gameplay changes. Mention test/build results and any manual setup needed.

## Security & Configuration Tips

Do not commit credentials, signing keys, or local machine paths. Keep package declarations in `Packages/manifest.json` and let Unity update `packages-lock.json` when dependencies change.

