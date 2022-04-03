using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ArenaCrestfallen1 : Arena
{
    float shootTime = 0.5f;
    List<float> shootTimes = new List<float>() { 0.5f, 0.25f, 0.75f, 0.25f, 0.5f };
    PackedScene downerPrefab = ResourceLoader.Load<PackedScene>("res://prefabs/BulletDowner.tscn");
    List<Node2D> spawns = new List<Node2D>() { };
    int count = new List<int>() { 8, 9, 10, 11, 12 }.Random();

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

            BulletDowner downer = (BulletDowner)downerPrefab.Instance();
            AddChild(downer);
            downer.Position = spawn.Position;
            downer.direction = -Math.Sign(spawn.Position.y);
        }
    }
}
