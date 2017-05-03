using System;

namespace TaskPlanner.Domain
{
    public class Task
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
        public bool Done { get; set; }
        public string[] Tags { get; set; }
    }
}