using TaskMan.Application;
using TaskMan.Domain;
using Spectre.Console;

namespace TaskMan.CLI;
class TaskCLI
{
    private readonly TaskService _taskService;
    private readonly CommandHandler _commandHandler;

    public TaskCLI(TaskService taskService, CommandHandler commandHandler)
    {
        _taskService = taskService;
        _commandHandler = commandHandler;
    }

    public void Start()
    {
        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
                continue;

            _commandHandler.ExecuteCommand(input);
        }
    }

    private void AddTask()
    {
        // gettig new task 
        Console.WriteLine("Enter Task Title:");
        string title = Console.ReadLine()!;
        Console.WriteLine("Enter Task Description:");
        string description = Console.ReadLine()!;
        Console.WriteLine("Enter Task Priority (High/Medium/Low):");
        string priority = Console.ReadLine()!;

        // create new task
        _taskService.AddTask(title, description, priority, false);
    }

    private void RemoveTask()
    {
        Console.WriteLine("Enter Task ID that you want to be removed from your list:");
        int userinput = int.Parse(Console.ReadLine()!);
        _taskService.RemoveTaskByID(userinput);
    }

    private void ListTasks()
    {
        List<TaskItem> taskItems = _taskService.LoadTasks();

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
                item.Description,
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