using Spectre.Console;
using TaskMan.Application;
using TaskMan.Domain;

namespace TaskMan.CLI;
class ListCommand : CommandBase
{
    private readonly TaskService _taskService;
    public ListCommand(TaskService taskService)
    {
        _taskService = taskService;
    }

    public override string Name => "list";

    public override void Execute(string[] args)
    {
        List<TaskItem> taskItems = _taskService.LoadTasks();

        if (args.Length > 0 && (args[0] == "-high" || args[0] == "-h"))
        {
            // list high priority if  needed
            Console.WriteLine("loading high priority tasks...");

        }

        var table = new Table();

        table.AddColumn("[bold yellow]ID[/]");
        table.AddColumn("[bold cyan]Title[/]");
        table.AddColumn("[bold green]Description[/]");
        table.AddColumn("[bold red]Priority[/]");
        table.AddColumn("[bold blue]Due Date[/]");
        table.AddColumn("[bold magenta]Completed[/]");

        table.ShowRowSeparators();


        foreach (var item in taskItems)
        {
            table.AddRow(
                item.Id.ToString(),
                item.Title,
                string.IsNullOrEmpty(item.Description) ? "No Description" : item.Description,
                $"[bold {GetPriorityColor(item.Priority)}]{item.Priority.ToString()}[/]",
                item.DueDate?.ToString("yyyy-MM-dd") ?? "[grey]No Due Date[/]",
                item.IsCompleted ? "[green]✔[/]" : "[red]✘[/]"
            );
        }

        var panel = new Panel(table)
        {
            Header = new PanelHeader("[bold yellow] Task List [/]").Centered(),
            Border = BoxBorder.Rounded
        };

        AnsiConsole.Write(panel);
    }

    private string GetPriorityColor(Priority priority)
    {
        return priority switch
        {
            Priority.Low => "green",
            Priority.Medium => "yellow",
            Priority.High => "red",
            _ => "white"
        };
    }
}
