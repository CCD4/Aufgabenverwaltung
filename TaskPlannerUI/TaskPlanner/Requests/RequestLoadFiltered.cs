namespace TaskPlanner.Requests
{
    public class RequestLoadFiltered
    {
        public RequestLoadFiltered(string filter)
        {
            Filter = filter;
        }

        public string Filter { get; set; } 
    }
}