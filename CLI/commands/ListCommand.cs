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

        if (args.Length == 0)
        {
            Table table = new();

            if (taskItems.Count > 0)
            {
                TableDrawer.DrawTaskTable(table, taskItems);
            }

            var panel = new Panel(taskItems.Count > 0 ? table : new Markup("[red] Task list is empty );[/]"))
            {
                Header = new PanelHeader("[bold yellow] Task List [/]").Centered(),
                Border = BoxBorder.Rounded
            };

            AnsiConsole.Write(panel);
            return;
        }


        if (args.Length == 1 && (args[0] == "-high" || args[0] == "-h"))
        {
            // list high priority if  needed
            Console.WriteLine("loading high priority tasks...");
            return;
        }

        AnsiConsole.MarkupLine($"[bold red]Invalid argument: '{args[0]}'[/]");
    }
}
