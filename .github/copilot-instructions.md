# GitHub Copilot Instructions for "More Planning (Continued)" Mod

## Mod Overview and Purpose

**Mod Name**: More Planning (Continued)

**Description**: This mod is a continuation of the "More Planning" mod, originally created by Alan Dariva and Usagirei, and updated for RimWorld versions 1.3 to 1.5. The mod enhances the planning functionality in the game by introducing more customizable options and improving the user experience in the planning phase of base-building.

The mod leverages the HugsLib library and Harmony for modding capabilities.

## Key Features and Systems

- **Customizable Planning Designations**: The mod provides 10 customizable planning designations to allow for more detailed and personalized planning.
  
- **Visibility Control**: Users can show or hide planning designations as needed.

- **Opacity Control**: The plugin allows users to adjust the opacity of planning elements, aiding in better visibility under different circumstances.

- **Copy, Cut, and Paste Functions**: Similar to standard editing tools, users can cut, copy, and paste planning designations, making adjustments more efficient.

- **Auto-Remove Feature**: The mod offers an option to automatically remove planning designations when constructions are built or deconstructed on top of them.

- **Override with Shift**: By holding the Shift key, users can override other planning designations, streamlining the planning process.

## Coding Patterns and Conventions

- **Class and Method Organization**: The mod codebase uses a structured approach where classes are named according to their functionality, often following the Designator pattern (e.g., `CutDesignator`, `PasteDesignator`). This helps in maintaining a clean and understandable structure.

- **Internal and Public Access Modifiers**: Classes are defined as `internal` where they are meant to be accessible within the assembly only, and as `public` when they need to be used across different parts of the application or potentially by other mods.

- **Utility Classes**: `UtilityWorldObject` and other utility classes/methods are used to keep common functionalities organized and reusable.

## XML Integration

- **Defining Designations**: Designations are defined using XML, integrating seamlessly with RimWorld's existing XML-based modding architecture. This setup allows for easy customization and extension if further designations or features need to be added.

- **ModSettings**: XML is also used for mod settings, allowing for persistent user configuration across sessions.

## Harmony Patching

- **HarmonyPatches.cs**: This file contains static classes and methods that are used to apply Harmony patches to RimWorld. These patches are essential for hooking into the game's original methods, enabling the mod to extend or alter the default game behavior without directly modifying the game's assembly.

- **Method Overrides**: Specific game methods are overridden or extended using Harmony to implement enhanced features, such as controlling the visibility of planning designations or altering game UI elements.

## Suggestions for Copilot

1. **Patterns Recognition**: Suggest designator classes consistently by recognizing the common pattern of class extensions and method signatures (e.g., extending `BaseDesignator`).

2. **Error Handling**: Encourage implementing try-catch blocks in methods that interact with the game's core functions or other mods, as this is crucial for managing potential mod conflicts gracefully.

3. **Feedback Mechanism**: Utilize in-game logging (with HugsLib's logging capabilities) to provide feedback when actions are taken or when errors occur, enhancing user experience and debugging.

4. **Enhance XML Definitions**: Propose syntax for defining new XML elements that conform to RimWorld's modding structure, making future extensions easier to implement.

5. **Improve User Interactions**: Suggest UI enhancements or shortcuts that could improve player interactions with the planning tools, such as context menus or additional keybinds.

By following these structures and suggestions, developers and contributors can maintain and extend "More Planning (Continued)" with ease, ensuring a robust and user-friendly experience.


This file provides comprehensive instructions for leveraging GitHub Copilot effectively in the development and maintenance of the "More Planning (Continued)" mod for RimWorld.
