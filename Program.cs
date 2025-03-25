using TaskMan.Application;
using TaskMan.Repository;
using TaskMan.CLI;

class Program
{
    static void Main(string[] args)
    {
        TaskRepository tRepository = new();
        TaskService tService = new(tRepository);
        CommandHandler cHandler = new(tService);
        TaskCLI tCLI = new(tService,cHandler);

        // start Interface
        tCLI.Start();
    }
}
