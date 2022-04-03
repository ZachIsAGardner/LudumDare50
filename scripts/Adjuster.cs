using Godot;
using System;

[Tool]
public class Adjuster : Node
{
    Node2D parent;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        parent = parent ?? GetParent() as Node2D;
        if (parent == null) return;
        parent.ZIndex = (int)Math.Round(parent.Position.y / 10f);
    }
}
