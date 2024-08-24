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
        var newId = _tasks.Max(t => t.Id) + 1;
        var newTask = new Task(newId, description);
        _tasks.Add(newTask);
        Console.WriteLine($"Task {newTask.Id} added successfully.");
    }
    
    private void SaveTasks()
    {
        var json = JsonSerializer.Serialize(_tasks);
        File.WriteAllText(FilePath, json);
    }
}