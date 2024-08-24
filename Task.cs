namespace Task_Tracer_CLI;

public class Task(int id, string description)
{
    public int Id { get; } = id;
    public  string Description { get; set; } = description;
    public  TaskStatus Status { get; set; } = TaskStatus.Todo; // todo, in-progress, done
    private DateTime CreatedAt { get; } = DateTime.Now;
    private DateTime UpdatedAt { get; set; } = DateTime.Now;

    public void Update(string description)
    {
        Description = description;
        UpdatedAt = DateTime.Now;
    }
    
    public void MarkInProgress()
    {
        Status = TaskStatus.InProgress;
        UpdatedAt = DateTime.Now;
    }
    
    public void MarkAsDone()
    {
        Status = TaskStatus.Done;
        UpdatedAt = DateTime.Now;
    }
    
    public override string ToString()
    {
        return $"Id: {Id}, Description: {Description}, Status: {Status}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}";
    }
}