// See https://aka.ms/new-console-template for more information




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


static void main()
{
    while(true)
    {
        Console.WriteLine("Please input a command");
        string input = Console.ReadLine();
        Console.WriteLine(input);

        if(String.IsNullOrEmpty(input))
        {
            Console.WriteLine("Invalid Command");
            break;
        }

        String[] cmds = input.Split(' ');
        foreach(string cmd in cmds)
        {
            Console.WriteLine(cmd);
        }

        switch(input)
        {
            case "exit":
            {
                break;
            }
        }

    }
}

main();