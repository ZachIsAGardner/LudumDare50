using Godot;
using System;

public class Destructible : Area2D
{
    public override void _Ready()
    {
        base._Ready();

        Connect("area_entered", this, nameof(OnAreaEntered));
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area.Name.Contains("Destructor"))
        {
            Game.PlaySfx("Hurt");
            GetParent().QueueFree();
        }
    }
}
