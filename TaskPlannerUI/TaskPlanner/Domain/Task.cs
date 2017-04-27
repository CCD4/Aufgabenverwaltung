namespace TaskPlanner.Domain
{
    public class Task
    {
        public string Text { get; set; }
        public bool Done { get; set; }
        public string[] Tags { get; set; }
    }
}