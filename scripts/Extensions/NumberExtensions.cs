using System;
using System.Collections.Generic;
using System.Text;
using Godot;

public static class NumberExtensions
{
    // Rounding 

    public static int ToNearest(this int i, int nearest)
    {
        return ((int)Math.Round(i / (double)nearest)) * nearest;
    }

    public static double ToNearest(this double i, double nearest)
    {
        return Math.Round(i / nearest) * nearest;
    }

    public static float ToNearest(this float i, float nearest)
    {
        return (float)(Math.Round(i / nearest) * nearest);
    }

    public static Vector2 ToNearest(this Vector2 vector2, int nearest)
    {
        return new Vector2(
            (float)(Math.Round(vector2.x / nearest) * nearest),
            (float)(Math.Round(vector2.y / nearest) * nearest)
        );
    }

    public static Vector2 RoundTo(this Vector2 i)
    {
        return new Vector2(
            i.x.RoundTo(),
            i.y.RoundTo()
        );
    }

    public static Vector2 RoundTo(this Vector2 i, int place)
    {
        return new Vector2(
            i.x.RoundTo(place),
            i.y.RoundTo(place)
        );
    }

    public static float RoundTo(this float i)
    {
        return (float)Math.Round(i, 0);
    }

    public static float RoundTo(this float i, int place)
    {
        return (float)Math.Round(i, place);
    }

    public static float Floor(this float i)
    {
        return (float)Math.Floor(i);
    }

    public static float Ceiling(this float i)
    {
        return (float)Math.Ceiling(i);
    }

    // Abs

    public static int Abs(this int num)
    {
        return Math.Abs(num);
    }

    public static float Abs(this float num)
    {
        return Math.Abs(num);
    }

    public static Vector2 Abs(this Vector2 vector2)
    {
        return new Vector2(
            Math.Abs(vector2.x),
            Math.Abs(vector2.y)
        );
    }

    // Sign

    public static float Sign(this float num)
    {
        return Math.Sign(num);
    }

    public static Vector2 Sign(this Vector2 vector2)
    {
        return new Vector2(
            Math.Sign(vector2.x),
            Math.Sign(vector2.y)
        );
    }

    // Random

    public static int Random(this Vector2 vector2)
    {
        Random r = new Random();
        int result = r.Next((int)vector2.x, (int)vector2.y + 1);
        return result;
    }

    // Distance

    public static float Distance(this Vector2 vector2, Vector2 other)
    {
        return (vector2.x - other.x).Abs() + (vector2.y - other.y).Abs();
    }
}

