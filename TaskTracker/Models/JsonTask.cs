using System;
namespace TaskTracker.Models
{
    public class JsonTask
    {
        public enum TaskStatus
        {
            todo, inProgress, done
        };


        public int Id;
        public string Description;
        public TaskStatus Status;
        public DateTime CreatedAt;
        public DateTime UpdatedAt;


        public JsonTask(int id, string description)
        {
            Id = id;
            Description = description;
            Status = TaskStatus.todo;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}

