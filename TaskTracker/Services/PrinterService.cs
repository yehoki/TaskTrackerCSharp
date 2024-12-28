using System;
using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class PrinterService
    {
        public PrinterService()
        {
        }

        public void PrintHelpMessage()
        {
            Console.WriteLine(@"Please find below all possible commands:
task-cli add [description]: Adds a new task, with description
task-cli update [id] [description]: Updates task with specified Id, with the new description
task-cli delete [id]: Removes task from list of tasks
task-cli mark-progress [id]: Sets task to status InProgress
task-cli mark-done [id]: Sets task to status Done
task-cli list: Lists all tasks
task-cli list-progress
");
        }

        public void PrintInvalidArgumentMessage()
        {
            Console.WriteLine("Invalid Argument, please use the task-cli help command to see all available commands");
        }


        public void PrintNoTasks()
        {
            Console.WriteLine("There are currently no tasks");
        }

        public void PrintNoTasks(JsonTask.TaskStatus taskStatus)
        {
            Console.WriteLine($"There are currently no tasks with status {taskStatus}");
        }

        public void PrintTableHeader()
        {
            string formattedString = String.Format("|{0,-5}|{1,-5}|{2,-5}|", "Id", "Description", "Status");
            Console.WriteLine(formattedString);
        }

        public void PrintSingleTask(JsonTask task)
        {
            string formattedString = String.Format("|{0,-5}|{1,-5}|{2,-5}|", task.Id, task.Description, task.Status);
            Console.WriteLine(formattedString);
        }
    }
}

