using Godot;
using System;

public class ArenaApple1 : Arena
{
    bool done = false;

    public override bool Done()
    {
        return done;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (this.GetChildrenWithType<Bullet>().Count <= 0)
        {
            done = true;
        }
    }
}
