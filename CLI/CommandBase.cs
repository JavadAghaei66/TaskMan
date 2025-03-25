public abstract class CommandBase
{
    public abstract string Name { get; }
    public abstract void Execute(string[] args);
}