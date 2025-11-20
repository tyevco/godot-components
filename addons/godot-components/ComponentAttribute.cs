using System;
using System.Runtime.CompilerServices;

namespace TyEvCo.Addons.GodotComponents;

/// <summary>
/// Attribute that marks a class as a Godot component.
/// Classes with this attribute will be automatically registered with the Godot editor.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ComponentAttribute : Attribute
{
    /// <summary>
    /// Gets the file path of the script containing this component.
    /// Automatically populated via CallerFilePath.
    /// </summary>
    public string ScriptPath { get; }

    /// <summary>
    /// Gets the optional custom icon path for this component in the editor.
    /// </summary>
    public string IconPath { get; }

    /// <summary>
    /// Creates a new ComponentAttribute.
    /// </summary>
    /// <param name="scriptPath">The script file path (automatically filled by the compiler)</param>
    /// <param name="iconPath">Optional path to a custom icon texture</param>
    public ComponentAttribute(
        [CallerFilePath] string scriptPath = null,
        string iconPath = null)
    {
        ScriptPath = scriptPath;
        IconPath = iconPath;
    }
}