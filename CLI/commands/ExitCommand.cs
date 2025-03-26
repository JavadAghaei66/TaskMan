
namespace TaskMan.CLI;

class ExitCommand : CommandBase
{
    public override string Name => "exit";

    public override void Execute(string[] args)
    {
        Environment.Exit(0);
        return;
    }
}