namespace Task_Tracer_CLI;

public static class Program
{
    private static void Main()
    {
        var taskManager = new TaskManager();
        string? input;
        Console.WriteLine("======================Task Tracer CLI======================");
        Console.WriteLine("Use \"help\" for more information.");
        do
        {
            // handle user input
            Console.Write("task-cli> ");
            input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input)) continue;
            const int amountArgs = 3;
            var commandParts = input.Split(' ', amountArgs); // divide input into arguments array
            var command = commandParts[0].ToLower(); // get function name from first argument
            var argument1 = commandParts.Length > 1 ? commandParts[1] : ""; // get arguments from rest of array
            var argument2 = commandParts.Length > 2 ? commandParts[2] : ""; // get arguments from rest of array
            switch (command)
            {
                // case for add task
                case "add":
                    taskManager.AddTask(argument1);
                    break;
                case "update":
                    taskManager.UpdateTask(int.Parse(argument1), argument2);
                    break;
                default:
                    Console.WriteLine("Unknown command. Use \"help\" for a list of available commands.");
                    break;
            }
        } while (input != "exit");
    }
}