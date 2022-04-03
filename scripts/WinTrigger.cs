using Godot;
using System;

public class WinTrigger : Area2D
{
    public override void _Ready()
    {
        base._Ready();

        Connect("body_entered", this, nameof(OnBodyEntered));
    }

    public void OnBodyEntered(Node body)
    {
        if (body.Name == "Player")
        {
            Game.LoadLevel("Win");
        }
    }
}
