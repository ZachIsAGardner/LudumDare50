using Godot;
using System;

public class BulletDowner : Bullet
{
    [Export]
    public int direction = 1;

    Vector2 velocity;
    float speed = 500f;

    bool didInit = false;

    public override void _Ready()
    {
        base._Ready();

        area.Connect("area_entered", this, nameof(OnAreaEntered));
    }

    public void OnAreaEntered(Area2D area)
    {
        if ((area.Name.Contains("WallAreaGround") && direction == 1) || (area.Name.Contains("WallAreaTop") && direction == -1))
        {
            QueueFree();
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (!didInit)
        {
            velocity.y = 200f * -direction;
            didInit = true;
        }
        sprite.FlipV = direction == -1;
        velocity.y += speed * direction * delta;
        Position += velocity * delta;
    }
}
