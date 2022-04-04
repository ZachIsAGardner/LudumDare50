using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Test : Control
{
    public override void _Ready()
    {
        base._Ready();
        // _ = Thing();

        foreach (var item in Thing())
        {
            GD.Print(item);        
        }
    }

    IEnumerable<int> Thing()
    {
        yield return 1;
        yield return 2;
        yield return 3;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (Input.IsActionJustPressed("ui_accept"))
        {
            GD.Print("ACCEPT PRESSED");
        }
    }
}
