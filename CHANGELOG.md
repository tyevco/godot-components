# Changelog

All notable changes to the Godot Components plugin will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.2.0] - 2025-11-20

### Fixed

#### Critical Bug Fixes
- **ComponentToolPlugin**: Fixed hard-coded magic number (46) in `GetProjectRoot()` method
  - Now dynamically calculates project root by navigating directory structure
  - Prevents plugin failure when folder structure changes
  - Location: `ComponentToolPlugin.cs:135`

- **ComponentToolPlugin**: Added null checking for texture loading
  - Static default texture now loaded with validation in `_EnterTree()`
  - Prevents NullReferenceException if icon file is missing
  - Added error logging for missing textures
  - Location: `ComponentToolPlugin.cs:25`

- **ComponentToolPlugin**: Added comprehensive script loading validation
  - Script loading now includes null checks and error handling
  - Base type validation prevents invalid component registration
  - Path bounds checking prevents index out of range errors
  - Clear error messages for all failure cases
  - Location: `ComponentToolPlugin.cs:118`

- **SelectableComponent**: Fixed potential NullReferenceException
  - Added null check in `SetSelected()` method
  - Added validation in `_Ready()` lifecycle method
  - Warns users when OutlineMesh is not assigned in editor
  - Location: `SelectableComponent.cs:60`

- **NodeExtensions**: Removed memory leak from static cache
  - Eliminated static Dictionary that cached NodePath objects indefinitely
  - NodePath construction is lightweight, caching not needed
  - Prevents unbounded memory growth over time
  - Location: `NodeExtensions.cs:18`

#### Code Quality Improvements
- **ComponentToolPlugin**: Removed redundant null check in `AddCustomType()`
  - `ScanAssembly()` already filters for ComponentAttribute
  - Added clarifying comment

- **ComponentToolPlugin**: Refactored texture path handling
  - Hard-coded path moved to const `DEFAULT_ICON_PATH`
  - Custom icon loading includes null check and fallback
  - Error logging for failed custom icon loads

- **NodeExtensions**: Enhanced type checking with error logging
  - Silent cast failures now logged with helpful error messages
  - Distinguishes between "node not found" and "wrong type"
  - Improves debugging experience

- **SelectableComponent**: Improved API design
  - Added `Selected` property alongside existing `SetSelected()` method
  - Added property validation with error logging
  - Improved encapsulation with private backing field

### Added

- **Documentation**: Comprehensive XML documentation comments
  - All public classes, methods, and properties now documented
  - Includes parameter descriptions and return value documentation
  - Improves IntelliSense experience in IDE

- **FollowableComponent**: Added documentation and implementation suggestions
  - Clarified component is a placeholder/example
  - Added TODO comments with implementation ideas
  - Provided commented example code structure

- **ComponentAttribute**: Enhanced with XML documentation
  - Properties now read-only (initialized in constructor)
  - Better describes purpose and usage

- **Error Handling**: Comprehensive error logging throughout plugin
  - `GD.PrintErr()` calls for all error conditions
  - Clear, actionable error messages
  - Helps developers debug configuration issues

### Changed

- **NodeExtensions**: Simplified implementation
  - Removed static cache to prevent memory leak
  - Direct NodePath construction on each call
  - Added explicit type checking pattern

- **ComponentAttribute**: Made properties read-only
  - `ScriptPath` and `IconPath` now use getter-only properties
  - Values set via constructor only
  - Prevents accidental modification after creation

## [0.1.0] - Initial Release

### Added
- Initial component system implementation
- `[Component]` attribute for marking Node classes
- Automatic component registration with Godot editor
- Custom icon support
- Basic NodeExtensions for typed node retrieval
- Sample components (SelectableComponent, FollowableComponent)

---

## Migration Guide

### Upgrading from 0.1.0 to 0.2.0

No breaking changes. All fixes are backward compatible. However, you may now see additional error messages in the console if:

1. **OutlineMesh not assigned**: SelectableComponent will now warn you at runtime
2. **Wrong node type**: NodeExtensions will log errors when casting fails
3. **Missing resources**: Plugin will warn about missing icons or scripts

These warnings help identify configuration issues early. To silence them, ensure:
- All `[Export]` properties are assigned in the editor
- Node paths reference nodes of the correct type
- All resource paths are valid

## Known Issues

None at this time.

## Planned Features

- Support for custom categories in "Create New Node" dialog
- Component dependency validation
- Hot-reload support for component changes
- Performance profiling tools
