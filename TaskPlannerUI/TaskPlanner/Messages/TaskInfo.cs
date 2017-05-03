using System;
using System.Dynamic;

namespace TaskPlanner.Messages
{
    public class TaskInfo
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool Done { get; set; }
    }
}
