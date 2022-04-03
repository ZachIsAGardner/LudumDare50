using Godot;
using System;
using System.Threading.Tasks;

public class Game : Node
{
    public static bool skipIntro = true;

    private static Node instance;

    public static Player player;

    public static float health = 6;
    public static readonly float maxHealth = 6;

    public static CanvasLayer canvas;

    public static PackedScene spottedPrefab;
    public static PackedScene textboxPrefab;
    public static PackedScene floatingTextboxPrefab;
    public static PackedScene fadePrefab;
    public static PackedScene sfxPrefab;
    public static PackedScene arenaApple1Prefab;

    // Talk

    private static Textbox textbox;

    public override void _Ready()
    {
        base._Ready();

        instance = this;

        canvas = GetTree().Root.GetChildWithType<CanvasLayer>();

        spottedPrefab = ResourceLoader.Load<PackedScene>("res://prefabs/Spotted.tscn");
        textboxPrefab = ResourceLoader.Load<PackedScene>("res://prefabs/Textbox.tscn");
        floatingTextboxPrefab = ResourceLoader.Load<PackedScene>("res://prefabs/FloatingTextbox.tscn");
        fadePrefab = ResourceLoader.Load<PackedScene>("res://prefabs/Fade.tscn");
        sfxPrefab = ResourceLoader.Load<PackedScene>("res://prefabs/Sfx.tscn");
        sfxPrefab = ResourceLoader.Load<PackedScene>("res://prefabs/Sfx.tscn");
        arenaApple1Prefab = ResourceLoader.Load<PackedScene>("res://prefabs/ArenaApple1.tscn");
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
    }

    public static void Fade(Action middleAction)
    {
        Fade fade = (Fade)Game.fadePrefab.Instance();
        canvas.GetChildWithName<Node>("FadeContainer").AddChild(fade);

        fade.MiddleAction = middleAction;
    }

    public static void LoadLevel(string name)
    {
        Fade(() =>
        {
            Node level = instance.GetTree().Root.GetNode("Level");
            instance.GetTree().Root.RemoveChild(level);
            level.CallDeferred("free");

            canvas.GetChildWithName<Node>("DynamicContainer").RemoveChildren();

            string path = $"res://levels/{name}.tscn";
            PackedScene newLevel = ResourceLoader.Load<PackedScene>(path);
            instance.GetTree().Root.AddChild(newLevel.Instance());
        });
    }

    public static void PlaySfx(string name)
    {
        AudioStreamPlayer sfx = sfxPrefab.Instance() as AudioStreamPlayer;
        instance.AddChild(sfx);
        string path = $"res://sfx/{name}.ogg";
        AudioStream stream = ResourceLoader.Load<AudioStream>(path);
        sfx.Stream = stream;
        sfx.Play();
    }
}
