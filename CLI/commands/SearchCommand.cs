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

        string searchString = string.Join(" ", args);

        List<TaskItem> searchResult = _taskService.SearchTask(searchString);

        // draw result table 
        Table table = new();
        if (searchResult.Count > 0)
            TableDrawer.DrawTaskTable(table, searchResult);


        var panel = new Panel(searchResult.Count > 0 ? table : new Markup("[red] Task list is empty );[/]"))
        {
            Header = new PanelHeader("[bold yellow] Search Result [/]").Centered(),
            Border = BoxBorder.Rounded
        };

        AnsiConsole.Write(panel);
    }
}