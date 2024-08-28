using Spectre.Console;
using ShiftsLoggerAPI.Models;
using ShiftsLoggerConsoleApp.Controllers;
using Microsoft.CodeAnalysis.Elfie.Extensions;

namespace ShiftsLoggerConsoleApp;

internal static class Menu
{
    internal static async Task ShowMenuAsync()
    {
        AnsiConsole.WriteLine("Phone Book Console App");
        var continueRunning = true;
        while (continueRunning)
        {
            var menuOptions = new Dictionary<string, Func<Task>>
            {
                { "List Shits", ListShifts },
                { "Add Shift", AddShift },
                { "Update Shift", UpdateShift },
                { "Delete Shift", DeleteShift },
                { "Exit", () =>
                    {
                        AnsiConsole.Markup("[red]Exiting Program[/]");
                        continueRunning = false;
                        return Task.CompletedTask;
                    }
                },
            };

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose an option:")
                    .AddChoices(menuOptions.Keys));

            if (menuOptions.TryGetValue(choice, out var selectedAction))
            {
                AnsiConsole.Clear();
                await selectedAction.Invoke();
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid option.[/]");
            }
        }
    }

    private static async Task ListShifts()
    {
        try
        {
            var shifts = await ShiftController.GetAllShiftLogs();
            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("First Name");
            table.AddColumn("Last Name");
            table.AddColumn("Start Time");
            table.AddColumn("End Time");
            table.AddColumn("Hours Worked");

            foreach (var shift in shifts)
            {
                table.AddRow(shift.Id.ToString(),
                             shift.FirstName!,
                             shift.LastName!,
                             shift.ShiftStartTime.ToString("yyyy-mm-dd hh:mm"),
                             shift.ShiftEndTime.ToString("yyyy-mm-dd hh:mm"),
                             Math.Round((shift.ShiftEndTime - shift.ShiftStartTime).TotalHours, 2).ToString() + " hours");
            }

            AnsiConsole.Write(table);
        }
        catch (Exception ex)
        {
            AnsiConsole.Write(ex.Message);
        }

        AnsiConsole.MarkupLine("Press [green]Enter[/] to retun to main menu.");
        while (Console.ReadKey().Key != ConsoleKey.Enter) ;
    }

    private static async Task AddShift()
    {
        try
        {
            //var firstName = AnsiConsole.Ask<string>("Enter first name:").Trim();
            //var lastName = AnsiConsole.Ask<string>("Enter last name:").Trim();
            //var startTime = AnsiConsole.Ask<DateTime>("Enter the start time of work. (YYYY-MM-dd HH:mm:ss)");
            //var endTime = AnsiConsole.Ask<DateTime>("Enter the end time of work. (YYYY-MM-dd HH:mm:ss)");
            //ShiftController.AddShift(new ShiftLogDTO(0, firstName, lastName, startTime, endTime));
            await ShiftController.AddShift(new ShiftLogDTO(0, "John", "Doe", DateTime.Now, DateTime.Now.AddHours(8.123456)));
        }
        catch (Exception ex)
        {
            AnsiConsole.Write(ex.Message);
        }
    }

    private static async Task UpdateShift()
    {
        throw new NotImplementedException();
    }

    private static async Task DeleteShift()
    {
        throw new NotImplementedException();
    }
}