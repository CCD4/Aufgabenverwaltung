using System;
using System.Linq;

namespace TaskPlanner
{
    public class RequestHandler
    {
        private MemoryDBProvider dbProvider;

        public RequestHandler()
        {
            dbProvider = new MemoryDBProvider();
        }

        public ReplyLoadFiltered LoadTasks(RequestLoadFiltered request)
        {
            string[] tags = ParseQuery(request.Filter);
            var tasks = dbProvider.LoadTasks(tags);
            var taskInfos = Map(tasks);
            return new ReplyLoadFiltered
            {
                Filter = request.Filter,
                TaskInfos = taskInfos
            };
        }

        private TaskInfo[] Map(Task[] tasks)
        {
            return tasks.Select(task => new TaskInfo
            {
                Text = task.Text,
                Done = task.Done
            }).ToArray();
        }

        private string[] ParseQuery(string filter)
        {
            var tags = filter.Split(new[] {'#', ' '}, StringSplitOptions.RemoveEmptyEntries);
            return tags.Select(t => '#' + t).ToArray();
        }

        public ReplayLoadTags LoadTags(RequestLoadTags request)
        {
            var tags = dbProvider.LoadTags();
            return new ReplayLoadTags {Tags = tags.ToArray()};
        }
    }

}