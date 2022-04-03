using Godot;
using System;

public abstract class Bullet : Node2D
{
    public Sprite sprite = null;
    public Area2D area = null;
    public Dodger dodger = null;

    public override void _Ready()
    {
        base._Ready();

        sprite = this.GetChildWithType<Sprite>();
        area = this.GetChildWithType<Area2D>();
        dodger = GetParent().GetChildWithNameInHierarchy("Dodger") as Dodger;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
    }

    public virtual void LandedHit()
    {
        QueueFree();
    }
}
