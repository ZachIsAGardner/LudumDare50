using Godot;
using System;
using System.Collections.Generic;

public class BulletNumber : Bullet
{
    [Export]
    public int direction = 1;

    Vector2 velocity;
    float speed = 100f;
    float timeOffset = new List<float>() { 0.25f, 0.5f, 0.75f, 1, 0 }.Random();

    bool didInit = false;

    Vector2 startPosition = new Vector2();

    public override void _Ready()
    {
        base._Ready();

        area.Connect("area_entered", this, nameof(OnAreaEntered));

        string s = new List<string>()
        {
            "NumberOne",
            "NumberTwo",
            "NumberThree"
        }.Random();

        sprite.Texture = ResourceLoader.Load<Texture>($"res://sprites/{s}.png");
    }

    public void OnAreaEntered(Area2D area)
    {
        if ((area.Name.Contains("WallAreaRight") && direction == 1) || (area.Name.Contains("WallAreaLeft") && direction == -1))
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
        }
        velocity.x += speed * direction * delta;
        Position += velocity * delta;
        Vector2 destination = Position;
        destination = new Vector2(destination.x, startPosition.y + (Mathf.Sin((Game.elapsed + timeOffset) * 2) * 64));
        Position = Position.MoveToward(destination, 2f);
    }
}
