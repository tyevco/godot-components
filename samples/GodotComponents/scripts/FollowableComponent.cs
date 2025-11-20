using Godot;
using TyEvCo.Addons.GodotComponents;

namespace TyEvCo.Samples.GodotComponents.Scripts;

/// <summary>
/// Example component that marks a node as followable.
/// This is a placeholder demonstrating the Component system.
///
/// TODO: Implement following logic, such as:
/// - Target position for followers
/// - Follow speed/distance parameters
/// - Update notifications for followers
/// </summary>
[Component]
public partial class FollowableComponent : Node
{
    // Example implementation could include:
    // [Export] public float FollowDistance { get; set; } = 5.0f;
    // [Export] public bool AllowFollowing { get; set; } = true;

    // public Vector3 GetFollowPosition()
    // {
    //     return GlobalPosition;
    // }
}