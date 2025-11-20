using Godot;

namespace TyEvCo.Addons.GodotComponents;

public static class NodeExtensions
{
    /// <summary>
    /// Gets a child node of the specified type at the given path.
    /// Returns null if the node doesn't exist or is not of the correct type.
    /// </summary>
    /// <typeparam name="T">The expected type of the node</typeparam>
    /// <param name="node">The parent node to search from</param>
    /// <param name="nodePathString">The path to the child node</param>
    /// <returns>The node cast to type T, or null if not found or wrong type</returns>
    public static T GetNode<T>(this Node node, string nodePathString)
        where T : Node
    {
        // NodePath construction is lightweight, no caching needed
        // This avoids memory leaks from a static cache that never clears
        var nodePath = new NodePath(nodePathString);

        if (!node.HasNode(nodePath))
            return null;

        var retrievedNode = node.GetNode(nodePath);

        // Explicit type check with error logging for debugging
        if (retrievedNode is T typedNode)
            return typedNode;

        // Log error when node exists but is wrong type
        GD.PrintErr($"Node '{nodePathString}' exists but is not of type {typeof(T).Name}, " +
                    $"it is {retrievedNode.GetType().Name}");
        return null;
    }

    /// <summary>
    /// Tries to get a child node of the specified type at the given path.
    /// </summary>
    /// <typeparam name="T">The expected type of the node</typeparam>
    /// <param name="node">The parent node to search from</param>
    /// <param name="nodePathString">The path to the child node</param>
    /// <param name="childNode">The retrieved node, or null if not found</param>
    /// <returns>True if the node was found and is of the correct type</returns>
    public static bool TryGetNode<T>(this Node node, string nodePathString, out T childNode)
        where T : Node
    {
        childNode = GetNode<T>(node, nodePathString);

        return childNode != null;
    }
}