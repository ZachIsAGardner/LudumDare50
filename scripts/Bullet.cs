using Godot;
using System;

public class Bullet : Node2D
{
    Dodger dodger = null;

    Vector2 velocity = new Vector2(0, 0);
    float acceleration = 1f;
    float speed = 2000f;

    Area2D area;

    public override void _Ready()
    {
        base._Ready();

        area = this.GetChildWithType<Area2D>();
        area.Connect("area_entered", this, nameof(OnAreaEntered));
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area.Name.Contains("Wall"))
        {
            QueueFree();
        }
    }

    public override void _Process(float delta)
    {
        if (dodger == null) dodger = Owner.GetChildWithNameInHierarchy("Dodger") as Dodger;
        if (dodger == null) return;

        Vector2 direction = Position.DirectionTo(dodger.Position);
        velocity = velocity.MoveToward(direction * speed, acceleration);
        Position += velocity * delta;


        base._Process(delta);
    }

    public void LandedHit()
    {
        QueueFree();
    }
}
