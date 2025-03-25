using Spectre.Console.Cli;
using TaskMan.Application;

public class ListCommand : CommandBase
{
    private readonly TaskService _taskService;

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
