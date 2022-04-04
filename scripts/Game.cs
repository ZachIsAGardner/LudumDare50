using Godot;
using System;
using System.Threading.Tasks;

public class Game : Node
{
    public static float elapsed = 0;

    public static bool skipIntro = false;

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
    public static PackedScene songPrefab;

    public static PackedScene arenaApple1Prefab;
    public static PackedScene arenaCrestfallen1Prefab;
    public static PackedScene arenaSchoolChild1Prefab;
    public static PackedScene arenaEggMan1Prefab;
    public static PackedScene arenaLongCat1Prefab;
    public static PackedScene arenaMultiButt1Prefab;

    private static AudioStreamPlayer song;
    private static bool fadeOut = false;

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
        songPrefab = ResourceLoader.Load<PackedScene>("res://prefabs/Song.tscn");

        arenaApple1Prefab = ResourceLoader.Load<PackedScene>("res://prefabs/ArenaApple1.tscn");
        arenaCrestfallen1Prefab = ResourceLoader.Load<PackedScene>("res://prefabs/ArenaCrestfallen1.tscn");
        arenaSchoolChild1Prefab = ResourceLoader.Load<PackedScene>("res://prefabs/ArenaSchoolChild1.tscn");
        arenaEggMan1Prefab = ResourceLoader.Load<PackedScene>("res://prefabs/ArenaEggMan1.tscn");
        arenaLongCat1Prefab = ResourceLoader.Load<PackedScene>("res://prefabs/ArenaLongCat1.tscn");
        arenaMultiButt1Prefab = ResourceLoader.Load<PackedScene>("res://prefabs/ArenaMultiButt1.tscn");
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        elapsed += delta;

        if (fadeOut && song != null)
        {
            song.VolumeDb -= 10f * delta;
        }

        if (Input.IsActionJustPressed("toggle_fullscreen"))
        {
            OS.WindowFullscreen = !OS.WindowFullscreen;
        }
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

    public static void PlaySong(string name)
    {
        fadeOut = false;

        if (song != null)
        {
            song.Stop();
            song.QueueFree();
        }

        if (String.IsNullOrWhiteSpace(name))
        {
            return;
        }

        song = songPrefab.Instance() as AudioStreamPlayer;
        instance.AddChild(song);
        string path = $"res://songs/{name}.ogg";
        AudioStream stream = ResourceLoader.Load<AudioStream>(path);
        song.Stream = stream;
        song.Play();
    }

    public static void FadeOutSong()
    {
        fadeOut = true;
    }
}
