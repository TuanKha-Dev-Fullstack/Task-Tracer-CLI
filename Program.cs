namespace Task_Tracer_CLI;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine(
                "No command provided. Use 'add', 'update', 'delete', 'mark-in-progress', 'mark-done', 'list', 'list done', 'list in-progress', 'list todo'");
        }
        else
        {
            var taskManager = new TaskManager();
            var command = args[0];

            switch (command)
            {
                case "add":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Usage: task-cli add \"Task description\"");
                    }
                    else
                    {
                        var description = args[1];
                        taskManager.AddTask(description);
                    }

                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
    }
}