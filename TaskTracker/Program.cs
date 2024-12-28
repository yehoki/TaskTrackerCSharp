// See https://aka.ms/new-console-template for more information
using TaskTracker.Services;



/// The application should run from the command line, accept user actions and inputs as arguments, and store the tasks in a JSON file. The user should be able to:
//
//Add, Update, and Delete tasks
//Mark a task as in progress or done
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


void Main()
{
    Helper helper = new();
    bool isRunning = true;
    while(isRunning)
    {
        Console.WriteLine("Please input a command");
        string input = Console.ReadLine();
        Console.WriteLine(input);

        if(String.IsNullOrEmpty(input))
        {
            Console.WriteLine("Invalid Command");
            break;
        }


        string[] cmds = helper.ParseCommands(input);

        // Check first command is correct, i.e. is task-cli
        if(cmds[0] != "task-cli")
        {
            Console.WriteLine(@"All valid must begin with 'task-cli'
Please use the 'help' command for more information");
        }

        //string jsonPath = GetJsonPath();
        //Console.WriteLine(jsonPath);
        //File.WriteAllText(jsonPath, "1234");


        // Switching between commands
        switch(input)
        {
            case "add":
                AddTask();
                break;
            case "update":
                break;
            case "exit":
                isRunning = false;
                break;
            case "help":
                PrintHelpMessage();
                break;
            default:
                break;
        }

    }
}

Boolean CheckCorrectBeginning(String firstCommand)
{
    return firstCommand == "task-cli";
}


void PrintHelpMessage()
{
    Console.WriteLine(@"Commands
    1234
1254
444
");
}

void ReadJsonFile()
{
    try
    {
        string jsonPath = GetJsonPath() ??
            throw new Exception("The path of the JSON file is null");
        using StreamReader reader = new(jsonPath);
        string text = reader.ReadToEnd();
        Console.WriteLine(text);
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Error: {ex}");
    }
}

String GetJsonPath()
{
    string workingDirectory = Environment.CurrentDirectory;
    string? projectDirectory = Directory.GetParent(workingDirectory)?.
                                         Parent?.Parent?.FullName;
    string jsonPath = $"{projectDirectory}/tasks.json";
    return jsonPath;
}


void AddTask()
{
    ReadJsonFile();
    return;
}


Main();