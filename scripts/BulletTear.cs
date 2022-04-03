using Godot;
using System;
using System.Collections.Generic;

public class BulletTear : Bullet
{
    [Export]
    public Vector2 direction = new Vector2(1, 0);

    Vector2 velocity;
    float speed = new List<float>() { 100f, 200f, 300f, 400f, 500f }.Random();
    float timeOffset = new List<float>() { 0.25f, 0.5f, 0.75f, 1, 0 }.Random();

    bool didInit = false;

    Vector2 startPosition = new Vector2();

    public override void _Ready()
    {
        base._Ready();

        area.Connect("area_entered", this, nameof(OnAreaEntered));
    }

    public void OnAreaEntered(Area2D area)
    {
        if (
            (area.Name.Contains("WallAreaRight") && direction.x > 0)
                || (area.Name.Contains("WallAreaLeft") && direction.x < 0)
                || (area.Name.Contains("WallAreaTop") && direction.y < 0)
                || (area.Name.Contains("WallAreaGround") && direction.y > 0)
        )
        {
            QueueFree();
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (!didInit)
        {
            startPosition = Position;
            didInit = true;
            velocity = -(direction * speed) / 4f;
        }
        velocity = velocity.MoveToward(direction * speed, 6f);
        Position += velocity * delta;
        float rotation = (float)Math.Atan2(direction.y, direction.x);
        sprite.Rotation = rotation;
    }
}
