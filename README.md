# Civil 3D Template

A minimal starting point for a Civil 3D plug-in written in C#. It adds a **My Project** ribbon tab with a single **Show Message** button, which writes a message to the Civil 3D command line (command: `SHOW_MESSAGE`).

## Requirements

- Windows, 64-bit
- Autodesk Civil 3D 2025 (installed and licensed)
- Visual Studio Community 2022 (17.8 or later) or newer, with the **.NET desktop development** workload (includes the .NET 8 SDK)

## Autodesk files

The project references a few Autodesk DLLs (`acmgd`, `acdbmgd`, `accoremgd`, `AcWindows`, `AdWindows`) directly from the local Civil 3D installation. They are licensed Autodesk files and are **not** part of this repository or the build output: every reference uses `<Private>False</Private>`, and `.gitignore` excludes all `*.dll` files. Civil 3D must be installed on the computer to build the project.

To use the Civil 3D API in your own commands, add references to `C3D\AeccDbMgd.dll` (and `ACA\AecBaseMgd.dll`) from the same installation folder, again with `<Private>False</Private>`.

## Project structure

| File | Purpose |
|---|---|
| `application.cs` | Plug-in entry point; builds the ribbon tab and button |
| `CommandHandler.cs` | Links a ribbon button to a command |
| `src/Commands/ShowMessage.cs` | The `SHOW_MESSAGE` command |
| `Resources/Message_32.png` | Button icon (embedded resource) |
| `Civil3DTemplate.bundle/PackageContents.xml` | Tells Civil 3D how to auto-load the plug-in |
| `Properties/launchSettings.json` | Lets Visual Studio start Civil 3D with **Start** (F5) |

## How it runs

1. **Build** in Visual Studio. After every build the DLL and `PackageContents.xml` are copied to
   `C:\ProgramData\Autodesk\ApplicationPlugins\Civil3DTemplate.bundle\`.
2. **Start** (F5) launches Civil 3D 2025 with the debugger attached.
3. Because the plug-in is installed as an Autodesk bundle, it is also available when Civil 3D is opened in the ordinary way.

> Close Civil 3D before building: while it is running, the DLL is locked and the copy step fails.

## Targeting a newer Civil 3D version

**Civil 3D 2026** (same AutoCAD engine and .NET 8):

1. In `Civil3DTemplate.csproj`, change `<C3DVersion>2025</C3DVersion>` to `2026`.
2. In `Properties/launchSettings.json`, change `AutoCAD 2025` to `AutoCAD 2026`.

`PackageContents.xml` already covers 2025–2026 (`R25.0`–`R25.9`).

**Civil 3D 2027** (new engine, .NET 10): the two changes above (with `2027`), plus

3. In `Civil3DTemplate.csproj`, change `<TargetFramework>net8.0-windows</TargetFramework>` to `net10.0-windows`.
4. In `Civil3DTemplate.bundle/PackageContents.xml`, change `SeriesMin="R25.0" SeriesMax="R25.9"` to `SeriesMin="R26.0" SeriesMax="R26.9"`.
5. Use a Visual Studio version that includes the .NET 10 SDK (Visual Studio 2026).

If a reference shows a yellow warning icon in Solution Explorer, check that the DLL exists at the path in `Civil3DTemplate.csproj` on your computer.
