using System.Text.Json;
using TaskMan.Domain;
using TaskMan.Common;
using FuzzySharp;

namespace TaskMan.Repository;

class TaskRepository : ITaskRepository
{
    public TaskRepository() { }

    public bool AddTask(TaskItem task)
    {
        if (task is null)
        {
            Console.WriteLine("Invalid Task.");
            return false;
        }

        List<TaskItem> taskItems = LoadTasks();
        taskItems.Add(task);

        SaveTasks(taskItems);

        return true;
    }

    public bool SaveTasks(List<TaskItem> tasks)
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            tasks[i].Id = i + 1;
        }

        try
        {
            string updatedJson = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(Constants.DBFilePath, updatedJson);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"error occurred while Saving Tasks.\n{e.Message}\n {e.InnerException ?? null}");
            return false;
        }
    }

    public List<TaskItem> LoadTasks()
    {
        if (File.Exists(Constants.DBFilePath))
        {
            try
            {
                string json = File.ReadAllText(Constants.DBFilePath);
                return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"error occurred while Reading Tasks.\n{e.Message}\n {e.InnerException ?? null}");
            }
        }
        return new List<TaskItem>();
    }

    public bool RemoveByID(int taskID)
    {
        List<TaskItem> tasks = LoadTasks();

        TaskItem? taskToRemove = tasks.FirstOrDefault(t => t.Id == taskID);

        if (taskToRemove is null)
        {
            return false;
        }

        bool removed = tasks.Remove(taskToRemove);
        SaveTasks(tasks);
        return true;
    }

    public bool RemoveAll()
    {
        List<TaskItem> tasks = [];
        bool result = SaveTasks(tasks);
        return result;
    }

    public List<TaskItem> SearchTask(string searchString)
    {
        return LoadTasks()
        .Where(item => searchString.Split(' ')
        .Any(word => Fuzz.PartialRatio(item.Title.ToLower(), word) > 70))
        .ToList();
    }

    public List<TaskItem> GetByPriority(Priority priority)
    {
        return LoadTasks()
        .Where(task => task.Priority == priority)
        .ToList();
    }

    public bool ToggleTask(int index)
    {
        List<TaskItem> tasks = LoadTasks();

        tasks[index].IsCompleted = !tasks[index].IsCompleted;

        return SaveTasks(tasks);
    }

    public List<TaskItem> GetCompleted()
    {
        return LoadTasks().Where(task => task.IsCompleted).ToList();
    }
}