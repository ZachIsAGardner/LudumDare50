using Godot;
using System;
using System.Threading.Tasks;

public class Textbox
{
    public Node2D root;
    public Control container;
    public Label label;
    public Control prompt;
}

public class Talk : Node
{
    private static Node instance;

    private static Textbox textbox;

    public override void _Ready()
    {
        base._Ready();

        instance = this;
    }

    private static void Init(Node2D speaker)
    {
        textbox = new Textbox();
        textbox.root = Game.floatingTextboxPrefab.Instance() as Node2D;
        textbox.container = textbox.root.GetChildWithNameInHierarchy("Container") as Control;
        textbox.label = textbox.root.GetChildWithNameInHierarchy("Label") as Label;
        textbox.prompt = textbox.root.GetChildWithNameInHierarchy("Prompt") as Control;

        textbox.root.Position = new Vector2(0, -256);

        speaker.AddChild(textbox.root);
    }

    public static async Task WaitForInput()
    {
        await Async.WaitForMilliseconds(100);
        while (!Input.IsActionJustPressed("ui_accept")) await Async.WaitForMilliseconds(1);
        Game.PlaySfx("UiAccept");
    }

    public static async Task Single(Node2D speaker, string text)
    {
        Init(speaker);
        await End(text);
    }

    public static async Task Begin(Node2D speaker, string text)
    {
        Init(speaker);
        await Next(text);
    }

    public static async Task Next(string text)
    {
        textbox.label.Text = text;
        await WaitForInput();
    }

    public static async Task End(string text)
    {
        textbox.label.Text = text;
        await WaitForInput();
        textbox.root.QueueFree();;
    }

    public static void Hide()
    {
        textbox.container.Visible = false;
    }

    public static void Show()
    {
        textbox.container.Visible = true;
    }
}
