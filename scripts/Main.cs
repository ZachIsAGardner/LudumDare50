using Godot;
using System;
using System.Threading.Tasks;

public class Main : Node2D
{
    Player player = null;
    Sprite playerSprite = null;
    Label interactPrompt = null;
    Label movePrompt = null;

    public override void _Ready()
    {
        base._Ready();

        if (Game.skipIntro)
        {
            return;
        }

        player = this.GetChildWithNameInHierarchy("Player") as Player;
        interactPrompt = this.GetChildWithNameInHierarchy("InteractPrompt") as Label;
        movePrompt = this.GetChildWithNameInHierarchy("MovePrompt") as Label;
        player.IsInControl = false;
        playerSprite = player.GetChildWithType<Sprite>();

        _ = Cutscene();
    }

    async Task Cutscene()
    {
        playerSprite.Scale = new Vector2(-1, 1);
        await Async.WaitForMilliseconds(2000);

        interactPrompt.Show();

        playerSprite.Frame = 1;
        await Talk.Begin(player as Node2D, "Hm 90% off sneezey bananas...");

        interactPrompt.Hide();

        playerSprite.Frame = 0;
        await Talk.Next("I'll have to think about this one.");

        Talk.Hide();
        await Async.WaitForMilliseconds(500);

        playerSprite.Scale = new Vector2(1, 1);
        await Async.WaitForMilliseconds(500);

        playerSprite.Scale = new Vector2(-1, 1);
        await Async.WaitForMilliseconds(500);

        playerSprite.Scale = new Vector2(1, 1);
        Talk.Show();
        playerSprite.Frame = 2;
        await Talk.Next("Hmmmm no one's around. Now's my chance.");

        playerSprite.Frame = 3;
        Talk.Hide();
        Game.PlaySfx("Fart");
        await Async.WaitForMilliseconds(3500);

        playerSprite.Frame = 4;
        await Async.WaitForMilliseconds(2000);

        Talk.Show();
        await Talk.Next("Oh. My. God.");

        Talk.Hide();
        Label title = GetTree().Root.GetChildWithNameInHierarchy("Title") as Label;

        title.Visible = true;
        Game.PlaySfx("OhNo");
        await Async.WaitForMilliseconds(4000);

        title.Visible = false;
        await Async.WaitForMilliseconds(1000);

        Talk.Show();
        await Talk.Next("No wait... I think there's still time.");

        await Talk.Next("I'll be fine if I can make it to a bathroom in time.");
        await Talk.Next("I just gotta keep my composure.");
        playerSprite.Frame = 5;
        
        await Talk.End("I hope I don't run into anyone.");

        player.IsInControl = true;
        movePrompt.Show();
        await Async.WaitForMilliseconds(3000);
        movePrompt.Hide();
    }
}
