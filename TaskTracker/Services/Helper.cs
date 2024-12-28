using System;
namespace TaskTracker.Services
{
    public class Helper
    {
        public Helper()
        {
        }

        public String[] ParseCommands(String input)
        {
            return input.Split(' ');
        }


        public String GetCommand(String[] commands)
        {
            if(commands.Length == 0)
            {
                throw new Exception("No commands provided");
            }
            return "";
        }
    }
}

