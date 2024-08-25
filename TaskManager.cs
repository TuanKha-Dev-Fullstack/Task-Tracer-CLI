using System.Text.Json;

namespace Task_Tracer_CLI;

public class TaskManager
{
    private const string FilePath = "tasks.json";
    private List<Task> _tasks;
    
    public TaskManager()
    {
        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            _tasks = JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();
        }
        else
        {
            _tasks = new List<Task>();
        }
    }

    public void AddTask(string description)
    {
        try
        {
            var newId = _tasks.Count == 0 ? 1 : _tasks.Max(t => t.Id) + 1;
            var newTask = new Task(newId, description);
            _tasks.Add(newTask);
            SaveTasks();
            Console.WriteLine($"Task added successfully (ID: {newId})");
        }
        catch (Exception)
        {
            Console.WriteLine("An error occurred, please try again. If the error persists, please contact support.");
            throw;
        }
    }
    
    public void UpdateTask(int id, string description)
    {
        try
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("Task not found. Please try again.");
                return;
            }
            task.Description = description.Trim('\"');
            SaveTasks();
            Console.WriteLine($"Task updated successfully (ID: {task.Id})");
        }
        catch (Exception)
        {
            Console.WriteLine("An error occurred, please try again. If the error persists, please contact support.");
            throw;
        }
    }
    
    private void SaveTasks()
    {
        var json = JsonSerializer.Serialize(_tasks);
        File.WriteAllText(FilePath, json);
    }
}