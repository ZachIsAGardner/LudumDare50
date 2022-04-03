using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using Godot;
using System;

public static class NodeExtensions
{
    public static List<Node> GetHierarchy(this Node node)
    {
        List<Node> children = new List<Node>() { };

        children = node.GetChildren().ToList<Node>();

        Action<Node> recursive = null;
        recursive = (Node child) =>
        {
            List<Node> childsChildren = child.GetChildren().ToList<Node>();
            children.AddRange(childsChildren);
            foreach (Node childsChild in childsChildren) recursive(childsChild);
        };

        foreach (Node child in children.ToList()) recursive(child);

        return children;
    }

    public static T GetChildWithTypeInHierarchy<T>(this Node node) where T : class
    {
        return node.GetChildrenWithTypeInHierarchy<T>().FirstOrDefault();
    }

    public static List<T> GetChildrenWithTypeInHierarchy<T>(this Node node) where T : class
    {
        return node.GetHierarchy().Where(x => x.GetType() == typeof(T)).Select(x => x as T).ToList<T>();
    }

    public static Node GetChildWithNameInHierarchy(this Node node, string name)
    {
        return node.GetHierarchy().Find(x => x.Name == name);
    }

    public static T GetChildWithType<T>(this Node node)
    {
        return node.GetChildren().OfType<T>().FirstOrDefault();
    }

    public static List<T> GetChildrenWithType<T>(this Node node)
    {
        return node.GetChildren().OfType<T>().ToList();
    }

    public static T GetChildWithName<T>(this Node node, string name) where T : Node
    {
        return node.GetChildren().ToList<T>().FirstOrDefault(c => c.Name == name);
    }

    public static void RemoveChildren(this Node node)
    {
        foreach (Node child in node.GetChildren()) child.QueueFree();;
    }
}