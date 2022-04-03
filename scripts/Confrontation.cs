using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TalkOption : Godot.Object
{
    public string text;
    public string reply;
    public string speach;
    public Action<Confrontation> action;
}

public class ConfrontationEntry
{
    public string character;
    public string flavorText;
    public List<PackedScene> arenas = new List<PackedScene>() { };
    public List<TalkOption> options = new List<TalkOption>() { };

    public static ConfrontationEntry Apple = new ConfrontationEntry()
    {
        character = "Apple",
        flavorText = "Looks like they need some help.",
        arenas = new List<PackedScene>()
        {
            Game.arenaApple1Prefab,
        },
        options = new List<TalkOption>()
        {
            new TalkOption()
            {
                text = "Help",
                action = (Confrontation confrontation) => {
                    confrontation.canLeave = true;
                    confrontation.character.Texture = ResourceLoader.Load<Texture>("res://sprites/AppleNoSuit.png");
                    confrontation.entry.options = new List<TalkOption>()
                    {
                        new TalkOption()
                        {
                            text = "Stare",
                            action = (Confrontation confrontation2) => {  },
                            reply = "You stare at their half naked body.",
                            speach = "Um.. I'm okay now, you can leave if you want."
                        }
                    };
                },
                reply = "You help them take of their suit.",
                speach = "Wow! Thanks!"
            },
            new TalkOption()
            {
                text = "Don't Help",
                action = (Confrontation confrontation) => { },
                reply = "You refuse to help.",
                speach = "I am sweating profusely."
            }
        },
    };

    public static ConfrontationEntry Crestfallen = new ConfrontationEntry()
    {
        character = "Crestfallen",
        flavorText = "This man is really overthinking grocery shopping.",
        arenas = new List<PackedScene>()
        {
            Game.arenaApple1Prefab,
        },
        options = new List<TalkOption>()
        {
            new TalkOption()
            {
                text = "You're doing great.",
                action = (Confrontation confrontation) => { },
                reply = "You tell him how cool his shorts are.",
                speach = "I hate these shorts."
            },
            new TalkOption()
            {
                text = "Lay down with him.",
                action = (Confrontation confrontation) =>
                {
                    confrontation.canLeave = true;
                },
                reply = "You lay down with him.",
                speach = "I guess having company isn't so bad."
            }
        },
    };
}

public class Confrontation : Control
{
    public ConfrontationEntry entry;

    float delta = 0;

    public bool canLeave = false;
    public bool didCanLeave = false;
    bool didLoadGameOver = false;

    public string flavorText = "It seems your are just so approachable.";

    public Button focus;
    public Control actionContainer = null;
    public Label mainTextbox = null;
    public List<Button> mainButtons = new List<Button>() { };
    public Control talkButtonsContainer = null;
    public Control arenaContainer = null;
    public Label hp;
    public Sprite speachBubble;
    public TextureRect character;
    public Sprite starSprite;
    public Sprite starSprite2;

    public override void _Ready()
    {
        base._Ready();

        List<Node> hierarchy = this.GetHierarchy();

        // Get elements

        focus = hierarchy.Find(h => h.Name == "Focus") as Button;
        talkButtonsContainer = hierarchy.Find(h => h.Name == "TalkButtonsContainer") as Control;
        mainTextbox = hierarchy.Find(h => h.Name == "MainTextbox") as Label;
        actionContainer = hierarchy.Find(h => h.Name == "ActionContainer") as Control;
        arenaContainer = hierarchy.Find(h => h.Name == "ArenaContainer") as Control;
        hp = hierarchy.Find(h => h.Name == "HP") as Label;
        speachBubble = hierarchy.Find(h => h.Name == "SpeachBubble") as Sprite;
        character = hierarchy.Find(h => h.Name == "Character") as TextureRect;
        starSprite = hierarchy.Find(h => h.Name == "Star") as Sprite;
        starSprite2 = hierarchy.Find(h => h.Name == "Star2") as Sprite;

        Node buttonContainer = hierarchy.Find(h => h.Name == "Buttons");
        mainButtons = buttonContainer.GetChildrenWithType<Button>();

        // Setup buttons

        mainButtons[0].GrabFocus();
        mainButtons[0].Connect("pressed", this, nameof(TalkPressed));
        mainButtons[1].Connect("pressed", this, nameof(LeavePressed));

        // Set data

        mainTextbox.Text = entry.flavorText;
        character.Texture = ResourceLoader.Load<Texture>($"res://sprites/{entry.character}.png");
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        this.delta = delta;
        hp.Text = $"Poop: {Game.health}";
        if (Game.health <= 0 && !didLoadGameOver)
        {
            didLoadGameOver = true;
            Game.LoadLevel("GameOver");
            Game.PlaySfx("Fart3");
        }

        if (canLeave && !didCanLeave)
        {
            didCanLeave = true;
            starSprite.Show();
            starSprite2.Show();
            Game.PlaySfx("CanLeave");
        }
    }

