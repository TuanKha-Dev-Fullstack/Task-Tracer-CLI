using System.Text.Json;

namespace Task_Tracer_CLI;

public class TaskManager
{
    private const string FilePath = "tasks.json";
    private readonly List<Task> _tasks;

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

    public void DeleteTask(int id)
    {
        try
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("Task not found. Please try again.");
                return;
            }

            _tasks.Remove(task);
            SaveTasks();
            Console.WriteLine($"Task deleted successfully (ID: {task.Id})");
        }
        catch (Exception)
        {
            Console.WriteLine("An error occurred, please try again. If the error persists, please contact support.");
            throw;
        }
    }

    public void MarkInProgress(int id)
    {
        try
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("Task not found. Please try again.");
                return;
            }

            task.Status = TaskStatus.InProgress;
            SaveTasks();
            Console.WriteLine($"Task marked in progress successfully (ID: {task.Id})");
        }
        catch (Exception)
        {
            Console.WriteLine("An error occurred, please try again. If the error persists, please contact support.");
            throw;
        }
    }

    public void MarkDone(int id)
    {
        try
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("Task not found. Please try again.");
                return;
            }

            task.Status = TaskStatus.Done;
            SaveTasks();
            Console.WriteLine($"Task marked done successfully (ID: {task.Id})");
        }
        catch (Exception)
        {
            Console.WriteLine("An error occurred, please try again. If the error persists, please contact support.");
            throw;
        }
    }

    public void ListTasks(string status)
    {
        if (string.IsNullOrEmpty(status))
        {
            PrintTasks(_tasks);
            return;
        }

        var tasks = _tasks.Where(t => t.Status.ToString().Equals(status, StringComparison.OrdinalIgnoreCase)).OrderBy(t => t.UpdatedAt).ToList();
        PrintTasks(tasks);
    }

    private static void PrintTasks(List<Task> tasks)
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
            return;
        }
        // Define padding for each column
        const int padding = 5;
        // Determine the maximum widths for each column based on both header and data
        const string idHeader = "ID";
        const string descriptionHeader = "Description";
        const string statusHeader = "Status";
        const string createdAtHeader = "Created At";
        const string updatedAtHeader = "Updated At";
        // Calculate the maximum width for each column
        var idWidth = Math.Max(idHeader.Length, tasks.Max(task => task.Id.ToString().Length));
        var descriptionWidth = Math.Max(descriptionHeader.Length, tasks.Max(task => task.Description.Length));
        var statusWidth = Math.Max(statusHeader.Length, tasks.Max(task => task.Status.ToString().Length));
        var createdAtWidth = Math.Max(createdAtHeader.Length,
            tasks.Max(task => task.CreatedAt.ToString("").Length));
        var updatedAtWidth = Math.Max(updatedAtHeader.Length,
            tasks.Max(task => task.UpdatedAt.ToString("").Length));
        // Print the header
        Console.WriteLine(
            $"{idHeader.PadRight(idWidth + padding)}" +
            $"{descriptionHeader.PadRight(descriptionWidth + padding)}" +
            $"{statusHeader.PadRight(statusWidth + padding)}" +
            $"{createdAtHeader.PadRight(createdAtWidth + padding)}" +
            $"{updatedAtHeader.PadRight(updatedAtWidth)}");
        // Print the separator line
        const int paddingContents = 4;
        Console.WriteLine(new string('-',
            idWidth + descriptionWidth + statusWidth + createdAtWidth + updatedAtWidth + paddingContents * padding)); // Adjust for padding
        // Print each task
        foreach (var task in tasks)
        {
            Console.WriteLine(
                $"{task.Id.ToString().PadRight(idWidth + padding)}" +
                $"{task.Description.PadRight(descriptionWidth + padding)}" +
                $"{task.Status.ToString().PadRight(statusWidth + padding)}" +
                $"{task.CreatedAt.ToString("").PadRight(createdAtWidth + padding)}" +
                $"{task.UpdatedAt.ToString("").PadRight(updatedAtWidth)}");
        }
        Console.WriteLine();
    }
    
    private void SaveTasks()
    {
        var json = JsonSerializer.Serialize(_tasks);
        File.WriteAllText(FilePath, json);
    }
}