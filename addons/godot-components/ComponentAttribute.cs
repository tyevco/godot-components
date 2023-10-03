using System;
using System.Runtime.CompilerServices;

namespace tyevco.addons.godot_components;

[AttributeUsage(AttributeTargets.Class)]
public class ComponentAttribute : Attribute
{
    public string ScriptPath { get; set; }
    public string IconPath { get; set; }

    public ComponentAttribute(
        [CallerFilePath]
        string scriptPath = null,
        string iconPath = null)
    {
        ScriptPath = scriptPath;
        IconPath = iconPath;
    }
}