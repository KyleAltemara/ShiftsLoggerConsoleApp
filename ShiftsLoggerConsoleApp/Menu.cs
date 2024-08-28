using Spectre.Console;
using ShiftsLoggerAPI.Models;
using ShiftsLoggerConsoleApp.Controllers;

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
        var firstName = AnsiConsole.Ask<string>("Enter first name (or type 'c' to cancel):").Trim();
        if (UserCanceled(firstName))
        {
            return;
        }

        var lastName = AnsiConsole.Ask<string>("Enter last name (or type 'c' to cancel):").Trim();
        if (UserCanceled(lastName))
        {
            return;
        }

        if (!TryPrompt("Enter the date of work (YYYY-MM-dd) (or type 'c' to cancel):", DateTime.TryParse, out DateTime date) ||
            !TryPrompt("Enter the start time of work (HH:mm) (or type 'c' to cancel):", DateTime.TryParse, out DateTime startTime) ||
            !TryPrompt("Enter the length of the shift in hours (or type 'c' to cancel):", double.TryParse, out double shiftLength))
        {
            return;
        }

        startTime = new DateTime(date.Year, date.Month, date.Day, startTime.Hour, startTime.Minute, 0);
        var endTime = startTime.AddHours(shiftLength);
        try
        {
            await ShiftController.AddShift(new ShiftLogDTO(0, firstName, lastName, startTime, endTime));
        }
        catch (Exception ex)
        {
            AnsiConsole.Write(ex.Message);
        }
    }

    private static async Task UpdateShift()
    {
        var shifts = await ShiftController.GetAllShiftLogs();
        var menuOptions = shifts.ToDictionary(shift => shift.ToString(), shift => shift.Id);
        menuOptions.Add("Cancel", -1);
        var choice = menuOptions[AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a shift to update:")
                .AddChoices(menuOptions.Keys))];

        if (choice == -1)
        {
            return;
        }

        var shift = shifts.First(s => s.Id == choice);
        const string firstNamePrompt = "Enter first name (or type 'c' to cancel, or press enter to not change the first name):";
        var firstName = AnsiConsole.Ask(firstNamePrompt, defaultValue: shift.FirstName!).Trim();
        if (UserCanceled(firstName))
        {
            return;
        }

        const string lastNamePrompt = "Enter last name (or type 'c' to cancel, or press enter to not change the last name):";
        var lastName = AnsiConsole.Ask(lastNamePrompt, defaultValue: shift.LastName!).Trim();
        if (UserCanceled(lastName))
        {
            return;
        }

        const string startDatePrompt = "Enter the date of work (yyyy-mm-dd) (or type 'c' to cancel, or press enter to not change the date):";
        const string startTimePrompt = "Enter the start time of work (hh:mm) (or type 'c' to cancel, or press enter to not change the time):";
        const string shiftHoursPrompt = "Enter the length of the shift in hours (or type 'c' to cancel, or press enter to not change the hours):";
        double originalShiftHours = Math.Round((shift.ShiftEndTime - shift.ShiftStartTime).TotalHours, 2);
        if (!TryPrompt(startDatePrompt, DateTime.TryParse, out DateTime date, shift.ShiftStartTime.Date.ToString("yyyy-MM-dd")) ||
            !TryPrompt(startTimePrompt, DateTime.TryParse, out DateTime startTime, shift.ShiftStartTime.TimeOfDay.ToString(@"hh\:mm")) ||
            !TryPrompt(shiftHoursPrompt, double.TryParse, out double shiftLength, originalShiftHours.ToString()))
        {
            return;
        }

        startTime = new DateTime(date.Year, date.Month, date.Day, startTime.Hour, startTime.Minute, 0);
        var endTime = startTime.AddHours(shiftLength);
        try
        {
            await ShiftController.UpdateShift(new ShiftLogDTO(choice, firstName, lastName, startTime, endTime));
        }
        catch (Exception ex)
        {
            AnsiConsole.Write(ex.Message);
        }
    }

    private static async Task DeleteShift()
    {
        var shifts = await ShiftController.GetAllShiftLogs();
        var menuOptions = shifts.ToDictionary(shift => shift.ToString(), shift => shift.Id);
        menuOptions.Add("Cancel", -1);
        var choice = menuOptions[AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a shift to delete:")
                .AddChoices(menuOptions.Keys))];
        if (choice == -1)
        {
            return;
        }

        try
        {
            await ShiftController.DeleteShift(choice);
            AnsiConsole.MarkupLine("Shift deleted successfully.");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("Failed to delete shift.");
            AnsiConsole.Write(ex.Message);
        }
    }

    private static bool UserCanceled(string firstName)
    {
        return firstName.Equals("c", StringComparison.OrdinalIgnoreCase) ||
                    firstName.Equals("cancel", StringComparison.OrdinalIgnoreCase);
    }

    private static bool TryPrompt<T>(string prompt, TryParseHandler<T> tryParse, out T result, string? defaultValue)
    {
        while (true)
        {
            string? input = defaultValue == null ? AnsiConsole.Ask<string>(prompt) : AnsiConsole.Ask(prompt, defaultValue);
            if (UserCanceled(input))
            {
                result = default!;
                return false;
            }

            if (tryParse(input, out result))
            {
                return true;
            }

            AnsiConsole.MarkupLine("[red]Invalid input format.[/]");
        }
    }

    private static bool TryPrompt<T>(string prompt, TryParseHandler<T> tryParse, out T result) => TryPrompt(prompt, tryParse!, out result!, null);

    private delegate bool TryParseHandler<T>(string input, out T result);

}