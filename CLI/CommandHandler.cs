using TaskMan.Application;

namespace TaskMan.CLI;
class CommandHandler
{
    private readonly Dictionary<string, CommandBase> _commands;

    public CommandHandler(TaskService taskService)
    {
        _commands = new Dictionary<string, CommandBase>(){
            {"list",new ListCommand(taskService)}
        };
    }
}