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

    public void ExecuteCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please enter a command.");
            return;
        }

        string commandName = args[0].ToLower();
        string[] commandArgs = args.Skip(1).ToArray();

        // search for command and run execute method of the class object
        if (_commands.TryGetValue(commandName, out var command))
        {
            command.Execute(commandArgs);
        }
        else
        {
            Console.WriteLine("Command not found.");
        }
    }
}