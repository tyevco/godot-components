using Godot;
using TyEvCo.Addons.GodotComponents;

namespace TyEvCo.Samples.GodotComponents.Scripts;

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