using Godot;
using System;

public class Fade : Panel
{
    public Action MiddleAction = null;

    private Panel panel;
    [Export]
    private bool fadingIn = true;
    private Color color = new Color(0, 0, 0, 0);
    private float wait = 0.5f;
    private float speed = 2;
    private bool didDelete = false;

    public override void _Ready()
    {
        base._Ready();
        panel = this as Panel;
        if (fadingIn) color = new Color(0, 0, 0, 0);
        else color = new Color(0, 0, 0, 1);
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        panel.AddStyleboxOverride("panel", new StyleBoxFlat() { BgColor = color });

        if (fadingIn)
        {
            color.a += speed * delta;
            if (color.a >= 1)
            {
                fadingIn = false;
                if (MiddleAction != null)
                {
                    MiddleAction();
                }
            }
        }
        else
        {
            wait -= delta;
            if (wait <= 0)
            {
                color.a -= speed * delta;
                if (color.a <= 0 && !didDelete)
                {
                    didDelete = true;
                    QueueFree();
                }
            }
        }
    }
}
