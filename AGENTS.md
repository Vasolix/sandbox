# Repository Guidelines

## Project Structure & Module Organization
This is a s&box game project for `VBaseWars`. Runtime C# code lives in `Code/`, grouped by domain: `GameLoop`, `Player`, `Weapons`, `Npcs`, `UI`, `Components`, `Spawner`, and related systems. Editor-only code lives in `Editor/`. Game content is stored in `Assets/`, including scenes (`Assets/scenes`), prefabs, weapons, sounds, materials, entities, and UI images. Translations live in `Localization/<locale>/`. Engine and game settings live in `ProjectSettings/`, and package metadata is defined in `sandbox.sbproj`.

## Build, Test, and Development Commands
- `dotnet build vbasewars.slnx` builds the game and editor projects. This requires local s&box references matching the paths in the solution.
- `dotnet build Code/vbasewars.csproj` builds only runtime code.
- `dotnet build Editor/vbasewars.editor.csproj` builds editor extensions.
- Open the project in the s&box editor and run `Assets/scenes/vbasewars.scene` for play testing. The startup scene is also configured in `sandbox.sbproj`.

There is no standalone automated test suite in this repository. Treat successful builds plus targeted in-editor validation as the minimum check before submitting changes.

## Coding Style & Naming Conventions
Follow `.editorconfig`: use tabs with width 4 for `*.cs` and `*.razor`, CRLF line endings, and final newlines. Place `using` directives outside namespaces. Prefer braces, pattern matching, expression-bodied properties/accessors when concise, and normal block-bodied methods for non-trivial logic.

Use PascalCase for types and public members, camelCase for locals and parameters, and keep partial files named by feature, for example `Player.Camera.cs` or `BaseWeapon.Reloading.cs`. Keep Razor component styles next to components as `Component.razor.scss`.

## Testing Guidelines
When adding gameplay behavior, validate the affected flow in s&box: spawning, networking, damage, weapons, UI, or scene interactions as applicable. If adding tests later, place them in a clear `UnitTest`/`Tests` area and name files after the system under test, such as `InventoryTests.cs`.

## Commit & Pull Request Guidelines
Recent commits use short, imperative summaries such as `Remove physgun blockout` and `fix trace attack applying force to physics incorrectly`. Keep commits focused and describe the behavioral change.

Pull requests should include a concise description, affected systems, validation steps performed, and screenshots or clips for visible UI, scene, weapon, or asset changes. Link related issues and call out any migration or content-rebuild requirements.

## Agent-Specific Instructions
Do not rewrite generated asset metadata casually. Preserve existing scene, prefab, and localization structure unless the task explicitly requires changing it. For code changes, prefer narrow edits in the relevant domain folder and verify with `dotnet build`.
