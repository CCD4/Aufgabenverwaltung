namespace TaskPlanner.Requests
{
    public class RequestLoadFiltered
    {
        public RequestLoadFiltered(string filter, bool includeDoneTasks)
        {
            Filter = filter;
            IncludeDoneTasks = includeDoneTasks;
        }

        public string Filter { get; set; }
        public bool IncludeDoneTasks { get; }
    }
}