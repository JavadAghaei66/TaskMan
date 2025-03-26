using TaskMan.Application;
using Spectre.Console;
using TaskMan.Domain;
using System.Globalization;

namespace TaskMan.CLI;

class AddCommand : CommandBase
{
    private readonly TaskService _taskService;
    public AddCommand(TaskService taskService)
    {
        _taskService = taskService;
    }

    public override string Name => "add";

    public override void Execute(string[] args)
    {
        AnsiConsole.MarkupLine("[bold blue]Let's add a new task![/]");
        // 
        string title = AnsiConsole.Prompt(new TextPrompt<string>("[bold blue]Enter task title:[/]"));
        //
        string? description = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold blue]Enter task description (or leave empty):[/]")
                .AllowEmpty()
            );
        //
        Priority priority = AnsiConsole.Prompt(
            new SelectionPrompt<Priority>()
            .Title("[bold]select task priority:[/]")
            .AddChoices(Priority.Low, Priority.Medium, Priority.High)
        );
        //
        AnsiConsole.MarkupLine("[bold blue]How do you want to set the duo date?[/]");
        DateTime duoDate = getDueDate();

        // create new task
        _taskService.AddTask(title, description, priority.ToString(), false, dueDate: duoDate);

        AnsiConsole.MarkupLine("[bold green]Task added successfully.[/]");
    }

    private DateTime getDueDate()
    {
        DateTime dueDate;

        var choices = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[bold yellow]Select an option:[/]")
            .AddChoices("Specify number of days (e.g., '7 days from now')", "Pick an exact date from calendar")
        );

        if (choices.StartsWith("Specify"))
        {
            int days = AnsiConsole.Ask<int>("[bold green]Enter number of days from now:[/]");
            dueDate = DateTime.Now.AddDays(days);
            return dueDate;
        }
        else
        {
            int year;
            int day;
            
            while (true)
            {
                year = AnsiConsole.Ask<int>("[bold green]Enter year (e.g., 2025):[/]");

                if (year >= DateTime.Now.Year)
                    break;
                else
                    AnsiConsole.MarkupLine("[bold yellow]Invalid year. try again.[/]");
            }

            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames
                       .Where(m => !string.IsNullOrEmpty(m))
                       .ToArray();

            var selectedMonth = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
               .Title("[bold yellow]? Select month:[/]")
               .AddChoices(months)
            );
            while (true)
            {
                day = AnsiConsole.Ask<int>("[bold green]Enter day (1-31):[/]");

                if (day >= 1 && day <= 31)
                    break;
                else
                    AnsiConsole.MarkupLine("[bold yellow]Invalid day of month. try again.[/]");
            }

            int monthNumber = Array.IndexOf(months, selectedMonth) + 1;

            dueDate = new DateTime(year, monthNumber, day);
            AnsiConsole.MarkupLine($"[bold green]✅ Due date set to: {dueDate:yyyy-MM-dd} ({selectedMonth})[/]");
            return dueDate;
        }
    }
}