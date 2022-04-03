using System.Threading.Tasks;


public static class Async
{
    public static async Task WaitForSeconds(int duration)
    {
        await Task.Delay(duration * 1000);
    }

    public static async Task WaitForMilliseconds(int duration)
    {
        await Task.Delay(duration);
    }
}
