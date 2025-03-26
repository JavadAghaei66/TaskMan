using Spectre.Console;
using TaskMan.Application;

namespace TaskMan.CLI;

class RemoveCommand : CommandBase
{
    private readonly TaskService _taskSrvice;

    public RemoveCommand(TaskService taskService)
    {
        _taskSrvice = taskService;
    }
    public override string Name => "remove";

    public override void Execute(string[] args)
    {
        if (args.Length > 0 && (args[0] == "-a" || args[0] == "-all"))
        {
            _taskSrvice.RemoveAllTasks();
            return;
        }

        if (args.Length > 0)
        {
            int taskID = int.Parse(args[0]);
            _taskSrvice.RemoveTaskByID(taskID);
            return;
        }

        int inpupt = AnsiConsole.Ask<int>("Enter task ID:");
        _taskSrvice.RemoveTaskByID(inpupt);
    }
}