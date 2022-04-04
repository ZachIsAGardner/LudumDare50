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

    public ConfrontationEntry Copy()
    {
        ConfrontationEntry result = new ConfrontationEntry();

        result.character = character;
        result.flavorText = flavorText;
        result.arenas = new List<PackedScene>() { };
        foreach (PackedScene arena in arenas) result.arenas.Add(arena);
        foreach (TalkOption option in options) result.options.Add(option);

        return result;
    }

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
                    confrontation.character.GetChildWithType<Shaker>().ShakeX();

                    confrontation.entry = confrontation.entry.Copy();

                    confrontation.entry.flavorText = "They seem satisfied!";
                    confrontation.entry.options = new List<TalkOption>()
                    {
                        new TalkOption()
                        {
                            text = "Stare",
                            action = _ => {  },
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
            Game.arenaCrestfallen1Prefab,
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

    public static ConfrontationEntry MultiButt = new ConfrontationEntry()
    {
        character = "MultiButt",
        flavorText = "Are those all butts?",
        arenas = new List<PackedScene>()
        {
            Game.arenaMultiButt1Prefab,
        },
        options = new List<TalkOption>()
        {
            new TalkOption()
            {
                text = "It's an emergency",
                action = (Confrontation confrontation) => { },
                reply = "You plead with them.",
                speach = "Sorry bucko, I've been looking forward to this all day!"
            },
            new TalkOption()
            {
                text = "Ask about the butts",
                action = (Confrontation confrontation) =>
                {
                    confrontation.entry = confrontation.entry.Copy();
                    confrontation.entry.flavorText = "It seems they like talking about their butts.";
                    confrontation.entry.options = new List<TalkOption>()
                    {
                        new TalkOption()
                        {
                            text = "Compliment their butts",
                            reply = "You say you wished you looked like them.",
                            action = _ =>
                            {
                                confrontation.canLeave = true;
                                confrontation.entry = confrontation.entry.Copy();
                                confrontation.entry.flavorText = "They seem very proud of themselves.";
                            },
                            speach = "I get that a lot!"
                        },
                        new TalkOption()
                        {
                            text = "Talk about the weather",
                            action = _ =>
                            {
                                confrontation.entry = confrontation.entry.Copy();
                                confrontation.entry.flavorText = "It is nice out today isn't it?";
                            },
                            reply = "You chat casually about the weather.",
                            speach = "Yeah it's great out today!"
                        },
                    };
                },
                reply = "You ask them about their butts.",
                speach = "I love pooping so much I got plastic surgery to add more butts."
            },
            new TalkOption()
            {
                text = "Talk about the weather",
                action = (Confrontation confrontation) =>
                {
                    confrontation.entry = confrontation.entry.Copy();
                    confrontation.entry.flavorText = "It is nice out today isn't it?";
                },
                reply = "You chat casually about the weather.",
                speach = "Yeah it's great out today!"
            },
        },
    };

    public static ConfrontationEntry LongCat = new ConfrontationEntry()
    {
        character = "LongCat",
        flavorText = "Their intentions are unknown.",
        arenas = new List<PackedScene>()
        {
            Game.arenaLongCat1Prefab,
        },
        options = new List<TalkOption>()
        {
            new TalkOption()
            {
                text = "Ask what they want",
                action = (Confrontation confrontation) => { },
                reply = "You inquire about their motivations.",
                speach = "Huh?"
            },
            new TalkOption()
            {
                text = "Nod",
                action = (Confrontation confrontation) =>
                {
                    confrontation.canLeave = true;

                    confrontation.entry = confrontation.entry.Copy();
                    confrontation.entry.flavorText = "Chill.";
                },
                reply = "You nod at them.",
                speach = "Chill."
            }
        },
    };

    private static Action<Confrontation> schoolChildRound2 = (Confrontation confrontation) =>
    {
        confrontation.entry = confrontation.entry.Copy();
        confrontation.entry.flavorText = "Now they want to know what 2 * 11 is.";
        confrontation.entry.options = new List<TalkOption>()
        {
            new TalkOption()
            {
                text = "Wait what",
                action = _ => { },
                reply = "You ask them to repeat themselves",
                speach = "Listen!"
            },
            new TalkOption()
            {
                text = "211",
                action = _ => schoolChildRound3(confrontation),
                reply = "You tell them the wrong answer and they feverishly write it down.",
                speach = "I just wanna play video games."
            },
            new TalkOption()
            {
                text = "22",
                action = _ => schoolChildRound3(confrontation),
                reply = "You tell them the correct answer and they feverishly write it down.",
                speach = "I just wanna play video games."
            }
        };
    };
    private static Action<Confrontation> schoolChildRound3 = (Confrontation confrontation) =>
    {
        confrontation.entry = confrontation.entry.Copy();
        confrontation.entry.flavorText = "They want to know what\n(1 + ((225 * (1023984 / 200)) * 11)) is.";
        confrontation.entry.options = new List<TalkOption>()
        {
            new TalkOption()
            {
                text = "12671806",
                action = _ =>
                {
                    confrontation.canLeave = true;

                    confrontation.entry = confrontation.entry.Copy();
                    confrontation.entry.flavorText = "They're finally done with their homework.";
                    confrontation.entry.options = new List<TalkOption>()
                    {
                        new TalkOption()
                        {
                            text = "Say hello.",
                            reply = "You ask them if they need anything else.",
                            speach = "Go away."
                        }
                    };
                },
                reply ="You tell them the wrong answer and they feverishly write it down.",
                speach = "Oh thank god, I'm finally done."
            },
            new TalkOption()
            {
                text = "12671804",
                action = _ =>
                {
                    confrontation.canLeave = true;

                    confrontation.entry = confrontation.entry.Copy();
                    confrontation.entry.flavorText = "They're finally done with their homework.";
                    confrontation.entry.options = new List<TalkOption>()
                    {
                        new TalkOption()
                        {
                            text = "Say hello.",
                            reply = "You ask them if they need anything else.",
                            speach = "Go away."
                        }
                    };
                },
                reply ="You tell them the wrong answer and they feverishly write it down.",
                speach = "Oh thank god, I'm finally done."
            },
            new TalkOption()
            {
                text = "12671803",
                action = _ =>
                {
                    confrontation.canLeave = true;

                    confrontation.entry = confrontation.entry.Copy();
                    confrontation.entry.flavorText = "They're finally done with their homework.";
                    confrontation.entry.options = new List<TalkOption>()
                    {
                        new TalkOption()
                        {
                            text = "Say hello.",
                            reply = "You ask them if they need anything else.",
                            speach = "Go away."
                        }
                    };
                },
                reply ="You tell them the correct answer and they feverishly write it down.",
                speach = "Oh thank god, I'm finally done."
            },
            new TalkOption()
            {
                text = "7",
                action = _ =>
                {
                    confrontation.canLeave = true;

                    confrontation.entry = confrontation.entry.Copy();
                    confrontation.entry.flavorText = "They're finally done with their homework.";
                    confrontation.entry.options = new List<TalkOption>()
                    {
                        new TalkOption()
                        {
                            text = "Say hello.",
                            reply = "You ask them if they need anything else.",
                            speach = "Go away."
                        }
                    };
                },
                reply ="You tell them the wrong answer and they feverishly write it down.",
                speach = "Oh thank god, I'm finally done."
            }
        };
    };
    public static ConfrontationEntry SchoolChild = new ConfrontationEntry()
    {
        character = "SchoolChild",
        flavorText = "They want to know what 2 + 5 is.",
        arenas = new List<PackedScene>()
        {
            Game.arenaSchoolChild1Prefab,
        },
        options = new List<TalkOption>()
        {
            new TalkOption()
            {
                text = "Wait what",
                action = (Confrontation confrontation) => { },
                reply = "You ask them to repeat themselves",
                speach = "Listen!"
            },
            new TalkOption()
            {
                text = "7",
                action = (Confrontation confrontation) => schoolChildRound2(confrontation),
                reply = "You tell them the correct answer and they feverishly write it down.",
                speach = "I hate homework so much."
            },
            new TalkOption()
            {
                text = "25",
                action = (Confrontation confrontation) => schoolChildRound2(confrontation),
                reply = "You tell them the wrong answer and they feverishly write it down.",
                speach = "I hate homework so much."
            }
        },
    };

    private static int eggManTextureIndex = 0;
    private static Action<Confrontation> eggManRound2 = (Confrontation confrontation) =>
    {
        confrontation.entry = confrontation.entry.Copy();
        confrontation.entry.flavorText = "They want to know if they're holding a box of eggs.";
        confrontation.entry.options = new List<TalkOption>()
        {
            new TalkOption()
            {
                text = "Confirm Eggs",
                action = _ => eggManRound3(confrontation),
                reply = "You confirm they're indeed holding a box of eggs.",
                speach = "Okay thanks, my vision is not so good."
            },
            new TalkOption()
            {
                text = "Deny Eggs",
                action = _ =>
                {
                    confrontation.character.GetChildWithType<Shaker>().ShakeX();
                    List<string> textures = new List<string>()
                    {
                        "EggMan",
                        "EggManEgg",
                        "EggManBird",
                        "EggManSword"
                    };
                    eggManTextureIndex++;
                    if (eggManTextureIndex >= textures.Count) eggManTextureIndex = 0;
                    confrontation.character.Texture = ResourceLoader.Load<Texture>($"res://sprites/{textures[eggManTextureIndex]}.png");
                },
                reply = "You tell them those aren't eggs and they grab something else.",
                speach = "Oh alright, well how about this?"
            }
        };
    };
    private static Action<Confrontation> eggManRound3 = (Confrontation confrontation) =>
    {
        confrontation.entry = confrontation.entry.Copy();
        confrontation.entry.flavorText = "They want to know if eggs are good for your eyes.";
        confrontation.entry.options = new List<TalkOption>()
        {
            new TalkOption()
            {
                text = "Eggs are good",
                action = _ =>
                {
                    confrontation.canLeave = true;

                    confrontation.entry = confrontation.entry.Copy();
                    confrontation.entry.flavorText = "They know more about eggs now.";
                    confrontation.entry.options = new List<TalkOption>()
                    {
                        new TalkOption()
                        {
                            text = "Say Hi",
                            speach = "Hi",
                            reply = "You say hello."
                        }
                    };
                },
                reply = "You give them bad advice about putting egg yolks on your eyeballs.",
                speach = "Sick dude. I've been avoiding getting glasses."
            },
            new TalkOption()
            {
                text = "Eggs are bad",
                action = _ =>
                {
                    confrontation.canLeave = true;

                    confrontation.entry = confrontation.entry.Copy();
                    confrontation.entry.flavorText = "They know more about eggs now.";
                    confrontation.entry.options = new List<TalkOption>()
                    {
                        new TalkOption()
                        {
                            text = "Say Hi",
                            speach = "Hi",
                            reply = "You say hello."
                        }
                    };
                },
                reply = "You warn them about the use of eggs on your eyeballs.",
                speach = "Oh alright, I guess I'll have to get glasses after all."
            }
        };
    };
    public static ConfrontationEntry EggMan = new ConfrontationEntry()
    {
        character = "EggMan",
        flavorText = "They have egg related questions.",
        arenas = new List<PackedScene>()
        {
            Game.arenaEggMan1Prefab,
        },
        options = new List<TalkOption>()
        {
            new TalkOption()
            {
                text = "Tell them you don't work here",
                action = (Confrontation confrontation) => eggManRound2(confrontation),
                reply = "You tell them that you don't work here.",
                speach = "Oh, well do you know anything about eggs?"
            },
            new TalkOption()
            {
                text = "Tell them you work here",
                action = (Confrontation confrontation) => eggManRound2(confrontation),
                reply = "You lie right to their face.",
                speach = "Awesome, do you know anything about eggs?"
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
    private float lastHealth = Game.maxHealth;

    // Misc Controls

    public Button focus;
    public Sprite speachBubble;
    public TextureRect character;

    // Action

    public Control actionContainer = null;
    public Label mainTextbox = null;
    public Control talkButtonsContainer = null;
    public List<Button> mainButtons = new List<Button>() { };
    public Control mainButtonsContainer;
    public Sprite starSprite;
    public Sprite starSprite2;

    // Arena

    public Control arenaContainer = null;

    // HP

    public Label hp;

    public override void _Ready()
    {
        base._Ready();

        Game.PlaySong("Confrontation");

        if (entry == null) entry = ConfrontationEntry.Apple;

        List<Node> hierarchy = this.GetHierarchy();

        // Get elements

        focus = hierarchy.Find(h => h.Name == "Focus") as Button;
        talkButtonsContainer = hierarchy.Find(h => h.Name == "TalkButtonsContainer") as Control;
        mainButtonsContainer = hierarchy.Find(h => h.Name == "MainButtonsContainer") as Control;
        mainTextbox = hierarchy.Find(h => h.Name == "MainTextbox") as Label;
        actionContainer = hierarchy.Find(h => h.Name == "ActionContainer") as Control;
        arenaContainer = hierarchy.Find(h => h.Name == "ArenaContainer") as Control;
        hp = hierarchy.Find(h => h.Name == "HP") as Label;
        speachBubble = hierarchy.Find(h => h.Name == "SpeachBubble") as Sprite;
        character = hierarchy.Find(h => h.Name == "Character") as TextureRect;
        starSprite = hierarchy.Find(h => h.Name == "Star") as Sprite;
        starSprite2 = hierarchy.Find(h => h.Name == "Star2") as Sprite;

        Node buttonContainer = hierarchy.Find(h => h.Name == "MainButtons");
        mainButtons = buttonContainer.GetChildrenWithType<Button>();

        // Setup buttons

        mainButtons[0].GrabFocus();
        mainButtons[0].Connect("focus_entered", this, nameof(ButtonFocused));
        mainButtons[0].Connect("pressed", this, nameof(TalkPressed));
        mainButtons[1].Connect("focus_entered", this, nameof(ButtonFocused));
        mainButtons[1].Connect("pressed", this, nameof(LeavePressed));

        List<Button> talkButtons = talkButtonsContainer.GetChildrenWithTypeInHierarchy<Button>();
        foreach (Button button in talkButtons) button.Connect("focus_entered", this, nameof(ButtonFocused));

        // Set data

        mainTextbox.Text = entry.flavorText;
        character.Texture = ResourceLoader.Load<Texture>($"res://sprites/{entry.character}.png");
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        this.delta = delta;
        if (lastHealth != Game.health && Game.health != Game.maxHealth)
        {
            hp.GetChildWithType<Shaker>().ShakeX();
        }
        lastHealth = Game.health;
        hp.Text = $"Poop: {Game.health}";
        if (Game.health <= 0 && !didLoadGameOver)
        {
            didLoadGameOver = true;
            Game.LoadLevel("GameOver");
        }

        if (canLeave && !didCanLeave)
        {
            didCanLeave = true;
            starSprite.Show();
            starSprite2.Show();
            Game.PlaySfx("CanLeave");
        }
        else if (!canLeave)
        {
            starSprite.Hide();
            starSprite2.Hide();
        }
    }

    // Misc

    public void OnOptionPressed(TalkOption option)
    {
        Game.PlaySfx("UiAccept");
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
        mainButtonsContainer.Show();
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
        mainButtonsContainer.Hide();
        RemoveFocus();

        mainTextbox.Text = text;
        await Talk.WaitForInput();

        if (returnToActions)
        {
            mainButtonsContainer.Show();
            mainButtons[0].GrabFocus();
            mainTextbox.Text = entry.flavorText;
        }
    }

    public async Task TextBegin(string text)
    {
        mainTextbox.Show();
        mainButtonsContainer.Hide();
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

        mainButtonsContainer.Show();
        GiveBackFocus();
        mainButtons[0].GrabFocus();

        mainTextbox.Text = entry.flavorText;
    }

    // Buttons

    public void ButtonFocused()
    {
        Game.PlaySfx("UiMove");
    }

    public void TalkPressed()
    {
        Game.PlaySfx("UiAccept");

        mainTextbox.Hide();
        mainButtonsContainer.Hide();
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
        Game.PlaySfx("UiAccept");

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
