using TaskPlanner.Messages;

namespace TaskPlanner.Replies
{
    public class ReplyLoadFiltered
    {
        public string Filter { get; set; }
        public TaskInfo[] TaskInfos { get; set; } 
    }
}