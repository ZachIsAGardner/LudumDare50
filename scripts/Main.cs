using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Main : Node2D
{
    Player player = null;
    Sprite playerSprite = null;
    Shaker playerShaker = null;
    Label interactPrompt = null;
    Label movePrompt = null;
    Label title = null;

    int progress = 0;
    float timer = 0;

    public override void _Ready()
    {
        base._Ready();

        GD.Print("MAIN START");

        player = this.GetChildWithNameInHierarchy("Player") as Player;
        interactPrompt = this.GetChildWithNameInHierarchy("InteractPrompt") as Label;
        movePrompt = this.GetChildWithNameInHierarchy("MovePrompt") as Label;
        player.IsInControl = false;
        playerSprite = player.GetChildWithType<Sprite>();
        playerShaker = player.GetChildWithTypeInHierarchy<Shaker>();
        title = GetTree().Root.GetChildWithNameInHierarchy("Title") as Label;

        if (Game.skipIntro)
        {
            player.IsInControl = true;
            playerSprite.Frame = 5;
            Game.PlaySong("Main");
            return;
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (Game.skipIntro) return;

        timer -= delta;
        Cutscene();
    }

    void Cutscene()
    {
        if (timer > 0) return;

        if (progress == 0)
        {
            progress++;
            Game.PlaySong("Intro");
            playerSprite.Scale = new Vector2(-1, 1);
            timer = 2;
            return;
        }

        if (progress == 1)
        {
            progress++;
            interactPrompt.Show();
            playerSprite.Frame = 1;
            Talk.Begin(player as Node2D, "Hm 90% off sneezey bananas...");
            timer = 0.1f;
            return;
        }

        if (progress == 2)
        {
            Talk.WaitForInput(() => progress++);
            return;
        }

        if (progress == 3)
        {
            progress++;
            interactPrompt.Hide();
            playerSprite.Frame = 0;
            Talk.Next("I'll have to think about this one.");
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
            Talk.Hide();
            timer = 0.5f;
            return;
        }

        if (progress == 6)
        {
            progress++;
            playerSprite.Scale = new Vector2(1, 1);
            timer = 0.5f;
            return;
        }

        if (progress == 7)
        {
            progress++;
            playerSprite.Scale = new Vector2(-1, 1);
            timer = 0.5f;
            return;
        }

        if (progress == 8)
        {
            progress++;
            playerSprite.Scale = new Vector2(1, 1);
            Talk.Show();
            playerSprite.Frame = 2;
            Talk.Next("Hmmmm no one's around. Now's my chance.");
            return;
        }

        if (progress == 9)
        {
            Talk.WaitForInput(() => progress++);
            return;
        }

        if (progress == 10)
        {
            Talk.Hide();
            progress++;
            Game.PlaySong("");
            playerSprite.Frame = 3;
            Game.PlaySfx("Fart");
            playerShaker.ShakeX(4.5f, 8);
            timer = 3;
            return;
        }

        if (progress == 11)
        {
            progress++;
            playerSprite.Frame = 4;
            playerShaker.ShakeX(2.5f, 24);
            timer = 3;
            return;
        }

        if (progress == 12)
        {
            progress++;
            Talk.Show();
            Game.PlaySong("Terror");
            Talk.Next("Oh. My. God.");
            return;
        }

        if (progress == 13)
        {
            Talk.WaitForInput(() => progress++);
            return;
        }

        if (progress == 14)
        {
            progress++;
            Talk.Hide();
            title.Visible = true;
            Game.PlaySfx("OhNo");
            timer = 4;
            return;
        }

        if (progress == 15)
        {
            progress++;
            title.Visible = false;
            timer = 1;
            return;
        }

        if (progress == 16)
        {
            progress++;
            Talk.Show();
            Talk.Next("No wait... I think there's still time.");
            return;
        }

        if (progress == 17)
        {
            Talk.WaitForInput(() => progress++);
            return;
        }

        if (progress == 18)
        {
            progress++;
            Game.FadeOutSong();
            return;
        }

        if (progress == 19)
        {
            progress++;
            Talk.Next("I'll be fine if I can make it to a bathroom in time.");
            return;
        }

        if (progress == 20)
        {
            Talk.WaitForInput(() => progress++);
            return;
        }

        if (progress == 21)
        {
            progress++;
            Talk.Next("I just gotta keep my composure.");
            return;
        }

        if (progress == 22)
        {
            Talk.WaitForInput(() => progress++);
            return;
        }

        if (progress == 23)
        {
            progress++;
            playerSprite.Frame = 5;
            Talk.Next("I hope I don't run into anyone.");
            return;
        }

        if (progress == 24)
        {
            Talk.WaitForInput(() => progress++);
            return;
        }

        if (progress == 25)
        {
            progress++;
            Talk.End();
            Game.PlaySong("Main");
            player.IsInControl = true;
            movePrompt.Show();
            timer = 3;
            return;
        }

        if (progress == 26)
        {
            progress++;
            movePrompt.Hide();
            return;
        }
    }
}
