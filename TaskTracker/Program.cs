using TaskTracker.Services;
using TaskTracker.Models;
using System.Text.Json;

/// The application should run from the command line, accept user actions and inputs as arguments, and store the tasks in a JSON file. The user should be able to:
//
//Add, Update, and Delete tasks - Done
//Mark a task as in progress or done - 
//List all tasks
//List all tasks that are done
//List all tasks that are not done
//List all tasks that are in progress
//Here are some constraints to guide the implementation:

//You can use any programming language to build this project.
//Use positional arguments in command line to accept user inputs.
//Use a JSON file to store the tasks in the current directory.
//The JSON file should be created if it does not exist.
//Use the native file system module of your programming language to interact with the JSON file.
//Do not use any external libraries or frameworks to build this project.
//Ensure to handle errors and edge cases gracefully.

HelperService helper = new();
PrinterService printer = new();
void Main()
{
    bool isRunning = true;
    while(isRunning)
    {
        Console.WriteLine("Please input a valid command");
        string? input = Console.ReadLine();
        if(String.IsNullOrEmpty(input))
        {
            Console.WriteLine("A command cannot be empty, please try again");
            break;
        }

        string[] cmds = helper.ParseCommands(input);

        // Check first command is correct, i.e. is task-cli
        if(cmds[0] != "task-cli")
        {
            Console.WriteLine(@"All valid must begin with 'task-cli'
Please use the 'help' command for more information");
        }

        if(cmds.Length < 2)
        {
            Console.WriteLine("Try again");
            continue;
        }

        // Switching between commands
        switch(cmds[1])
        {
            case "add":
                AddTask(cmds);
                break;
            case "update":
                UpdateTaskDescription(cmds);
                break;
            case "delete":
                DeleteTask(cmds);
                break;
            case "exit":
                isRunning = false;
                break;
            case "help":
                printer.PrintHelpMessage();
                break;
            case "mark":
                MarkTask(cmds);
                break;
            case "list":
                ListTasks(cmds);
                break;
            default:
                printer.PrintInvalidArgumentMessage();
                break;
        }

    }
}

List<JsonTask>? ReadJsonFile()
{
    try
    {
        string jsonPath = helper.GetJsonPath() ??
            throw new Exception("The path of the JSON file is null");
        using StreamReader reader = new(jsonPath);
        string text = reader.ReadToEnd();
        if(String.IsNullOrEmpty(text))
        {
            text = "[]";
        }
        List<JsonTask>? tasks = JsonSerializer.Deserialize<List<JsonTask>>(text);
        return tasks;
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Error: {ex}");
        return null;
    }
}
#region methods
#region Create
void AddTask(string[] cmds)
{
    try
    {
        if(cmds.Length < 3)
        {
            throw new IndexOutOfRangeException("Argument for Task description needed");
        }
        string description = cmds[2];
        List<JsonTask>? tasks = ReadJsonFile();
        if(tasks == null)
        {
            return;
        }
        JsonTask task = new(helper.GetHighestId(tasks) + 1, description ?? String.Empty);
        tasks.Add(task);
        WriteTasksToFile(tasks);
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex);
    }
}
#endregion
#region Update
void UpdateTaskDescription(string[] cmds)
{
    try
    {
        if(cmds.Length < 4)
        {
            throw new IndexOutOfRangeException("Argument for Task id and description needed");
        }
        int id = Int32.Parse(cmds[2]);
        string description = cmds[3];

        List<JsonTask>? tasks = ReadJsonFile();
        if(tasks == null)
        {
            return;
        }
        // Find the task
        JsonTask? foundTask = tasks.Find(task => task.Id == id);
        if(foundTask == null)
        {
            Console.WriteLine($"Could not find Task with Id: {id}");
            return;
        }
        foundTask.Description = description;
        foundTask.UpdatedAt = DateTime.Now;
        WriteTasksToFile(tasks);
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex);
    }

}
void UpdateTaskStatus(int id, JsonTask.TaskStatus taskStatus)
{
    try
    {
        List<JsonTask>? tasks = ReadJsonFile();
        if(tasks == null)
        {
            return;
        }
        // Find the task
        JsonTask? foundTask = tasks.Find(task => task.Id == id);
        if(foundTask == null)
        {
            Console.WriteLine($"Could not find Task with Id: {id}");
            return;
        }
        foundTask.Status = taskStatus;
        foundTask.UpdatedAt = DateTime.Now;
        WriteTasksToFile(tasks);
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex);
    }
}
#endregion
#region Delete
void DeleteTask(string[] cmds)
{
    try
    {
        if(cmds.Length < 3)
        {
            throw new IndexOutOfRangeException("Argument for Task id needed");
        }
        int id = Int32.Parse(cmds[2]);
        List<JsonTask>? tasks = ReadJsonFile();
        if(tasks == null)
        {
            return;
        }
        List<JsonTask> filteredTasks = tasks.Where(task => task.Id != id)
                                            .ToList<JsonTask>();
        WriteTasksToFile(filteredTasks);
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex);
    }
}
#endregion
void WriteTasksToFile(List<JsonTask> tasks)
{
    string json = JsonSerializer.Serialize<List<JsonTask>>(tasks);
    string jsonPath = helper.GetJsonPath();
    File.WriteAllText(jsonPath, json);
}
void PrintAllTasks()
{
    List<JsonTask>? tasks = ReadJsonFile();
    if(tasks == null || tasks.Count == 0)
    {
        printer.PrintNoTasks();
    }
    printer.PrintTableHeader();
    foreach(JsonTask task in tasks)
    {
        printer.PrintSingleTask(task);
    }
}
#endregion


void MarkTask(string[] cmds)
{
    if(cmds.Length < 4)
    {
        throw new IndexOutOfRangeException("Argument for Task id and status needed");
    }
    int id = Int32.Parse(cmds[2]);
    string status = cmds[3];
    switch(status)
    {
        case "done":
            UpdateTaskStatus(id, JsonTask.TaskStatus.done);
            break;
        case "in-progress":
            UpdateTaskStatus(id, JsonTask.TaskStatus.inProgress);
            break;
        default:
            Console.WriteLine($"'{status}' is not a valid task status");
            break;
    }
}

void ListTasks(string[] cmds)
{
    if(cmds.Length == 2)
    {
        PrintAllTasks();
        return;
    }

    string listCommand = cmds[2];
    switch(listCommand)
    {
        case "done":
            ListTasksByStatus(JsonTask.TaskStatus.done);
            break;
        case "in-progress":
            ListTasksByStatus(JsonTask.TaskStatus.inProgress);
            break;
        case "todo":
            ListTasksByStatus(JsonTask.TaskStatus.todo);
            break;
        default:
            break;
    }
    return;
}

void ListTasksByStatus(JsonTask.TaskStatus taskStatus)
{
    List<JsonTask>? tasks = ReadJsonFile();
    if(tasks == null || tasks.Count == 0)
    {
        printer.PrintNoTasks();
        return;
    }
    List<JsonTask>? filteredTasks = tasks?
                                    .Where(task => task.Status == taskStatus)
                                    .ToList();
    if(filteredTasks == null || filteredTasks.Count == 0)
    {
        printer.PrintNoTasks(taskStatus);
        return;
    }
    printer.PrintTableHeader();
    foreach(JsonTask task in filteredTasks)
    {
        printer.PrintSingleTask(task);
    }
}
Main();