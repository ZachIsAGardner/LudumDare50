using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ArenaSchoolChild1 : Arena
{
    float shootTime = 0.5f;
    List<float> shootTimes = new List<float>() { 0.75f, 0.5f, 1f, 0.75f, 1, 0.5f };
    PackedScene numberPrefab = ResourceLoader.Load<PackedScene>("res://prefabs/BulletNumber.tscn");
    List<Node2D> spawns = new List<Node2D>() { };
    int count = new List<int>() { 4, 6, 8, 10 }.Random();

    public override void _Ready()
    {
        base._Ready();
        spawns = this.GetHierarchy().Where(x => x.Name.Contains("Spawn")).Select(x => x as Node2D).ToList();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        shootTime -= delta;

        if (shootTime <= 0 && count > 0)
        {
            count--;

            shootTime = shootTimes.Random();

            Node2D spawn = spawns.Random();

            BulletNumber number = (BulletNumber)numberPrefab.Instance();
            AddChild(number);
            number.Position = spawn.Position;
            number.direction = -Math.Sign(spawn.Position.x);
        }
    }
}
