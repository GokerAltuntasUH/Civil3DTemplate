# Civil 3D Template

A minimal starting point for a Civil 3D plug-in written in C#. It adds a **My Project** ribbon tab with a single **Show Message** button, which opens a message window (command: `SHOW_MESSAGE`).

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

## Opening the project

Open **`Civil3DTemplate.sln`** (double-click it, or **File > Open > Project/Solution**), not the folder. The green start button should read **Civil3DTemplate**. If it reads **Select Startup Item...**, Visual Studio is in folder view: use **File > Close Folder** and open the `.sln` instead.

## Adding your own command

1. **Command**: add a class in `src/Commands` (right-click > Add > Class) with a method marked `[CommandMethod("MY_COMMAND")]`. Use `ShowMessage.cs` as the model. Command names must be unique; a project prefix such as `MP_` avoids clashes with Civil 3D commands.
2. **Button (optional)**: in `application.cs`, copy the `showMessageButton` block, change `Text`, `Id` and the command name in `RibbonCommandHandler("MY_COMMAND")`, and add it with `generalPanelSource.Items.Add(...)`.
3. **Icon (optional)**: put a 32x32 PNG in `Resources`, set its Build Action to **Embedded Resource**, and load it with `LoadPngFromResources("Civil3DTemplate.Resources.<FileName>.png")`.
4. Close Civil 3D, build, and start with F5.

The setup guide (PDF) contains a complete worked example (`SHOW_DRAWING_NAME`).

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

## Similar examples

Other templates and references for Civil 3D / AutoCAD .NET plug-ins:

- [AutoCAD .NET Wizards](https://github.com/ADN-DevTech/AutoCAD-Net-Wizards) by Autodesk's developer team: a Visual Studio extension that adds AutoCAD project templates (C# and VB) for AutoCAD 2025 on .NET 8, with launch profiles for Civil 3D.
- [AutoCAD C# Project Template](https://github.com/gileCAD/AutoCAD-Csharp-Project-Template) by gileCAD: Visual Studio templates for AutoCAD versions before 2025 (.NET Framework 4.8), 2025–2026 (.NET 8) and 2027+ (.NET 10), including a template that builds for all three.
- [Civil 3D Project Template for Visual Studio](https://blog.autodesk.io/civil-3d-project-template-for-visual-studio/) on the Autodesk developer blog: an early Civil 3D template (2015) with the same bundle-deployment approach used here.
- [Creating New Civil 3D .NET Plugins](https://help.autodesk.com/cloudhelp/2025/ENU/Civil3D-DevGuide/files/GUID-04C3877E-4B9D-4CC2-B7A5-9301B6BCE21A.htm) in the Civil 3D Developer's Guide: Autodesk's step-by-step instructions for setting up a project by hand.
- [Creating a Civil 3D plug-in with a custom ribbon tab and buttons](https://forums.autodesk.com/t5/net-forum/creating-civil-3d-plugin-with-custom-ribbon-tab-and-buttons/m-p/13112959) on the Autodesk .NET forum: a discussion of the ribbon approach used in `application.cs`.

This template differs mainly in that it combines a ready-made ribbon button with automatic loading via the Autodesk ApplicationPlugins folder.
