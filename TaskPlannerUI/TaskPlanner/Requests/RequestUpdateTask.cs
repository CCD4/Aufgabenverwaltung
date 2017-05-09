using System;

namespace TaskPlanner.Requests
{
    public class RequestUpdateTask
    {
        public RequestUpdateTask(Guid id, bool done, string taskName)
        {
            TaskText = taskName;
            Id = id;
            Done = done;
        }

        public string TaskText { get; set; }
        public Guid Id { get; }
        public bool Done { get; set; }
    }
}