# Roadmap

This document outlines planned features, improvements, and future direction for the Godot Components plugin.

## Current Version: 0.2.0

The plugin currently provides core functionality for registering C# classes as custom Godot nodes using the `[Component]` attribute, with custom icon support and helpful node extension utilities.

---

## Upcoming Releases

### v0.3.0 - Enhanced Developer Experience (Near-term)

**Priority: High**

#### Component Organization
- **Custom Categories**: Support organizing components into custom categories in the "Create New Node" dialog
  - Add `category` parameter to `[Component]` attribute (e.g., `[Component(category: "Game/AI")]`)
  - Automatically create nested categories
  - Location: `ComponentAttribute.cs`, `ComponentToolPlugin.cs`

- **Component Groups**: Allow tagging components with multiple groups for better organization
  - Add `groups` parameter accepting string array
  - Enable filtering by group in editor

#### Improved Error Handling
- **Validation System**: Add comprehensive component validation
  - Detect circular dependencies
  - Validate required properties
  - Check for naming conflicts
  - Provide actionable error messages with fix suggestions

- **Diagnostic Tools**: Add runtime diagnostics
  - Component health checks
  - Performance warnings for heavy components
  - Missing dependency detection

#### Hot-Reload Support
- **Dynamic Reloading**: Automatically refresh components when scripts change
  - Watch for C# file changes
  - Re-register modified components without full project reload
  - Preserve component instances where possible
  - Location: `ComponentToolPlugin.cs`

---

### v0.4.0 - Advanced Component System (Mid-term)

**Priority: Medium-High**

#### Component Dependencies
- **Dependency Validation**: Ensure required components are present
  - Add `[RequiresComponent(typeof(OtherComponent))]` attribute
  - Automatically add missing dependencies in editor
  - Show warnings when dependencies are missing
  - Validate at both edit-time and runtime

- **Component Initialization Order**: Control component initialization sequence
  - Support dependency-based initialization
  - Add `[InitializationPriority(int)]` attribute
  - Ensure components initialize in correct order

#### Component Lifecycle Hooks
- **Extended Lifecycle**: Add component-specific lifecycle methods
  - `OnComponentEnabled()` / `OnComponentDisabled()`
  - `OnComponentValidate()` for editor validation
  - `OnComponentReset()` for resetting to defaults
  - Integration with Godot's existing lifecycle

#### Enhanced NodeExtensions
- **Additional Utilities**: Expand the NodeExtensions toolset
  - `GetComponentInChildren<T>()` - Find component in child nodes
  - `GetComponentInParent<T>()` - Find component in parent hierarchy
  - `GetComponents<T>()` - Get all components of type
  - `AddComponent<T>()` - Dynamically add components at runtime
  - `RemoveComponent<T>()` - Remove components at runtime
  - `HasComponent<T>()` - Check if component exists
  - Location: `extensions/NodeExtensions.cs`

#### Component Communication
- **Signal Integration**: Improved component-to-component communication
  - Auto-generate signals for common component events
  - Signal routing between components
  - Event bus for loosely coupled communication

---

### v0.5.0 - Performance & Developer Tools (Mid-term)

**Priority: Medium**

#### Performance Optimization
- **Assembly Scanning**: Optimize component discovery
  - Cache assembly scan results
  - Support incremental scanning
  - Multi-threaded scanning for large projects
  - Location: `ComponentToolPlugin.cs:46`

- **Component Pooling**: Support object pooling for frequently instantiated components
  - Add `[PoolableComponent(initialSize: 10)]` attribute
  - Automatic pool management
  - Performance metrics for pooled components

#### Profiling & Analytics
- **Performance Profiling**: Built-in performance monitoring
  - Track component initialization time
  - Memory usage per component
  - Component update frequency analysis
  - Export profiling data

- **Usage Analytics**: Help developers understand component usage
  - Most-used components report
  - Component dependency graph visualization
  - Unused component detection

#### Custom Inspector UI
- **Enhanced Editor Integration**: Better visual representation
  - Custom inspector panels for components
  - Visual property editors
  - Component state visualization
  - Real-time property monitoring

---

### v1.0.0 - Production Ready (Long-term)

**Priority: Medium**

#### Multi-Assembly Support
- **External Assembly Loading**: Support components from multiple assemblies
  - Scan all loaded assemblies, not just plugin assembly
  - Support for third-party component libraries
  - Assembly whitelist/blacklist configuration
  - Location: `ComponentToolPlugin.cs:32`

#### Component Templates
- **Template System**: Pre-configured component setups
  - Save component configurations as templates
  - Share templates across projects
  - Template marketplace/repository
  - Quick-start templates for common patterns

#### Advanced Features
- **Component Serialization**: Better save/load support
  - Custom serialization attributes
  - Version migration support
  - Backward compatibility helpers

- **Network Synchronization**: Multiplayer component support
  - Auto-sync component state across network
  - Integration with Godot's multiplayer API
  - Conflict resolution strategies

---

## Future Exploration (v1.1+)

**Priority: Low-Medium**

### Code Generation
- **Boilerplate Automation**: Reduce repetitive code
  - Auto-generate component wrapper classes
  - Property change notification generation
  - Signal declaration generation
  - Location: New tool in `tools/` directory

### Visual Scripting Integration
- **Visual Script Nodes**: Use components in visual scripts
  - Component nodes for visual scripting
  - Drag-and-drop component creation
  - Visual component composition

### Testing Utilities
- **Component Testing Framework**: Simplify component testing
  - Mock component system
  - Component test fixtures
  - Automated component validation tests
  - Integration with GdUnit or other test frameworks

### Documentation Tools
- **Auto-Documentation**: Generate documentation from code
  - Export component API documentation
  - Generate component usage examples
  - Create component relationship diagrams
  - Integration with DocFX or similar tools

### Advanced Editor Features
- **Component Marketplace**: Share and discover components
  - Built-in component browser
  - Import components from repository
  - Version management
  - Community ratings and reviews

- **Component Inheritance Visualization**: Understand component relationships
  - Visual dependency graph
  - Component hierarchy browser
  - Impact analysis for changes

### IDE Integration
- **Enhanced IDE Support**: Better development experience
  - Code snippets for common component patterns
  - Live templates for Rider/Visual Studio
  - IntelliSense improvements
  - Refactoring support

---

## Community Requests

This section will be populated based on community feedback and feature requests from users.

### Suggested Features
- Support for Godot 3.x (backport) - **Under consideration**
- GDScript interoperability - **Planned for v1.1+**
- Component presets in editor - **Planned for v1.0**

---

## Contributing

We welcome contributions! If you'd like to work on any of these features:

1. Check the [Issues](https://github.com/tyevco/godot-components/issues) page for related discussions
2. Comment on an existing issue or create a new one to discuss your approach
3. Fork the repository and create a feature branch
4. Submit a pull request with your implementation

### Priority Guidelines

- **High Priority**: Core functionality improvements, critical bugs, DX improvements
- **Medium Priority**: Nice-to-have features, performance optimizations
- **Low Priority**: Experimental features, advanced use cases

---

## Version History

- **v0.2.0** (2025-11-20): Bug fixes, null safety, improved error handling
- **v0.1.0**: Initial release with core component system

---

## Feedback

Have ideas for features not listed here? Please:
- Open an issue on GitHub
- Join discussions in existing feature request issues
- Contribute to the project with pull requests

Your feedback helps shape the future of Godot Components!
