using Godot;
using System;

public class GameOver : Control
{
    public override void _Process(float delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("ui_accept"))
        {
            Game.LoadLevel("Main");
            Game.health = Game.maxHealth;
        }
    }
}
