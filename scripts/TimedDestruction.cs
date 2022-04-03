using Godot;
using System;

public class TimedDestruction : Node
{
    [Export]
    private float duration = 1;

    public override void _Process(float delta)
    {
        base._Process(delta);
        duration -= delta;
        if (duration <= 0)
        {
            GetParent().QueueFree();
        }
    }
}
