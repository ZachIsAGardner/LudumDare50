using Godot;
using System;

public class BulletEgg : Bullet
{
    [Export]
    public int direction = 1;

    Vector2 velocity;
    float gravity = 500f;

    string state = "Fall";
    float shatterTime = 1f;

    public override void _Ready()
    {
        base._Ready();

        area.Connect("area_entered", this, nameof(OnAreaEntered));
        velocity.y = -200f;
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area.Name.Contains("WallAreaGround"))
        {
            state = "Shatter";
            sprite.Texture = ResourceLoader.Load<Texture>("res://sprites/BulletEggShattered.png");

            PackedScene birdPrefab = ResourceLoader.Load<PackedScene>("res://prefabs/BulletBird.tscn");
            BulletBird bird = (BulletBird)birdPrefab.Instance();
            Node parent = GetParent();
            parent.CallDeferred("add_child", bird);
            bird.direction = new Vector2(direction, -1);
            bird.Position = Position;
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (state == "Fall")
        {
            velocity.y += gravity * delta;
            velocity.x = 200 * direction;
            Position += velocity * delta;
        }
        else if (state == "Shatter")
        {
            shatterTime -= delta;
            if (shatterTime <= 0)
            {
                QueueFree();
            }
        }
    }
}
