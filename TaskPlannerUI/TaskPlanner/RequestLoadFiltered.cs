namespace TaskPlanner
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