    // Misc

    public void OnOptionPressed(TalkOption option)
    {
        GD.Print("press button");
        _ = DoTalkOption(option);
    }

    public async Task DoTalkOption(TalkOption option)
    {
        talkButtonsContainer.Hide();

        if (option.action != null)
        {
            option.action(this);
        }

        if (!String.IsNullOrWhiteSpace(option.reply))
        {
            await TextSingle(option.reply, false);
            mainTextbox.Hide();
        }

        if (!String.IsNullOrWhiteSpace(option.speach))
        {
            await SpeachSingle(option.speach);
        }

        RandomNumberGenerator rng = new RandomNumberGenerator();
        rng.Randomize();
        int r = rng.RandiRange(0, entry.arenas.Count - 1);

        actionContainer.Hide();
        Arena arena = (Arena)entry.arenas[r].Instance();
        arenaContainer.AddChild(arena);
        while (!arena.Done()) await Async.WaitForMilliseconds(1);

        actionContainer.Show();
        mainTextbox.Show();
        EnableActionButtons();
        mainButtons[0].GrabFocus();
        mainTextbox.Text = entry.flavorText;
        arena.QueueFree();
        GiveBackFocus();
    }

    public void RemoveFocus()
    {
        focus.EnabledFocusMode = FocusModeEnum.All;
        focus.GrabFocus();
    }

    public void GiveBackFocus()
    {
        focus.EnabledFocusMode = FocusModeEnum.None;
    }

    public void DisableActionButtons()
    {
        mainButtons.ForEach(b =>
        {
            b.Disabled = true;
            b.FocusMode = FocusModeEnum.None;
        });
    }

    public void EnableActionButtons()
    {
        mainButtons.ForEach(b =>
        {
            b.Disabled = false;
            b.FocusMode = FocusModeEnum.All;
        });
    }

    // Speach

    public async Task SpeachSingle(string text)
    {
        speachBubble.Show();
        speachBubble.GetChildWithType<Label>().Text = text;
        await Async.WaitForMilliseconds(100);
        float duration = 2;
        while (duration > 0 && !Input.IsActionJustPressed("ui_accept"))
        {
            duration -= delta;
            await Async.WaitForMilliseconds(1);
        }
        speachBubble.Hide();
    }

    // Main Textbox

    public async Task TextSingle(string text, bool returnToActions = true)
    {
        mainTextbox.Show();
        DisableActionButtons();
        RemoveFocus();

        mainTextbox.Text = text;
        await Talk.WaitForInput();

        if (returnToActions)
        {
            EnableActionButtons();
            mainButtons[0].GrabFocus();
            mainTextbox.Text = entry.flavorText;
        }
    }

    public async Task TextBegin(string text)
    {
        mainTextbox.Show();
        DisableActionButtons();
        RemoveFocus();

        mainTextbox.Text = text;
        await Talk.WaitForInput();
    }

    public async Task TextNext(string text)
    {
        mainTextbox.Text = text;
        await Talk.WaitForInput();
    }

    public async Task TextEnd(string text)
    {
        mainTextbox.Text = text;
        await Talk.WaitForInput();

        EnableActionButtons();
        GiveBackFocus();
        mainButtons[0].GrabFocus();

        mainTextbox.Text = flavorText;
    }

    // Buttons


    public void TalkPressed()
    {
        mainTextbox.Hide();
        DisableActionButtons();
        GiveBackFocus();

        talkButtonsContainer.Show();

        List<Button> talkButtons = talkButtonsContainer.GetChildrenWithTypeInHierarchy<Button>();
        talkButtons.ForEach(x =>
        {
            if (x.IsConnected("pressed", this, nameof(OnOptionPressed))) x.Disconnect("pressed", this, nameof(OnOptionPressed));
            x.Hide();
        });
        int i = 0;
        entry.options.ForEach(x =>
        {
            Button button = talkButtons[i];
            button.Show();
            button.Text = x.text;
            button.Connect("pressed", this, nameof(OnOptionPressed), new Godot.Collections.Array() { x });
            i++;
        });
        talkButtons[0].GrabFocus();
    }

    public void LeavePressed()
    {
        if (!canLeave)
        {
            _ = CE.CannotLeaveEntry(this);
        }
        else
        {
            _ = CE.VictoryEntry(this);
        }
    }
}
