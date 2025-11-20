# Godot Components

A C# library that makes it easy to register custom Node classes with the Godot editor using a simple `[Component]` attribute. Your custom components will automatically appear in the "Create New Node" dialog with optional custom icons.

## Features

- 🎨 Automatically adds C# classes as custom nodes in the Godot editor
- 🔧 Simple attribute-based registration with `[Component]`
- 🖼️ Support for custom icons for your component nodes
- 🔍 Includes helpful `NodeExtensions` utilities for improved node access
- ⚡ Minimal setup required
- 🛡️ Comprehensive error handling and null safety

## Requirements

- Godot 4.x with .NET support
- C# / .NET 6.0 or later
- C# support enabled in Godot

## Installation

1. Copy the `addons/godot-components` folder into your project's `addons` directory
2. Enable the plugin in Godot:
   - Go to **Project → Project Settings → Plugins**
   - Enable "Godot-Components"
3. Reload your project

## Usage

### Basic Component

Create a new C# script that inherits from any Node class and add the `[Component]` attribute:

```csharp
using Godot;
using TyEvCo.Addons.GodotComponents;

[Component]
public partial class MyCustomComponent : Node
{
    [Export] public string Message { get; set; } = "Hello World";

    public override void _Ready()
    {
        GD.Print(Message);
    }
}
```

After building your project, `MyCustomComponent` will appear in the "Create New Node" dialog.

### Component with Custom Icon

You can specify a custom icon for your component:

```csharp
[Component(iconPath: "res://path/to/your/icon.png")]
public partial class MyCustomComponent : Node
{
    // Your component implementation
}
```

### NodeExtensions Utilities

The plugin includes extension methods for improved node access:

```csharp
using TyEvCo.Addons.GodotComponents;

// Get a typed child node
var player = this.GetNode<Player>("Path/To/Player");

// Try to get a node (returns bool)
if (this.TryGetNode<Enemy>("Enemy", out var enemy))
{
    enemy.TakeDamage(10);
}
```

## How It Works

The plugin uses reflection to scan your assembly for classes decorated with the `[Component]` attribute. It validates that they inherit from `Node` and automatically registers them with the Godot editor using the `AddCustomType` API.

## API Reference

### `[Component]` Attribute

**Parameters:**
- `scriptPath` (optional): Automatically filled by the compiler using `[CallerFilePath]`
- `iconPath` (optional): Path to a custom icon texture (e.g., `"res://icons/my_icon.png"`)

**Example:**
```csharp
[Component(iconPath: "res://addons/my-plugin/icon.png")]
public partial class MyComponent : Node { }
```

## Examples

See the `samples/GodotComponents/scripts` directory for example components:
- **SelectableComponent**: Demonstrates a component with exportable properties and validation
- **FollowableComponent**: Placeholder example showing the component structure

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for version history and updates.

## Recent Improvements (v0.2)

- **Fixed critical path calculation bug**: Now properly handles different folder structures
- **Added null safety**: Comprehensive null checking prevents runtime crashes
- **Improved error logging**: Clear, actionable error messages for debugging
- **Fixed memory leak**: Removed static cache that caused memory growth
- **Better type checking**: Enhanced validation with helpful error messages
- **Added documentation**: XML comments on all public APIs

## Contributing

Contributions are welcome! Please ensure your code:
- Follows C# naming conventions
- Includes XML documentation comments
- Handles null cases appropriately
- Includes error logging where appropriate

## License

MIT License - See [LICENSE](LICENSE) file for details

Copyright (c) 2023 Tyler Coles

## Version

v0.2 - Bug fixes and stability improvements
