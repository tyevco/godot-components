#if TOOLS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Godot;

namespace tyevco.addons.godot_components;

[Tool]
public partial class ComponentToolPlugin : EditorPlugin
{
    private static readonly Texture2D Texture = GD.Load<Texture2D>("res://addons/godot-components/component_white.png");

    private List<Type> LoadedTypes { get; set; } = new List<Type>();
    private string ProjectRoot { get; set; }

    public override void _EnterTree()
    {
        var componentTypes = ScanAssembly(typeof(ComponentToolPlugin).Assembly);
        ProjectRoot = GetProjectRoot();
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
        var componentAttribute = type.GetCustomAttribute<ComponentAttribute>();

        if (componentAttribute == null)
            return;

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
            return;

        var icon = Texture;
        if (!string.IsNullOrWhiteSpace(componentAttribute.IconPath))
            icon = GD.Load<Texture2D>(componentAttribute.IconPath);

        var typeName = type.Name;

        var scriptPath = componentAttribute.ScriptPath[(ProjectRoot.Length)..].Replace("\\", "/");
        //GD.Print(scriptPath);
        AddCustomType(typeName, type.BaseType?.Name, GD.Load<Script>($"res://{scriptPath}"),
            icon);

        LoadedTypes.Add(type);
    }

    private void RemoveCustomType(Type type)
    {
        RemoveCustomType(type.Name);
    }

    private string GetProjectRoot([CallerFilePath] string path = null)
    {
        return path[0..^46];
    }
}
#endif