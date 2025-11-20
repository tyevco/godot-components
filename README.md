# Godot Components

A Godot plugin that enables a component-based architecture by automatically registering custom node types to the "Create New Node" dialog in the Godot editor.

## Features

- 🎨 Automatically adds C# classes as custom nodes in the Godot editor
- 🔧 Simple attribute-based registration with `[Component]`
- 🖼️ Support for custom icons for your component nodes
- 🔍 Includes helpful `NodeExtensions` utilities for improved node access
- ⚡ Minimal setup required

## Requirements

- Godot 4.x with .NET support
- C# / .NET enabled project

## Installation

1. Copy the `addons/godot-components` folder into your project's `addons` directory
2. Enable the plugin in Godot:
   - Go to **Project → Project Settings → Plugins**
   - Enable "Godot-Components"

## Usage

### Basic Component

Create a custom component by inheriting from any class that derives from `Node` and adding the `[Component]` attribute:

```csharp
using Godot;
using TyEvCo.Addons.GodotComponents;

[Component]
public partial class SelectableComponent : Node
{
    [Export] public MeshInstance3D OutlineMesh { get; set; }

    private bool _selected;

    public void SetSelected(bool value)
    {
        _selected = value;
        OutlineMesh.Visible = value;
    }

    public bool IsSelected()
    {
        return _selected;
    }
}
```

After building your project, `SelectableComponent` will appear in the "Create New Node" dialog.

### Custom Icon

You can specify a custom icon for your component:

```csharp
[Component(iconPath: "res://path/to/your/icon.png")]
public partial class MyComponent : Node
{
    // Your component code
}
```

### NodeExtensions Utilities

The plugin includes extension methods for improved node access:

```csharp
using TyEvCo.Addons.GodotComponents;

// Get a child node with caching
var player = GetNode<Player>("Path/To/Player");

// Try get pattern
if (TryGetNode<Enemy>("Path/To/Enemy", out var enemy))
{
    enemy.TakeDamage(10);
}
```

## How It Works

The plugin scans your project's assembly for classes marked with the `[Component]` attribute that inherit from `Node`. It then automatically registers them as custom types in the Godot editor, making them available in the "Create New Node" dialog.

## Examples

See the `samples/GodotComponents/scripts` directory for example components:
- `SelectableComponent.cs` - A component for managing selection state
- `FollowableComponent.cs` - A minimal component example

## License

MIT License - See [LICENSE](LICENSE) file for details

Copyright (c) 2023 Tyler Coles

## Version

v0.1.0 - Initial release