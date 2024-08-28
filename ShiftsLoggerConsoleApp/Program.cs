namespace ShiftsLoggerConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        Task.Run(Menu.ShowMenuAsync).GetAwaiter().GetResult();
    }
}
