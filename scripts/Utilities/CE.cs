using System.Threading.Tasks;

public static class CE
{
    public static ConfrontationEntry GetEntry(string entry)
    {
        if (entry == "Apple")
        {
            return ConfrontationEntry.Apple;
        }
        else if (entry == "Crestfallen")
        {
            return ConfrontationEntry.Crestfallen;
        }
        else if (entry == "MultiButt")
        {
            return ConfrontationEntry.MultiButt;
        }
        else if (entry == "LongCat")
        {
            return ConfrontationEntry.LongCat;
        }
        else if (entry == "SchoolChild")
        {
            return ConfrontationEntry.SchoolChild;
        }
        else if (entry == "EggMan")
        {
            return ConfrontationEntry.EggMan;
        }
        else
        {
            return ConfrontationEntry.Apple;
        }
    }

    public static async Task VictoryEntry(Confrontation confrontation)
    {
        await confrontation.TextSingle("You Win!", false);
        Game.Fade(() =>
        {
            if (Game.player != null) Game.player.IsInControl = true;
            confrontation.QueueFree();
        });
    }

    public static async Task CannotLeaveEntry(Confrontation confrontation)
    {
        await confrontation.TextSingle("It seems they aren't satisfied...");
    }
}