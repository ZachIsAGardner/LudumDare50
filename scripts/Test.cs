using Godot;
using System;
using System.Threading.Tasks;

public class Test : Control
{
    public override void _Ready()
    {
        base._Ready();
        // _ = Thing();
    }

    // async Task Thing() 
    // {
    //     while (true)
    //     {
    //         if (Input.IsActionJustPressed("ui_accept"))
    //         {
    //             GD.Print("ACCEPT PRESSED");
    //             break;
    //         }
    //         await Async.WaitForMilliseconds(1);
    //     }
    //     GD.Print("REACHED END OF ASYNC FUNCTION");
    // }
    public override void _Process(float delta)
    {
        base._Process(delta);
        if (Input.IsActionJustPressed("ui_accept"))
        {
            GD.Print("ACCEPT PRESSED");
        }
    }
}
