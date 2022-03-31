using Godot;

public class Player : KinematicBody
{
    private float speed = 10f;

    public override void _Process(float delta)
    {
        base._Process(delta);

        Vector3 input = new Vector3(0, 0, 0);

        if (Input.IsActionPressed("ui_left")) input.x = -1;
        else if (Input.IsActionPressed("ui_right")) input.x = 1;

        if (Input.IsActionPressed("ui_up")) input.z = -1;
        else if (Input.IsActionPressed("ui_down")) input.z = 1;

        MoveAndSlide(new Vector3(input.x * speed, 0, input.z * speed), Vector3.Up);
    }
}