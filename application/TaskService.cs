using TaskMan.Domain;
using TaskMan.Repository;

namespace TaskMan.Application;

class TaskService
{
    private readonly ITaskRepository _taskRepository;
    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public void AddTask(string title, string descripton, string priority, bool isCompleted, DateTime? dueDate = null, DateTime? completedAt = null)
    {
        List<TaskItem> tasks = _taskRepository.LoadTasks();

        TaskItem task = new()
        {
            Id = AutoIncrease(tasks),
            Title = title,
            Description = descripton,
            IsCompleted = isCompleted,
            DueDate = dueDate,
            CompletedAt = completedAt,
            Priority = ParsePriority(priority),
        };

        _taskRepository.AddTask(task);
    }

    public List<TaskItem> LoadTasks()
    {
        return _taskRepository.LoadTasks();
    }

    public void RemoveTaskByID(int taskID)
    {
        if (taskID <= 0)
        {
            Console.WriteLine("❌ Task ID cannot be zero or negative.");
            return;
        }

        bool result = _taskRepository.RemoveByID(taskID);

        if (!result)
        {
            Console.WriteLine("⚠️ Task not found! Try again.");
            return;
        }
        else
        {
            Console.WriteLine("✅ Task removed successfully.");
            return;
        }
    }


    public int AutoIncrease(List<TaskItem> tasks)
    {
        return tasks.Count == 0 ? 1 : tasks.Max(t => t.Id) + 1;
    }

    public Priority ParsePriority(string priority)
    {
        return priority.ToLower() switch
        {
            "low" => Priority.Low,
            "medium" => Priority.Medium,
            "high" => Priority.High,
            _ => throw new ArgumentException("Invalid priority value. Please enter High, Medium, or Low.")
        };
    }
}