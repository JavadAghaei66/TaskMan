using TaskMan.Application;

namespace TaskMan.CLI;
class CommandHandler
{
    private readonly Dictionary<string, CommandBase> _commands;

    public CommandHandler(TaskService taskService)
    {
        _commands = new Dictionary<string, CommandBase>(){
            {"list",new ListCommand(taskService)},
            {"add",new AddCommand(taskService)},
            {"remove",new RemoveCommand(taskService)},
            {"search",new SearchCommand(taskService)},
            {"exit",new ExitCommand()},
        };
    }

    public void ExecuteCommand(string commandString)
    {

        if (string.IsNullOrWhiteSpace(commandString))
        {
            Console.WriteLine("invalid command!");
            return;
        }

        // remove extra spaces between commands and split with ' '
        string[] args = commandString.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        string commandName = args[0].ToLower();
        string[] commandArgs = args.Skip(1).ToArray();

        // search for command and run execute method of the class object
        if (_commands.TryGetValue(commandName, out var command))
        {
            command.Execute(commandArgs);
        }
        else
        {
            Console.WriteLine($"Command '{commandName}' not found.");
        }
    }
}