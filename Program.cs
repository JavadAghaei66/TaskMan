using TaskMan.Application;
using TaskMan.Repository;
using TaskMan.CLI;

class Program
{
    static void Main(string[] args)
    {
        TaskRepository tRepository = new();
        TaskService tService = new(tRepository);
        TaskCLI tCLI = new(tService);

        // start Interface
        tCLI.Start();
    }
}
