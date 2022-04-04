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

    public static void WaitForInput(Action callback)
    {
        if (Input.IsActionJustPressed("ui_accept"))
        {
            callback();
            Game.PlaySfx("UiAccept");
        }
    }

    public static void Begin(Node2D speaker, string text)
    {
        Init(speaker);
        Next(text);
    }

    public static void Next(string text)
    {
        textbox.label.Text = text;
    }

    public static void End()
    {
        textbox.root.QueueFree();
    }

    // ---

    public static void Hide()
    {
        textbox.container.Visible = false;
    }

    public static void Show()
    {
        textbox.container.Visible = true;
    }
}
