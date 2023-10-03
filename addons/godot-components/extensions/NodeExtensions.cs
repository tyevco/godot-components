using System.Collections.Generic;
using Godot;

namespace tyevco.addons.godot_components;

public static class NodeExtensions
{
    private static IDictionary<string, NodePath> NodePathMap = new Dictionary<string, NodePath>();

    public static T GetNode<T>(this Node node, string nodePathString)
        where T : Node
    {
        if (!NodePathMap.TryGetValue(nodePathString, out var nodePath))
        {
            nodePath = new NodePath(nodePathString);
            NodePathMap[nodePathString] = nodePath;
        }

        if (node.HasNode(nodePath))
            return node.GetNode(nodePath) as T;

        return null;
    }

    public static bool TryGetNode<T>(this Node node, string nodePathString, out T childNode)
        where T : Node
    {
        childNode = GetNode<T>(node, nodePathString);

        return childNode != null;
    }
}