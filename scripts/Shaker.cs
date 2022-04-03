using Godot;
using System;

public class Shaker : Node
{
    public Control parentControl = null;
    public Sprite parentSprite = null;

    private Vector2 startPosition = new Vector2(0, 0);

    private float shakeX = 0;
    private float shakeXDuration = 0;
    private int shakeXDirection = 1;
    private float shakeCooldown = 0;
    private float magnitude = 16f;

    public override void _Ready()
    {
        base._Ready();

        parentControl = GetParent() as Control;
        if (parentControl != null)
        {
            startPosition = parentControl.RectPosition;
        }

        parentSprite = GetParent() as Sprite;
        if (parentSprite != null)
        {
            startPosition = parentSprite.Position;
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        shakeCooldown -= delta;
        shakeX -= delta;

        // Shake X

        if (shakeX > 0 && shakeCooldown <= 0)
        {
            shakeCooldown = 0.02f;
            shakeXDirection *= -1;

            if (parentControl != null)
            {
                parentControl.RectPosition = startPosition + new Vector2(((shakeX / shakeXDuration) * magnitude) * shakeXDirection, 0);
            }
            else if (parentSprite != null)
            {
                parentSprite.Position = startPosition + new Vector2(((shakeX / shakeXDuration) * magnitude) * shakeXDirection, 0);
            }
        }
    }

    public void ShakeX(float duration = 0.5f, float magnitude = 16f)
    {
        shakeX = shakeXDuration= duration;
        this.magnitude = magnitude;
        shakeCooldown = 0;
    }
}
