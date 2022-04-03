using Godot;
using System;

public class BulletFollower : Bullet
{
    Vector2 velocity = new Vector2(0, 0);
    float acceleration = 1f;
    float speed = 2000f;

    public override void _Ready()
    {
        base._Ready();

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
        base._Process(delta);

        if (dodger == null) return;

        Vector2 direction = Position.DirectionTo(dodger.Position);
        velocity = velocity.MoveToward(direction * speed, acceleration);
        Position += velocity * delta;
    }

    public void LandedHit()
    {
        QueueFree();
    }
}
