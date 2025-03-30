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
        List<TaskItem> taskItems = [];
        Table table = new();
        string header = "Task List";


        Dictionary<string, Priority> priorityMap = new()
        {
            {"-high",Priority.High} , {"-h",Priority.High},
            {"-medium",Priority.Medium} , {"-m",Priority.Medium},
            {"-low",Priority.Low} , {"-l",Priority.Low}
        };

        if (args.Length == 0)
        {
            taskItems = _taskService.LoadTasks();
        }
        else if (args.Length == 1 && priorityMap.TryGetValue(args[0], out Priority priority))
        {
            taskItems = _taskService.GetByPriority(priority);
            header = $"{priority} Priority Tasks";
        }
        else if (args.Length == 1 && args[0] == "--completed")
        {
            taskItems = _taskService.GetCompleted();
            header = "Completed Tasks";
        }
        else
        {
            AnsiConsole.MarkupLine($"[bold red]Invalid argument: '{args[0]}'[/]");
            return;
        }

        TableDrawer.DrawTaskTable(
            table: table,
            tasks: taskItems,
            pannelHeader: header
        );
    }
}
