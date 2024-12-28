using System;
namespace TaskTracker.Models
{
    public class JsonTask
    {
        public enum TaskStatus
        {
            todo, inProgress, done
        };

        public int Id { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

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

