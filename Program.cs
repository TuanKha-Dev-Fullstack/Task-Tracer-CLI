using System.Text.RegularExpressions;

namespace Task_Tracer_CLI;

public static partial class Program
{
    private static void Main()
    {
        var taskManager = new TaskManager();
        const string inputErrorMessage = "\nWrong input format. Use \"help\" for more information.\n";
        Console.WriteLine("======================Task Tracer CLI======================");
        Console.WriteLine("Use \"help\" for more information.\n");
        string? userInput;
        do
        {
            userInput = PromptUserForInput();
            if (string.IsNullOrEmpty(userInput)) continue;

            var commandParts = ParseInput(userInput, inputErrorMessage);
            if (commandParts.Length == 0) continue;

            var command = commandParts[0];
            var argument1 = GetArgument(commandParts, 1);
            var argument2 = GetArgument(commandParts, 2);

            ExecuteCommand(command, argument1, argument2, taskManager, inputErrorMessage);
        } while (userInput != "exit");
    }
    /// <summary>Prompts the user for input and returns the trimmed input string.</summary>
    /// <returns>The user's input.</returns>
    private static string? PromptUserForInput()
    {
        Console.Write("task-cli ");
        return Console.ReadLine()?.Trim();
    }
    /// <summary>Parses the user input using a regular expression.</summary>
    /// <param name="input">The input string to parse.</param>
    /// <param name="inputErrorMessage">The error message to display if the input is invalid.</param>
    /// <returns>An array of parsed command parts.</returns>
    private static string[] ParseInput(string input, string inputErrorMessage)
    {
        // Check for commands requiring quotes
        if ((!input.Contains("add") && !input.Contains("update")) || input.Count(c => c == '\"') % 2 == 0)
            return MyRegex().Matches(input)
                .Select(m => m.Value.Trim('"'))
                .ToArray();
        Console.WriteLine(inputErrorMessage);
        return [];
    }
    /// <summary>Retrieves the argument at the specified index from the command parts array.</summary>
    /// <param name="commandParts">The array of command parts.</param>
    /// <param name="index">The index of the argument to retrieve.</param>
    /// <returns>The argument at the specified index, or an empty string if out of bounds.</returns>
    private static string GetArgument(string[] commandParts, int index)
    {
        return commandParts.Length > index ? commandParts[index] : string.Empty;
    }
    /// <summary>Executes the appropriate command based on the command string and arguments.</summary>
    /// <param name="command">The command to execute.</param>
    /// <param name="argument1">The first argument for the command.</param>
    /// <param name="argument2">The second argument for the command.</param>
    /// <param name="taskManager">The task manager to perform operations on.</param>
    /// <param name="errorMessage">The error message to display for invalid inputs.</param>
    private static void ExecuteCommand(string command, string argument1, string argument2, TaskManager taskManager, string errorMessage)
    {
        switch (command)
        {
            case "add":
                if (string.IsNullOrEmpty(argument1))
                {
                    ShowError(errorMessage);
                }
                else
                {
                    taskManager.AddTask(argument1);
                }
                break;
            case "update":
                if (!int.TryParse(argument1, out var taskId) || string.IsNullOrEmpty(argument2))
                {
                    ShowError(errorMessage);
                }
                else
                {
                    taskManager.UpdateTask(taskId, argument2);
                }
                break;
            case "delete":
                if (!int.TryParse(argument1, out taskId))
                {
                    ShowError(errorMessage);
                }
                else
                {
                    taskManager.DeleteTask(taskId);
                }
                break;
            case "mark-in-progress":
                if (!int.TryParse(argument1, out taskId))
                {
                    ShowError(errorMessage);
                }
                else
                {
                    taskManager.MarkInProgress(taskId);
                }
                break;
            case "mark-done":
                if (!int.TryParse(argument1, out taskId))
                {
                    ShowError(errorMessage);
                }
                else
                {
                    taskManager.MarkDone(taskId);
                }
                break;
            case "list":
                if (argument1 != "todo" && argument1 != "in-progress" && argument1 != "done" && argument1 != "")
                {
                    ShowError(errorMessage);
                }
                else
                {
                    taskManager.ListTasks(argument1);
                }
                break;
            case "help":
                PrintHelp();
                break;
            default:
                Console.WriteLine("Unknown command. Use \"help\" for a list of available commands.");
                break;
        }
    }
    /// <summary>Displays an error message to the user.</summary>
    /// <param name="message">The error message to display.</param>
    private static void ShowError(string message)
    {
        Console.WriteLine(message);
    }
    [GeneratedRegex(@"""(?:[^""\\]|\\.)*""|[^ ]+")]
    private static partial Regex MyRegex();
    /// <summary>Prints the help message.</summary>
    private static void PrintHelp()
    {
        Console.WriteLine("\nAvailable commands and usage instructions:");
        Console.WriteLine("add \"<task>\" - Adds a new task.");
        Console.WriteLine("update <id> \"<task>\" - Updates the task with the given ID.");
        Console.WriteLine("delete <id> - Deletes the task with the given ID.");
        Console.WriteLine("mark-in-progress <id> - Marks the task with the given ID as in progress.");
        Console.WriteLine("mark-done <id> - Marks the task with the given ID as done.");
        Console.WriteLine("list - Lists all tasks.");
        Console.WriteLine("list todo - Lists tasks in the specified state.");
        Console.WriteLine("list in-progress - Lists tasks in the specified state.");
        Console.WriteLine("list done - Lists tasks in the specified state.");
        Console.WriteLine("exit - Exits the program.\n");
    }
}