using Godot;
using System;
using System.Linq;

public class Player : KinematicBody2D
{
    [Export]
    private float speed = 350f;
    public Sprite sprite;

    public bool IsInControl = true;

    public override void _Ready()
    {
        base._Ready();

        sprite = this.GetChildWithType<Sprite>();
        Game.player = this;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (!IsInControl) return;

        Vector2 input = new Vector2(0, 0);

        if (Input.IsActionPressed("ui_left")) input.x = -1;
        if (Input.IsActionPressed("ui_right")) input.x = 1;
        if (Input.IsActionPressed("ui_up")) input.y = -1;
        if (Input.IsActionPressed("ui_down")) input.y = 1;

        if (input.x != 0) sprite.FlipH = input.x == -1;

        MoveAndSlide(input * speed);
    }
}
