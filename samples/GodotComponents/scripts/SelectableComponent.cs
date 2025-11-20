using Godot;
using TyEvCo.Addons.GodotComponents;

namespace TyEvCo.Samples.GodotComponents.Scripts;

/// <summary>
/// Component that allows an object to be selected, showing/hiding an outline mesh.
/// </summary>
[Component]
public partial class SelectableComponent : Node
{
    private MeshInstance3D _outlineMesh;

    /// <summary>
    /// The mesh instance that represents the selection outline.
    /// Must be assigned in the editor.
    /// </summary>
    [Export]
    public MeshInstance3D OutlineMesh
    {
        get => _outlineMesh;
        set
        {
            if (value == null)
            {
                GD.PrintErr("SelectableComponent: OutlineMesh cannot be null");
            }
            _outlineMesh = value;
        }
    }

    private bool _selected;

    /// <summary>
    /// Gets or sets whether this object is currently selected.
    /// </summary>
    public bool Selected
    {
        get => _selected;
        set => SetSelected(value);
    }

    public override void _Ready()
    {
        // Validate that OutlineMesh is assigned
        if (OutlineMesh == null)
        {
            GD.PrintErr("SelectableComponent: OutlineMesh is not assigned! Please assign it in the editor.");
        }
    }

    /// <summary>
    /// Sets the selected state and updates the outline visibility.
    /// </summary>
    /// <param name="value">True to select, false to deselect</param>
    public void SetSelected(bool value)
    {
        _selected = value;

        if (OutlineMesh == null)
        {
            GD.PrintErr("SelectableComponent: Cannot set selected state - OutlineMesh is not assigned!");
            return;
        }

        OutlineMesh.Visible = value;
    }

    /// <summary>
    /// Gets whether this object is currently selected.
    /// </summary>
    /// <returns>True if selected, false otherwise</returns>
    public bool IsSelected()
    {
        return _selected;
    }
}