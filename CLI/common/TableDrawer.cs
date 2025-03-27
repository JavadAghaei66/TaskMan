using Spectre.Console;
using TaskMan.Domain;

class TableDrawer
{
    public static void DrawTaskTable(Table table, List<TaskItem> tasks)
    {
        table.AddColumn("[bold yellow]ID[/]");
        table.AddColumn("[bold cyan]Title[/]");
        table.AddColumn("[bold green]Description[/]");
        table.AddColumn("[bold red]Priority[/]");
        table.AddColumn("[bold blue]Due Date[/]");
        table.AddColumn("[bold magenta]Completed[/]");

        table.ShowRowSeparators();

        foreach (var item in tasks)
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
    }

    public static string GetPriorityColor(Priority priority)
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