using Godot;
using System;

public class Arena : Node
{
    protected Dodger dodger;
    protected bool done = false;
    public virtual bool Done() => done;
    float clearTime = 0;

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (dodger == null) dodger = this.GetChildWithNameInHierarchy("Dodger") as Dodger;

        if (this.GetChildrenWithType<Bullet>().Count <= 0)
        {
            clearTime += delta;

            if (clearTime >= 1.25f)
            {
                done = true;
            }
        }
        else
        {
            clearTime = 0;
        }
    }
}
