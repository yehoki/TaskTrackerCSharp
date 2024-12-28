using System;
using TaskTracker.Models;
using System.Text.RegularExpressions;
namespace TaskTracker.Services
{
    public class Helper
    {
        public Helper()
        {
        }

        public string[] ParseCommands(string input)
        {
            List<string> matchStrings = new List<string>();
            var regex = new Regex(@"[\""].+?[\""]|[^ ]+");
            var matches = regex.Matches(input);
            foreach(Match match in matches)
            {
                string trimmedValue = match.Value.Trim('"');
                matchStrings.Add(trimmedValue);
            }
            return matchStrings.ToArray();
        }


        public String GetCommand(String[] commands)
        {
            if(commands.Length == 0)
            {
                throw new Exception("No commands provided");
            }
            return "";
        }

        public String GetJsonPath()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string? projectDirectory = Directory.GetParent(workingDirectory)?.
                                                 Parent?.Parent?.FullName;
            string jsonPath = $"{projectDirectory}/tasks.json";
            return jsonPath;
        }

        public int GetHighestId(List<JsonTask> tasks)
        {
            return tasks.Max(task => task.Id);
        }
    }
}

