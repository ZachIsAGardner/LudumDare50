using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

public class Enemy : KinematicBody2D
{
    [Export] private string message = "Hello!";
    [Export] private string entry = "";

    private Sprite sprite;
    private Area2D area;

    private PackedScene confrontationLevel;

    private Player player;

    private List<Vector2> waypoints = new List<Vector2>();
    private Vector2? currentWaypoint = null;

    private string state = "Patrol";

    private float timer = 0;
    private int progress = 0;

    private bool doCutscene = false;
    private bool load = false;

    public override void _Ready()
    {
        base._Ready();

        confrontationLevel = ResourceLoader.Load<PackedScene>("res://levels/Confrontation.tscn");

        sprite = this.GetChildWithType<Sprite>();
        area = this.GetChildWithType<Area2D>();

        Node waypointContainer = this.GetChildWithName<Node>("WaypointContainer");
        if (waypointContainer != null)
        {
            waypoints = waypointContainer.GetChildrenWithType<Node2D>().Select(w => w.GlobalPosition).ToList();
        }

        if (area == null) return;
        area.Connect("body_entered", this, nameof(OnBodyEntered));
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        timer -= delta;

        if (doCutscene)
        {
            Cutscene();
        }

        if (area != null)
        {
            area.Scale = new Vector2(sprite.FlipH ? -1 : 1, 1);
        }

        if (state == "Stand")
        {

        }
        else if (state == "Patrol")
        {
            if (waypoints.Count > 0)
            {
                if (currentWaypoint == null) currentWaypoint = waypoints[0];

                Position = Position.MoveToward(currentWaypoint.Value, 5f);
                sprite.FlipH = currentWaypoint.Value.x < Position.x;

                if (Position.DistanceTo(currentWaypoint.Value) < 16)
                {
                    int index = waypoints.IndexOf(currentWaypoint.Value);
                    index++;
                    if (index >= waypoints.Count) index = 0;
                    currentWaypoint = waypoints[index];
                }
            }
        }
        else if (state == "Chase")
        {
            Position = Position.MoveToward(player.Position, 10f);
            sprite.FlipH = player.Position.x < Position.x;
            if ((Position.x - player.Position.x).Abs() < 128 && (Position.y - player.Position.y).Abs() < 128)
            {
                state = "Stand";
            }
        }
    }

    public void OnBodyEntered(Node body)
    {
        if (body.Name == "Player")
        {
            Game.PlaySfx("Notice");
            Game.PlaySong("Hello");

            state = "Stand";

            Sprite spottedInstance = (Sprite)Game.spottedPrefab.Instance();
            AddChild(spottedInstance);
            spottedInstance.Position = new Vector2(0, -192);

            player = body as Player;
            player.IsInControl = false;
            player.sprite.FlipH = player.Position.x > Position.x;

            doCutscene = true;
        }
    }

    void Cutscene()
    {
        if (timer > 0) return;

        if (progress == 0)
        {
            progress++;
            timer = 1;
            return;
        }

        if (progress == 1)
        {
            progress++;
            state = "Chase";
            return;
        }

        if (progress == 2)
        {
            if (state != "Chase") progress++;
            return;
        }

        if (progress == 3)
        {
            progress++;
            Talk.Begin(this as Node2D, message);
            return;
        }

        if (progress == 4)
        {
            Talk.WaitForInput(() => progress++);
            return;
        }

        if (progress == 5)
        {
            progress++;
            Game.Fade(() => load = true);
            return;
        }

        if (progress == 6)
        {
            if (load) progress++;
            return;
        }

        if (progress == 7)
        {
            progress++;
            Confrontation confrontationInstance = (Confrontation)confrontationLevel.Instance();
            confrontationInstance.entry = CE.GetEntry(entry);
            Control dynamicContainer = Game.canvas.GetChildWithName<Control>("DynamicContainer");
            dynamicContainer.AddChild(confrontationInstance);
            QueueFree();
            return;
        }
    }
}
