using Spectre.Console;
using TaskMan.Application;
using TaskMan.CLI;
using TaskMan.Domain;

class ToggleCommand : CommandBase
{
    private readonly TaskService _taskService;
    public override string Name => "toggle";
    public ToggleCommand(TaskService taskService)
    {
        _taskService = taskService;
    }

    public override void Execute(string[] args)
    {
        Console.Clear();

        while (true)
        {
            List<TaskItem> tasks = _taskService.LoadTasks();

            List<string> taskName = [];

            foreach (var item in tasks)
            {
                string status = item.IsCompleted ? "[green]✓[/] " : "[red]✗[/] ";
                taskName.Add($"{status} {item.Title}");
            }
            taskName.Add("[red]⬅ Back[/]");
            var selectedTask = AnsiConsole.Prompt(
                        new SelectionPrompt<string>().Title("select a task to toggle:").AddChoices(taskName)
                    );

            if (selectedTask == "[red]⬅ Back[/]")
                break;

            var index = taskName.IndexOf(selectedTask);
            if (index >= 0) _taskService.ToggleTask(index);
        }
    }
}