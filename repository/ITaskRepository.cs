using TaskMan.Domain;

namespace TaskMan.Repository;

interface ITaskRepository
{
    bool AddTask(TaskItem task);
    List<TaskItem> LoadTasks();
    bool SaveTasks(List<TaskItem> tasks);
    bool RemoveByID(int taskID);
    bool RemoveAll();
    List<TaskItem> SearchTask(string searchString);
    List<TaskItem> GetByPriority(Priority priority);
    bool ToggleTask(int index);
    List<TaskItem> GetCompleted();
}