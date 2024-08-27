namespace Task_Tracer_CLI;

public class Task
{
    public int Id { get; }
    public string Description { get; private set; }
    public string Status { get; private set; } // "todo", "in-progress", "done"
    public string CreatedAt { get; }
    public string UpdatedAt { get; private set; }
    /// <summary>Initializes a new instance of the <see cref="Task"/> class.</summary>
    /// <param name="id">The unique identifier for the task.</param>
    /// <param name="description">The description of the task.</param>
    /// <param name="status">The current status of the task.</param>
    /// <param name="createdAt">The date and time when the task was created.</param>
    /// <param name="updatedAt">The date and time when the task was last updated.</param>
    public Task(int id, string description, string status, string createdAt, string updatedAt)
    {
        Id = id;
        Description = description.Trim('\"');
        Status = status;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
    /// <summary>Updates the description of the task and sets the current date and time as the updated time.</summary>
    /// <param name="description">The new description for the task.</param>
    public void Update(string description)
    {
        Description = description.Trim('\"');
        UpdatedAt = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
    }
    /// <summary>Marks the task as "in-progress" and updates the last updated timestamp.</summary>
    public void MarkInProgress()
    {
        Status = "in-progress";
        UpdatedAt = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
    }
    /// <summary>Marks the task as "done" and updates the last updated timestamp.</summary>
    public void MarkAsDone()
    {
        Status = "done";
        UpdatedAt = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
    }
}
