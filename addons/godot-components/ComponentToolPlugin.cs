#if TOOLS
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Godot;

namespace TyEvCo.Addons.GodotComponents;

[Tool]
public partial class ComponentToolPlugin : EditorPlugin
{
    private const string DEFAULT_ICON_PATH = "res://addons/godot-components/component_white.png";

    private static Texture2D _defaultTexture;

    private List<Type> LoadedTypes { get; set; } = new List<Type>();
    private string ProjectRoot { get; set; }

    public override void _EnterTree()
    {
        // Load default texture with null check
        _defaultTexture = GD.Load<Texture2D>(DEFAULT_ICON_PATH);
        if (_defaultTexture == null)
        {
            GD.PrintErr($"Failed to load default component icon texture at: {DEFAULT_ICON_PATH}");
        }

        ProjectRoot = GetProjectRoot();
        var componentTypes = ScanAssembly(typeof(ComponentToolPlugin).Assembly);
        foreach (var type in componentTypes)
            AddCustomType(type);
    }


    public override void _ExitTree()
    {
        // Clean-up of the plugin goes here.
        // Always remember to remove it from the engine when deactivated.
        foreach (var type in LoadedTypes)
            RemoveCustomType(type);
    }

    private IEnumerable<Type> ScanAssembly(Assembly assembly)
    {
        return assembly.ExportedTypes.Where(x => x.GetCustomAttribute<ComponentAttribute>() != null);
    }

    private void AddCustomType(Type type)
    {
        // ScanAssembly() already filters for ComponentAttribute, so this is guaranteed to exist
        var componentAttribute = type.GetCustomAttribute<ComponentAttribute>();

        // Validate that the type inherits from Node
        bool isValid = false;
        Type inspectType = type.BaseType;
        while (inspectType != null)
        {
            if (inspectType.Equals(typeof(Node)))
            {
                isValid = true;
                break;
            }

            inspectType = inspectType.BaseType;
        }

        if (!isValid)
        {
            GD.PrintErr($"Component {type.Name} does not inherit from Node");
            return;
        }

        // Validate base type
        var baseTypeName = type.BaseType?.Name;
        if (string.IsNullOrEmpty(baseTypeName))
        {
            GD.PrintErr($"Component {type.Name} has no valid base type");
            return;
        }

        // Load icon with proper error handling
        var icon = _defaultTexture;
        if (!string.IsNullOrWhiteSpace(componentAttribute.IconPath))
        {
            var customIcon = GD.Load<Texture2D>(componentAttribute.IconPath);
            if (customIcon != null)
            {
                icon = customIcon;
            }
            else
            {
                GD.PrintErr($"Failed to load component icon: {componentAttribute.IconPath}");
            }
        }

        var typeName = type.Name;

        // Validate and process script path with bounds checking
        var scriptPath = componentAttribute.ScriptPath;
        if (string.IsNullOrEmpty(scriptPath))
        {
            GD.PrintErr($"Component {typeName} has no script path");
            return;
        }

        if (!scriptPath.StartsWith(ProjectRoot))
        {
            GD.PrintErr($"Script path is not within project root: {scriptPath}");
            return;
        }

        var relativePath = scriptPath[ProjectRoot.Length..].TrimStart('/', '\\').Replace("\\", "/");

        // Load script with null check
        var script = GD.Load<Script>($"res://{relativePath}");
        if (script == null)
        {
            GD.PrintErr($"Failed to load script for component {typeName}: res://{relativePath}");
            return;
        }

        AddCustomType(typeName, baseTypeName, script, icon);

        LoadedTypes.Add(type);
    }

    private void RemoveCustomType(Type type)
    {
        RemoveCustomType(type.Name);
    }

    private string GetProjectRoot([CallerFilePath] string path = null)
    {
        // Dynamically calculate project root by navigating up from the plugin directory
        // Expected structure: <project_root>/addons/godot-components/ComponentToolPlugin.cs
        var pluginDir = Path.GetDirectoryName(path);

        // Navigate up to find project root (parent of 'addons' directory)
        var currentDir = new DirectoryInfo(pluginDir);

        // Go up until we find the 'addons' directory or reach the root
        while (currentDir != null && currentDir.Name != "addons")
        {
            currentDir = currentDir.Parent;
        }

        if (currentDir == null)
        {
            GD.PrintErr($"Could not find 'addons' directory in path: {path}");
            return pluginDir; // Fallback to plugin directory
        }

        // Return the parent of the 'addons' directory (the project root)
        var projectRoot = currentDir.Parent?.FullName;

        if (projectRoot == null)
        {
            GD.PrintErr("Could not determine project root");
            return pluginDir; // Fallback to plugin directory
        }

        return projectRoot;
    }
}
#endif