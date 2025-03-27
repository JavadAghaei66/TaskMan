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

        if (args.Length == 0)
        {
            taskItems = _taskService.LoadTasks();

            TableDrawer.DrawTaskTable(
                table: table,
                tasks: taskItems,
                pannelHeader: "Task List"
            );

            return;
        }

        if (args.Length == 1 && (args[0] == "-high" || args[0] == "-h"))
        {
            taskItems = _taskService.GetByPriority(Priority.High);

            TableDrawer.DrawTaskTable(
                table: table,
                tasks: taskItems,
                pannelHeader: "High Priority Tasks"
            );
            return;
        }

        if (args.Length == 1 && (args[0] == "-medium" || args[0] == "-m"))
        {
            taskItems = _taskService.GetByPriority(Priority.Medium);
            TableDrawer.DrawTaskTable(
                table: table,
                tasks: taskItems,
                pannelHeader: "Medium Priority Tasks"
            );
            return;
        }

        if (args.Length == 1 && (args[0] == "-low" || args[0] == "-l"))
        {
            taskItems = _taskService.GetByPriority(Priority.Low);
            TableDrawer.DrawTaskTable(
                table: table,
                tasks: taskItems,
                pannelHeader: "Low Priority Tasks"
            );
            return;
        }

        AnsiConsole.MarkupLine($"[bold red]Invalid argument: '{args[0]}'[/]");
    }
}
