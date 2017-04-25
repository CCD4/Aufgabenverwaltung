namespace TaskPlanner
{
    public class RequestAddTask
    {
        public RequestAddTask(string taskName)
        {
            TaskText = taskName;
        }

        public string TaskText { get; set; }
    }
}
