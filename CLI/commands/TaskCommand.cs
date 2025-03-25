using Spectre.Console.Cli;
using TaskMan.Application;

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
        // list command 
        if (args.Length > 0 && args[0] == "-high")
        {
            // list high priority 
        }
    }
}
