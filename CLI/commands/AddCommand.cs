using TaskMan.Application;
using Spectre.Console;
using TaskMan.Domain;

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
    
        string title = AnsiConsole.Ask<string>("[bold blue]Enter task title:[/]");
        string? description = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold blue]Enter task description (or leave empty):[/]")
                .AllowEmpty()
            );

        Priority priority = AnsiConsole.Prompt(
            new SelectionPrompt<Priority>()
            .Title("[bold]select task priority:[/]")
            .AddChoices(Priority.Low, Priority.Medium, Priority.High)
        );

        // create new task
        _taskService.AddTask(title, description, priority.ToString(), false);

        AnsiConsole.MarkupLine("[bold green]Task added successfully.[/]");

    }
}