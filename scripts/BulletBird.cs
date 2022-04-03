using Godot;
using System;

public class BulletBird : Bullet
{
    [Export]
    public Vector2 direction = new Vector2(1, -1);

    Vector2 velocity;
    float speed = 100f;

    public override void _Ready()
    {
        base._Ready();

        area.Connect("area_entered", this, nameof(OnAreaEntered));
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area.Name.Contains("WallAreaTop"))
        {
            QueueFree();
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        sprite.FlipH = direction.x == -1;

        velocity = direction * speed;
        Position += velocity * delta;
    }
}
