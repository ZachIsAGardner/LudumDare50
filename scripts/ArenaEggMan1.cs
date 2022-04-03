using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ArenaEggMan1 : Arena
{
    float shootTime = 0.5f;
    List<float> shootTimes = new List<float>() { 0.5f, 0.25f, 0.75f, 0.25f, 0.5f };
    PackedScene eggPrefab = ResourceLoader.Load<PackedScene>("res://prefabs/BulletEgg.tscn");
    List<Node2D> spawns = new List<Node2D>() { };
    int count = new List<int>() { 4, 5, 6, 7, 8 }.Random();

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

            BulletEgg egg = (BulletEgg)eggPrefab.Instance();
            AddChild(egg);
            egg.Position = spawn.Position;
            egg.direction = -Math.Sign(spawn.Position.x);
        }
    }
}
