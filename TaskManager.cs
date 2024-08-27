using System.Text.Json;

namespace Task_Tracer_CLI;

public class TaskManager
{
    private const string FilePath = "tasks.json";
    private readonly List<Task> _tasks = LoadTasksFromFile();
    private const string ExceptionMessage = "\nAn error occurred, please try again. If the error persists, please contact support.\n";
    /// <summary>Loads tasks from a JSON file.</summary>
    /// <returns>A list of tasks loaded from the JSON file. If the file does not exist or is empty, an empty list is returned.</returns>
    /// <exception cref="Exception">Thrown if an error occurs while reading from the file or deserializing the tasks.</exception>
    private static List<Task> LoadTasksFromFile()
    {
        try
        {
            if (!File.Exists(FilePath)) return new List<Task>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();
        }
        catch (Exception)
        {
            Console.WriteLine(ExceptionMessage);
            throw;
        }
    }
    /// <summary>Adds a new task to the list and saves it to the JSON file.</summary>
    /// <param name="description">The description of the task to be added.</param>
    /// <exception cref="Exception">Thrown if an error occurs while adding the task or saving it to the file.</exception>
    public void AddTask(string description)
    {
        try
        {
            var newId = _tasks.Count == 0 ? 1 : _tasks.Max(t => t.Id) + 1;
            var newTask = new Task(newId, description, "todo", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
            _tasks.Add(newTask);
            SaveTasks();
            Console.WriteLine($"\nTask added successfully (ID: {newId})\n");
        }
        catch (Exception)
        {
            Console.WriteLine(ExceptionMessage);
            throw;
        }
    }
    /// <summary>Updates an existing task in the list and saves the updated list to the JSON file.</summary>
    /// <param name="id">The ID of the task to be updated.</param>
    /// <param name="description">The new description for the task.</param>
    /// <exception cref="Exception">Thrown if an error occurs while updating the task or saving it to the file.</exception>
    public void UpdateTask(int id, string description)
    {
        try
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("\nTask not found. Please try again.\n");
                return;
            }
            task.Update(description);
            SaveTasks();
            Console.WriteLine($"\nTask updated successfully (ID: {task.Id})\n");
        }
        catch (Exception)
        {
            Console.WriteLine(ExceptionMessage);
            throw;
        }
    }
    /// <summary>Deletes an existing task from the list and saves the updated list to the JSON file.</summary>
    /// <param name="id">The ID of the task to be deleted.</param>
    /// <exception cref="Exception">Thrown if an error occurs while deleting the task or saving the updated list to the file.</exception>
    public void DeleteTask(int id)
    {
        try
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("\nTask not found. Please try again.\n");
                return;
            }
            _tasks.Remove(task);
            SaveTasks();
            Console.WriteLine($"\nTask deleted successfully (ID: {task.Id})\n");
        }
        catch (Exception)
        {
            Console.WriteLine(ExceptionMessage);
            throw;
        }
    }
    /// <summary>Marks an existing task as "in-progress" and saves the updated list to the JSON file.</summary>
    /// <param name="id">The ID of the task to be marked as "in-progress".</param>
    /// <exception cref="Exception">Thrown if an error occurs while updating the task or saving the updated list to the file.</exception>
    public void MarkInProgress(int id)
    {
        try
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("\nTask not found. Please try again.\n");
                return;
            }
            task.MarkInProgress();
            SaveTasks();
            Console.WriteLine($"\nTask marked in progress successfully (ID: {task.Id})\n");
        }
        catch (Exception)
        {
            Console.WriteLine(ExceptionMessage);
            throw;
        }
    }
    /// <summary>Marks an existing task as "done" and saves the updated list to the JSON file.</summary>
    /// <param name="id">The ID of the task to be marked as "done".</param>
    /// <exception cref="Exception">Thrown if an error occurs while updating the task or saving the updated list to the file.</exception>
    public void MarkDone(int id)
    {
        try
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("\nTask not found. Please try again.\n");
                return;
            }
            task.MarkAsDone();
            SaveTasks();
            Console.WriteLine($"\nTask marked done successfully (ID: {task.Id})\n");
        }
        catch (Exception)
        {
            Console.WriteLine(ExceptionMessage);
            throw;
        }
    }
    /// <summary>Lists tasks with an optional status filter and prints them to the console.</summary>
    /// <param name="status">The status of the tasks to be listed. If null or empty, all tasks are listed.</param>
    /// <exception cref="Exception">Thrown if an error occurs while listing tasks.</exception>
    public void ListTasks(string status)
    {
        try
        {
            var tasks = string.IsNullOrEmpty(status)
                ? _tasks.OrderByDescending(t => t.UpdatedAt).ToList()
                : _tasks.Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(t => t.UpdatedAt)
                    .ToList();

            PrintTasks(tasks);
        }
        catch (Exception)
        {
            Console.WriteLine(ExceptionMessage);
            throw;
        }
    }
    /// <summary>Prints a list of tasks to the console in a tabular format.</summary>
    /// <param name="tasks">The list of tasks to be printed.</param>
    private static void PrintTasks(List<Task> tasks)
    {
        // Check if there are any tasks
        if (tasks.Count == 0)
        {
            Console.WriteLine("\nNo tasks found.\n");
            return;
        }
        // Define padding for each column
        const int padding = 5;
        // Define column headers
        const string idHeader = "ID";
        const string descriptionHeader = "Description";
        const string statusHeader = "Status";
        const string createdAtHeader = "Created At";
        const string updatedAtHeader = "Updated At";
        // Calculate the maximum width for each column
        var idWidth = Math.Max(idHeader.Length, tasks.Max(task => task.Id.ToString().Length));
        var descriptionWidth = Math.Max(descriptionHeader.Length, tasks.Max(task => task.Description.Length));
        var statusWidth = Math.Max(statusHeader.Length, tasks.Max(task => task.Status.Length));
        var createdAtWidth = Math.Max(createdAtHeader.Length, tasks.Max(task => task.CreatedAt.Length));
        var updatedAtWidth = Math.Max(updatedAtHeader.Length, tasks.Max(task => task.UpdatedAt.Length));
        // Print the header
        Console.WriteLine(
            $"\n{idHeader.PadRight(idWidth + padding)}" +
            $"{descriptionHeader.PadRight(descriptionWidth + padding)}" +
            $"{statusHeader.PadRight(statusWidth + padding)}" +
            $"{createdAtHeader.PadRight(createdAtWidth + padding)}" +
            $"{updatedAtHeader.PadRight(updatedAtWidth)}");
        // Print the separator line
        const int paddingContents = 4;
        Console.WriteLine(new string('-',
            idWidth + descriptionWidth + statusWidth + createdAtWidth + updatedAtWidth +
            paddingContents * padding)); // Adjust for padding
        // Print each task
        foreach (var task in tasks)
        {
            Console.WriteLine(
                $"{task.Id.ToString().PadRight(idWidth + padding)}" +
                $"{task.Description.PadRight(descriptionWidth + padding)}" +
                $"{task.Status.PadRight(statusWidth + padding)}" +
                $"{task.CreatedAt.PadRight(createdAtWidth + padding)}" +
                $"{task.UpdatedAt.PadRight(updatedAtWidth)}");
        }
        Console.WriteLine();
    }
    /// <summary>Saves the current list of tasks to the JSON file.</summary>
    /// <exception cref="Exception">Thrown if an error occurs while saving the task list to the file.</exception>
    private void SaveTasks()
    {
        try
        {
            var json = JsonSerializer.Serialize(_tasks);
            File.WriteAllText(FilePath, json);
        }
        catch (Exception)
        {
            Console.WriteLine(ExceptionMessage);
            throw;
        }
    }
}
