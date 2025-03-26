using TaskMan.Application;
using TaskMan.Domain;
using Spectre.Console;

namespace TaskMan.CLI;
class TaskCLI
{
    private readonly CommandHandler _commandHandler;

    public TaskCLI(CommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public void Start()
    {
        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
                continue;

            _commandHandler.ExecuteCommand(input);
        }
    }
}