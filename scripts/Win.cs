using Godot;
using System;

public class Win : Control
{
    bool didQuit = false;

    public override void _Ready()
    {
        base._Ready();

        Game.PlaySong("Win");
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("ui_accept") && !didQuit)
        {
            didQuit = true;

            Game.Fade(() =>
            {
                GetTree().Quit();
            });
        }
    }
}
