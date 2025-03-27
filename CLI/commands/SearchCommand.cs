using Spectre.Console;
using TaskMan.Application;
using TaskMan.CLI;
using TaskMan.Domain;

class SearchCommand : CommandBase
{
    private readonly TaskService _taskService;
    public override string Name => "search";

    public SearchCommand(TaskService taskService)
    {
        _taskService = taskService;
    }

    public override void Execute(string[] args)
    {
        if (args.Length == 0)
        {
            AnsiConsole.MarkupLine("[bold red]please add your task title after search command.[/]");
            return;
        }
        else
        {
            string searchString = string.Join(" ", args);

            List<TaskItem> tasks = _taskService.LoadTasks();
            List<TaskItem> searchResult = tasks.Where(item => item.Title.Contains(searchString)).ToList();


            // draw result table 
            Table table = new();
            if (searchResult.Count > 0)
                TableDrawer.DrawTaskTable(table, searchResult);


            var panel = new Panel(searchResult.Count > 0 ? table : new Markup("[red] Task list is empty );[/]"))
            {
                Header = new PanelHeader("[bold yellow] Task List [/]").Centered(),
                Border = BoxBorder.Rounded
            };

            AnsiConsole.Write(panel);
        }
    }
}