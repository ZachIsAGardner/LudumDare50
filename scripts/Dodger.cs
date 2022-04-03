using Godot;
using System;
using System.Linq;

public class Dodger : KinematicBody2D
{
    [Export]
    private float speed = 350f;
    public Sprite sprite;
    private Area2D area;

    public bool IsInControl = true;

    private float invincibleTime = 0;

    public override void _Ready()
    {
        base._Ready();

        sprite = this.GetChildWithType<Sprite>();
        area = this.GetChildWithTypeInHierarchy<Area2D>();
        area.Connect("area_entered", this, nameof(OnAreaEntered));
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        invincibleTime -= delta;

        if (invincibleTime <= 0)
        {
            sprite.Modulate = new Color(1, 1, 1, 1);
        }

        if (!IsInControl) return;

        Vector2 input = new Vector2(0, 0);

        if (Input.IsActionPressed("ui_left")) input.x = -1;
        if (Input.IsActionPressed("ui_right")) input.x = 1;
        if (Input.IsActionPressed("ui_up")) input.y = -1;
        if (Input.IsActionPressed("ui_down")) input.y = 1;

        // if (input.x != 0) Sprite.FlipH = input.x == -1;

        MoveAndSlide(input * speed);
    }

    // ---

    public void OnAreaEntered(Area2D area)
    {
        if (invincibleTime > 0) return;

        if (area.Name.Contains("Bullet"))
        {
            Game.PlaySfx("Hurt");
            (area.GetParent() as Bullet).LandedHit();
            sprite.Modulate = new Color(1, 0.5f, 0.5f, 0.5f);
            Game.health -= 1;
            invincibleTime = 1;
        }
    }
}
